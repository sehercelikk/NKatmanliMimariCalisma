using AutoMapper;
using BookStore.DataAccess.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Concrete.ManagerBase;

public class ManagerBaseBookStore
{
    private IUnitOfWorkBookStore unitOfWorkBookStore;
    private IMapper mapper;

    public IUnitOfWorkBookStore _unitOfWorkBookStore { get; }
    public IMapper _mapper { get; set; }

    public ManagerBaseBookStore(IUnitOfWorkBookStore unitOfWorkBookStore, IMapper mapper)
    {
        _unitOfWorkBookStore = unitOfWorkBookStore;
        _mapper = mapper;
    }
}
