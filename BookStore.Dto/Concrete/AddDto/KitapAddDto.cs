using BookStore.Dto.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete.AddDto;

public class KitapAddDto : IDto
{
    public string YazarId { get; set; }
    public string KitapAdi { get; set; }
    public string Dosya { get; set; }
    public short AktifMi { get; set; }
    public IFormFile File { get; set; }

}
