using BookStore.Core.DataAccess.Concrete;
using BookStore.DataAccess.Abstract;
using BookStore.DataAccess.Context;
using BookStore.Dto.Concrete;
using BookStore.Entities.Concrete;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Concrete;

public class EfKitapRepo : EfGenericRepository<Kitap>, IKitapDal
{
    private DataContext? context => _context as DataContext; 
    public EfKitapRepo(DbContext context) : base(context)
    {
    }

    public async Task<List<KitapDto>> KitaplarAsync()
    {
        var sartK = PredicateBuilder.New<Kitap>();
        var sartY= PredicateBuilder.New<Yazar>();

        sartK.And(a => a.AktifMi == 1 && a.SilindiMi == 0);
        sartY.And(a=>a.AktifMi==1 && a.SilindiMi == 0);

        var query = from k in context.Kitaplar.Where(sartK)
                    join y in context.Yazarlar.Where(sartY)
                    on k.YazarId equals y.Id
                    select new KitapDto
                    {
                        Id = k.Id,
                        YazarId = y.Id,
                        KitapAdi = k.KitapAdi,
                        YazarAdi=y.YazarAdi
                    };
        return await query.ToListAsync();
    }
}
