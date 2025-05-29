using AutoMapper;
using BookStore.Business.Abstract.BookStore;
using BookStore.Business.Abstract.GenericServiceInterface;
using BookStore.Business.Concrete.BookStore;
using BookStore.Business.Concrete.ManagerBase;
using BookStore.DataAccess.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Concrete.GenericService;

public class GenericServiceBookStore : ManagerBaseBookStore, IGenericServiceBookStore
{
    public GenericServiceBookStore(IUnitOfWorkBookStore unitOfWorkBookStore, IMapper mapper) : base(unitOfWorkBookStore, mapper)
    {
    }

    private YazarService _yazarService;
    private KitapService _kitapService;
    private KitapIcerikService _kitapIcerikService;

    public IYazarService YazarService => _yazarService ?? new YazarService(_unitOfWorkBookStore, _mapper);

    public IKitapService KitapService => _kitapService ?? new KitapService(_unitOfWorkBookStore, _mapper);

    public IKitapIcerikService KitapIcerikService => _kitapIcerikService ?? new KitapIcerikService(_unitOfWorkBookStore,_mapper);
}
