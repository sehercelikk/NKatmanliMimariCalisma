using BookStore.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete.UpdateDto;

public class YazarUpdateDto : IDto
{
    public string Id { get; set; }
    public string YazarAdi { get; set; }

}
