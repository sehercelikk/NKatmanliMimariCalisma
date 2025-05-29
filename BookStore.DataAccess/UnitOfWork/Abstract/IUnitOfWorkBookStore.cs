using BookStore.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.UnitOfWork.Abstract;

public interface IUnitOfWorkBookStore : IAsyncDisposable
{
    IKitapDal KitapDal { get; }
    IYazarDal YazarDal { get; }
    IKitapIcerikDal KitapIcerikDal { get; }

    Task<int> SaveAsync();
}
