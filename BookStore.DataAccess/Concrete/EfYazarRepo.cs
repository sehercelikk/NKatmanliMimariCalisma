using BookStore.Core.DataAccess.Concrete;
using BookStore.DataAccess.Abstract;
using BookStore.DataAccess.Context;
using BookStore.Dto.Concrete;
using BookStore.Entities.Concrete;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Concrete;

public class EfYazarRepo : EfGenericRepository<Yazar>, IYazarDal
{
    private DataContext? context => _context as DataContext;
    public EfYazarRepo(DbContext context) : base(context)
    {
    }

    public async Task<List<YazarDto>> YazarlarAsync()
    {
        var sartY = PredicateBuilder.New<Yazar>();
        sartY.And(a => a.AktifMi == 1 && a.SilindiMi == 0);

        var query = from y in context.Yazarlar.Where(sartY)
                    select new YazarDto
                    {
                        Id = y.Id,
                        YazarAdi = y.YazarAdi,
                        AktifMi = y.AktifMi,
                    };
        return await query.ToListAsync();
    }
}
