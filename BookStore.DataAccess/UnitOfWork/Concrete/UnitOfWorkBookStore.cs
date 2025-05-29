using BookStore.DataAccess.Abstract;
using BookStore.DataAccess.Concrete;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWorkBookStore : IUnitOfWorkBookStore
    {
        private readonly DataContext _context;
        public UnitOfWorkBookStore(DataContext context)
        {
            _context = context;
        }

        private EfKitapRepo _efKitapRepo;
        private EfKitapIcerikRepo _efKitapIcerikRepo;
        private EfYazarRepo _efYazarRepo;
        public IKitapDal KitapDal => _efKitapRepo ??= new EfKitapRepo(_context);

        public IYazarDal YazarDal => _efYazarRepo ??= new EfYazarRepo(_context);

        public IKitapIcerikDal KitapIcerikDal => _efKitapIcerikRepo ??= new EfKitapIcerikRepo(_context);

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
