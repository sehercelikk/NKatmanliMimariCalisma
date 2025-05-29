using AutoMapper;
using BookStore.Dto.Concrete;
using BookStore.Dto.Concrete.AddDto;
using BookStore.Dto.Concrete.UpdateDto;
using BookStore.Entities.Concrete;

namespace BookStore.DataAccess.AutoMapper;

public class BookStoreProfile : Profile
{
    public BookStoreProfile()
    {
        #region SingleDto
        CreateMap<KitapDto, Kitap>().ReverseMap();
        CreateMap<YazarDto, Yazar>().ReverseMap();
        CreateMap<KitapIcerikDto, KitapIcerik>().ReverseMap();
        #endregion

        #region UpdateDto
        CreateMap<KitapUpdateDto, Kitap>().ReverseMap();
        CreateMap<YazarUpdateDto, Yazar>().ReverseMap();
        CreateMap<KitapIcerikUpdateDto, KitapIcerik>().ReverseMap();
        #endregion

        #region AddDto
        CreateMap<KitapAddDto, Kitap>().ReverseMap();
        CreateMap<YazarAddDto, Yazar>().ReverseMap();
        CreateMap<KitapIcerikAddDto, KitapIcerik>().ReverseMap();
        #endregion

        #region UpdateToSingle
        CreateMap<KitapUpdateDto, KitapDto>().ReverseMap();
        CreateMap<YazarUpdateDto, YazarDto>().ReverseMap();
        CreateMap<KitapIcerikUpdateDto, KitapIcerikDto>().ReverseMap();
        #endregion

        #region AddToSingle
        CreateMap<KitapAddDto, KitapDto>().ReverseMap();
        CreateMap<YazarAddDto, YazarDto>().ReverseMap();
        CreateMap<KitapIcerikAddDto, KitapIcerikDto>().ReverseMap();
        #endregion
    }
}
