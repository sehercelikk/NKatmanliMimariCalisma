using BookStore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Concrete
{
    public class KitapIcerik :BaseEntity, IEntity
    {
        public string KitapId { get; set; }
        public Kitap Kitap { get; set; }
        public string YazarId { get; set; }
        public Yazar Yazar { get; set; }
        //public string Baslik { get; set; }
        public string Dosya { get; set; }
    }
}
