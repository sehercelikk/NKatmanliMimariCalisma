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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Concrete.BookStore;

public class YazarService : ManagerBaseBookStore, IYazarService
{
    public YazarService(IUnitOfWorkBookStore unitOfWorkBookStore, IMapper mapper) : base(unitOfWorkBookStore, mapper)
    {
    }

    public async Task<IDataResult<List<YazarDto>>> YazarlarAsync()
    {
		try
		{
			var result = await _unitOfWorkBookStore.YazarDal.YazarlarAsync();
			if (result == null) 
				return new DataResult<List<YazarDto>>(ResultStatus.Info,"Kayıt Bulunamadı", null, null);
			return new DataResult<List<YazarDto>>(ResultStatus.Success, "", _mapper.Map<List<YazarDto>>(result),null);
        }
        catch (Exception ex)
		{
            return new DataResult<List<YazarDto>>(ResultStatus.Errored, "", null, ex);
        }
    }

    public async Task<IResult> YazarEkleAsync(YazarAddDto model)
    {
		try
		{
			var result = await _unitOfWorkBookStore.YazarDal.AnyAsync(a => a.YazarAdi == model.YazarAdi && a.AktifMi == 1);
			if (result)
				return new Result(ResultStatus.Info, "Aynı Kayıttan tekrar eklenemez", null);
			var data = _mapper.Map<Yazar>(model);
            data.AktifMi = 1;
            data.SilindiMi = 0;
            await _unitOfWorkBookStore.YazarDal.AddAsync(data);
            await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt başarıyla eklendi", null);


        }
        catch (Exception ex)
		{
            return new Result(ResultStatus.Errored, "Ekleme işlemi sırasında bir hata oluştu", ex);

        }
    }

    public async Task<IDataResult<YazarDto>> GetByIdAsync(string id)
    {
        try
        {
            var result = await _unitOfWorkBookStore.YazarDal.GetAsync(a=>a.Id == id);
            if(result==null)
                return new DataResult<YazarDto>(ResultStatus.Info, "Kayıt Bulunamadı", null, null);
            return new DataResult<YazarDto>(ResultStatus.Success, "", _mapper.Map<YazarDto>(result), null);
        }
        catch (Exception ex)
        {
            return new DataResult<YazarDto>(ResultStatus.Errored, "", null, ex);
        }
    }

    public async Task<IResult> YazarGuncelleAsync(YazarUpdateDto model)
    {
        try
        {
            var result = await _unitOfWorkBookStore.YazarDal.GetAsync(a=>a.YazarAdi==model.YazarAdi);
            var kontrol = await _unitOfWorkBookStore.KitapDal.AnyAsync(a => a.YazarId == model.Id);
            if (result != null)
                return new Result(ResultStatus.Info, "Güncellemek istediğiniz isim zaten mevcut", null);
            await _unitOfWorkBookStore.YazarDal.UpdateAsync(_mapper.Map<Yazar>(model));
            await _unitOfWorkBookStore.SaveAsync(); 
            return new Result(ResultStatus.Success, "Kayıt başarıyla güncellendi", null);
        }
        catch (Exception ex)
        {
            return new Result(ResultStatus.Errored, "", ex);    

        }
    }

    public async Task<IResult> YazarSil(string id)
    {
        try
        {
            var result = await _unitOfWorkBookStore.YazarDal.GetAsync(a => a.Id == id);
            var kontrol= await _unitOfWorkBookStore.KitapIcerikDal.AnyAsync(a=>a.YazarId==id);
            var kontrol2 = await _unitOfWorkBookStore.KitapDal.AnyAsync(a => a.YazarId == id);
            if (result == null)
                return new Result(ResultStatus.Info, "Kayıt Bulunamadı", null);
            if(kontrol || kontrol2)
                return new Result(ResultStatus.Info, "Bu yazara ait kitap ve içerik bulunduğu için silinemez!", null);
            result.SilmeZamani=DateTime.Now;
            await _unitOfWorkBookStore.YazarDal.DeleteAsync(result);
            await _unitOfWorkBookStore.SaveAsync();
            return new Result(ResultStatus.Success, "Kayıt Başarıyla Silindi.", null);
        }
        catch (Exception ex)
        {
            return new Result(ResultStatus.Errored, "", ex);
        }
    }
}

           