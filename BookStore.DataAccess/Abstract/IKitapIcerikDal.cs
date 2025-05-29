using BookStore.Core.DataAccess.Abstract;
using BookStore.Dto.Concrete;
using BookStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Abstract;

public interface IKitapIcerikDal : IGenericDal<KitapIcerik>
{
    Task<List<KitapIcerikDto>> KitapIceriklerAsync();
}
