using BookStore.Dto.Abstract;
using BookStore.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Dto.Concrete.UpdateDto
{
    public class KitapIcerikUpdateDto : IDto
    {
        public string Id { get; set; }
        public string KitapId { get; set; }
        public string YazarId { get; set; }
        public string Dosya { get; set; }
        public short AktifMi { get; set; }
        public IFormFile File { get; set; }


    }
}
