using AutoMapper;
using BookStore.Business.Abstract.BookStore;
using BookStore.Business.Concrete.ManagerBase;
using BookStore.Core.Utilities.Result;
using BookStore.Core.Utilities.Result.Abstract;
using BookStore.Core.Utilities.Result.Concrete;
using BookStore.DataAccess.UnitOfWork.Abstract;
using BookStore.Dto.Concrete;
using BookStore.Dto.Concrete.AddDto;
using BookStore.Dto.Concrete.UpdateDto;
using BookStore.Entities.Concrete;

namespace BookStore.Business.Concrete.BookStore;

public class KitapIcerikService : ManagerBaseBookStore, IKitapIcerikService
{
    public KitapIcerikService(IUnitOfWorkBookStore unitOfWorkBookStore, IMapper mapper) : base(unitOfWorkBookStore, mapper)
    {
    }

    public async Task<IDataResult<List<KitapIcerikDto>>> KitapIceriklerAsync()
    {
        try
        {
            var result = await _unitOfWorkBookStore.KitapIcerikDal.KitapIceriklerAsync();
            if (result == null)
                return new DataResult<List<KitapIcerikDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null, null);
            return new DataResult<List<KitapIcerikDto>>(ResultStatus.Success, "", _mapper.Map<List<KitapIcerikDto>>(result), null);
        }
        catch (Exception ex)
        {
            return new DataResult<List<KitapIcerikDto>>(ResultStatus.Errored, "", null, ex);

        }
    }


    public async Task<IResult> KitapIcerikEkleAsync(KitapIcerikAddDto model)
    {

        try
        {

            var result = await _unitOfWorkBookStore.KitapIcerikDal.AnyAsync(x => x.Dosya == model.Dosya); //Mükerrer kayıt kontrolü değişmeli
            if (result)
                return new Result(ResultStatus.Info, "Bu isimde bir dosya zaten mevcut", null);
            var data = _mapper.Map<KitapIcerik>(model);
            data.AktifMi = 1;
            data.SilindiMi = 0;
            byte[] dosya;
            using (var ms = new MemoryStream())
            {
                model.File.CopyTo(ms);
                dosya = ms.ToArray();
            }
            var wwwRootPath = Environment.CurrentDirectory + "/wwwroot/";
            var serverPath = "Uploads/KitapIcerikleri/";
            var fileName = Guid.NewGuid().ToString() + ".pdf";
            if (!Directory.Exists(wwwRootPath + serverPath))
                Directory.CreateDirectory(wwwRootPath + serverPath);
            data.Dosya = serverPath + fileName;
            await File.WriteAllBytesAsync(wwwRootPath + serverPath + fileName, dosya);

            await _unitOfWorkBookStore.KitapIcerikDal.AddAsync(data);
            await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "", null);
        }
        catch (Exception ex)
        {
            return new Result(ResultStatus.Errored, "", ex);
        }
    }

    public async Task<IResult> KitapIcerikGuncelleAsync(KitapIcerikUpdateDto model)
    {
        try
        {
            var result = await _unitOfWorkBookStore.KitapIcerikDal.GetAsync(s => s.Id == model.Id);
            if (result == null)
                return new Result(ResultStatus.Info, "Kayıt Bulunamadı", null);
            var data = _mapper.Map<KitapIcerik>(model);
            data.AktifMi = 1;
            data.SilindiMi = 0;
            byte[] dosya;
            using (var ms = new MemoryStream())
            {
                model.File.CopyTo(ms);
                dosya = ms.ToArray();
            }
            var wwwRootPath = Environment.CurrentDirectory + "/wwwroot/";
            var serverPath = "Uploads/KitapIcerikleri/";
            var fileName = Guid.NewGuid().ToString() + ".pdf";
            if (!Directory.Exists(wwwRootPath + serverPath))
                Directory.CreateDirectory(wwwRootPath + serverPath);
            data.Dosya = serverPath + fileName;
            await File.WriteAllBytesAsync(wwwRootPath + serverPath + fileName, dosya);

            await _unitOfWorkBookStore.KitapIcerikDal.UpdateAsync(data);
            await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt Başarıyla Güncellendi.", null);

        }
        catch (Exception ex)
        {

            return new Result(ResultStatus.Errored, "", ex);

        }

    }




    public async Task<IResult> KitapIcerikSilAsync(string id)
    {
        try
        {
            var result = await _unitOfWorkBookStore.KitapIcerikDal.GetAsync(x => x.Id == id);
            if (result == null)
                return new Result(ResultStatus.Info, "Kayıt Bulunamadı", null);

            await _unitOfWorkBookStore.KitapIcerikDal.DeleteAsync(result);
            await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt başarıyla silindi", null);
        }
        catch (Exception ex)
        {
            return new Result(ResultStatus.Errored, "", ex);

        }
    }


    public async Task<IDataResult<KitapIcerikUpdateDto>> UpdateGetByIdAsync(string id)
    {
        try
        {
            KitapIcerikUpdateDto model = new();
            var entity = await _unitOfWorkBookStore.KitapIcerikDal
                .GetAsync(
                x => x.Id == id,
                x => x.Kitap,
                x => x.Yazar
                );
            if (entity != null)
            {
                model.KitapId= entity.KitapId;
                model.YazarId = entity.YazarId;
                model.Dosya = entity.Dosya;
                model.Id = entity.Id;

            }
            return new DataResult<KitapIcerikUpdateDto>(ResultStatus.Success, "", model, null);
        }
        catch (Exception ex)
        {
            return new DataResult<KitapIcerikUpdateDto>(ResultStatus.Errored, "", null, ex);
        }
    }

}
