using BookStore.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete;

public class YazarDto : IDto
{
    public string Id { get; set; }
    public string YazarAdi { get; set; }
    public short AktifMi { get; set; }
    
}
