using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Concrete;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public short AktifMi { get; set; } = 1;
    public short SilindiMi { get; set; } = 0;
    public DateTime EklemeZamani { get; set; } = DateTime.Now;
    public DateTime? PasifeAlmaZamani { get; set; }
    public DateTime? SilmeZamani { get; set; }
}
