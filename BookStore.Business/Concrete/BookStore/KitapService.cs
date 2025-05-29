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

public class KitapService : ManagerBaseBookStore, IKitapService
{
    public KitapService(IUnitOfWorkBookStore unitOfWorkBookStore, IMapper mapper) : base(unitOfWorkBookStore, mapper)
    {
    }

    public async Task<IDataResult<List<KitapDto>>> GetAllAsync()
    {
		try
		{
			var result =await  _unitOfWorkBookStore.KitapDal.KitaplarAsync();
			if(result == null)
				return new DataResult<List<KitapDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null,null);
			return new DataResult<List<KitapDto>>(ResultStatus.Success,"", _mapper.Map<List<KitapDto>>(result),null);

		}
		catch (Exception ex)
		{
            return new DataResult<List<KitapDto>>(ResultStatus.Errored, "", null, ex);
        }
    }


    public async Task<IDataResult<List<KitapDto>>> KitaplarAsync(string id)
    {
        try
        {
            var result = await _unitOfWorkBookStore.KitapDal.GetAll(a=>a.YazarId==id);
            if (result == null)
                return new DataResult<List<KitapDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null, null);
            return new DataResult<List<KitapDto>>(ResultStatus.Success, "", _mapper.Map<List<KitapDto>>(result), null);

        }
        catch (Exception ex)
        {
            return new DataResult<List<KitapDto>>(ResultStatus.Errored, "", null, ex);
        }
    }

    public async Task<IDataResult<KitapDto>> GetByIdAsync(string id)
	{
		try
		{
			var result = await _unitOfWorkBookStore.KitapDal.GetAsync(a=>a.Id== id);
			if (result == null)
				return new DataResult<KitapDto>(ResultStatus.Info, "Kayıt Bulunamadı", null,null);
			return new DataResult<KitapDto>(ResultStatus.Success,"", _mapper.Map<KitapDto>(result), null);
		}
		catch (Exception ex)
		{
            return new DataResult<KitapDto>(ResultStatus.Errored, "", null, ex);

        }
    }

	public async Task<IResult> KitapEkleAsync(KitapAddDto model)
	{
		try
		{
			var result = await _unitOfWorkBookStore.KitapDal.GetAsync(a => a.KitapAdi == model.KitapAdi);
			if (result != null)
				return new Result(ResultStatus.Info, "Bu isimde mevcut kitap var", null);
			var data = _mapper.Map<Kitap>(model);
			data.AktifMi = 1;
			await _unitOfWorkBookStore.KitapDal.AddAsync(data);
            await _unitOfWorkBookStore.SaveAsync();

            return new Result(ResultStatus.Success, "Kayıt başarıyla eklendi", null);

        }
        catch (Exception ex)
        {

            return new Result(ResultStatus.Errored, "Ekleme işlemi sırasnında bir hata oluştu", ex);

        }
    }

	public async Task<IResult> KitapGuncelleAsync(KitapUpdateDto model)
	{
		try
		{
			var result = await _unitOfWorkBookStore.KitapDal.GetAsync(a => a.KitapAdi == model.KitapAdi);
			if (result != null)
				return new Result(ResultStatus.Info, "Güncellemek istediğiniz isim zaten mevcut",null);
            await _unitOfWorkBookStore.KitapDal.UpdateAsync(_mapper.Map<Kitap>(model));
			await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt başarıyla güncellendi", null);
        }
        catch (Exception ex)
		{
            return new Result(ResultStatus.Errored, "Güncelleme işlemi sırasnında bir hata oluştu", ex);

        }
    }

	public async Task<IResult> KitapSil(string id)
	{
		try
		{
			var result = await _unitOfWorkBookStore.KitapDal.GetAsync(a => a.Id == id);
			var kontrol = await _unitOfWorkBookStore.KitapIcerikDal.AnyAsync(a => a.KitapId == id);
			if (kontrol)
				return new Result(ResultStatus.Info, "Silmek istediğiniz kitaba ait içerik mevcut!", null);
			if (result == null)
				return new Result(ResultStatus.Info, "Kayıt Bulunamadı", null);
            result.SilmeZamani = DateTime.Now;
            await _unitOfWorkBookStore.KitapDal.DeleteAsync(result);
			await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt Başarıyla Silindi.", null);

        }
        catch (Exception ex)
		{
            return new Result(ResultStatus.Errored, "Silme işlemi sırasnında bir hata oluştu", ex);

        }
    }

}   
