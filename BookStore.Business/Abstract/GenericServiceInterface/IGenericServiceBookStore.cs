using BookStore.Business.Abstract.BookStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Abstract.GenericServiceInterface;

public interface IGenericServiceBookStore
{
    IYazarService YazarService {  get; }
    IKitapService KitapService { get; }
    IKitapIcerikService KitapIcerikService { get; }
}