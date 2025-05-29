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

public interface IKitapService
{
    Task<IDataResult<List<KitapDto>>> GetAllAsync();
    Task<IDataResult<List<KitapDto>>> KitaplarAsync(string id);
    Task<IDataResult<KitapDto>> GetByIdAsync(string id);
    Task<IResult> KitapEkleAsync(KitapAddDto model);
    Task<IResult> KitapGuncelleAsync(KitapUpdateDto model);
    Task<IResult> KitapSil(string id);
}
