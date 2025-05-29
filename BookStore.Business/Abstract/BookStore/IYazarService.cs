using BookStore.Business.Abstract.GenericServiceInterface;
using BookStore.Core.Utilities.Result.Abstract;
using BookStore.Dto.Concrete;
using BookStore.Dto.Concrete.AddDto;
using BookStore.Dto.Concrete.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Abstract.BookStore;

public interface IYazarService
{
    Task<IDataResult<List<YazarDto>>> YazarlarAsync();
    Task<IDataResult<YazarDto>> GetByIdAsync(string id);
    Task<IResult> YazarEkleAsync(YazarAddDto model);
    Task<IResult> YazarGuncelleAsync(YazarUpdateDto model);
    Task<IResult> YazarSil(string id);
}
