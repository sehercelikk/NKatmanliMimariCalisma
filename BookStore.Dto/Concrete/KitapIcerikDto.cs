using BookStore.Dto.Abstract;
using BookStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete;

public class KitapIcerikDto : IDto
{
    public string Id { get; set; }
    public string KitapId { get; set; }
    public string KitapAdi { get; set; }
    public Kitap Kitap { get; set; }
    public string YazarId { get; set; }
    public string YazarAdi { get; set; }
    public Yazar Yazar { get; set; }
    public string Dosya { get; set; }
    public short AktifMi { get; set; }

}
