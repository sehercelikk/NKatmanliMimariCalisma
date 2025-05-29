using BookStore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Concrete;

public class Kitap : BaseEntity, IEntity
{
    public string KitapAdi { get; set; }
    public string YazarId { get; set; }
    public Yazar Yazar { get; set; }
    public List<KitapIcerik> KitapIcerikler { get; set; }

}
