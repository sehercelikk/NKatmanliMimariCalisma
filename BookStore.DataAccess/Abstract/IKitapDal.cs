using BookStore.Core.DataAccess.Abstract;
using BookStore.Dto.Concrete;
using BookStore.Entities.Concrete;


namespace BookStore.DataAccess.Abstract;

public interface IKitapDal : IGenericDal<Kitap>
{
    Task<List<KitapDto>> KitaplarAsync();
}
