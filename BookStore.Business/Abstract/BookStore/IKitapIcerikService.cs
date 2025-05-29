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

public interface IKitapIcerikService
{
    Task<IDataResult<List<KitapIcerikDto>>> KitapIceriklerAsync();
    Task<IResult> KitapIcerikEkleAsync(KitapIcerikAddDto model);
    Task<IResult> KitapIcerikGuncelleAsync(KitapIcerikUpdateDto model);
    Task<IResult> KitapIcerikSilAsync(string id);
    Task<IDataResult<KitapIcerikUpdateDto>> UpdateGetByIdAsync(string id);
}
