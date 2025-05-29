using BookStore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Concrete;

public class Yazar : BaseEntity, IEntity
{
    public string YazarAdi { get; set; }
    public List<Kitap> Kitaplar { get; set; }
    public List<KitapIcerik> KitapIcerikler { get; set; }
}
