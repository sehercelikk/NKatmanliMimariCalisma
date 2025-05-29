using BookStore.Dto.Abstract;

namespace BookStore.Dto.Concrete.AddDto;

public class YazarAddDto : IDto
{
    public string YazarAdi { get; set; }
    public short AktifMi { get; set; }
}
