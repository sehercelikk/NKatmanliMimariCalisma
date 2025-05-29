using BookStore.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete.UpdateDto;

public class KitapUpdateDto : IDto
{
    public string Id { get; set; }
    public string YazarId { get; set; }
    public string KitapAdi { get; set; }
    public string Dosya { get; set; }
}
