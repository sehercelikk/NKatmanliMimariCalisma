using AutoMapper;
using BookStore.Business.Abstract.GenericServiceInterface;
using BookStore.Dto.Concrete;
using BookStore.Dto.Concrete.AddDto;
using BookStore.Dto.Concrete.UpdateDto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGenericServiceBookStore _genericServiceBookStore;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        public AdminController(IGenericServiceBookStore genericServiceBookStore, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
           _contextAccessor = contextAccessor;
           _genericServiceBookStore = genericServiceBookStore;
           _mapper = mapper;   
        }
        public IActionResult Index() => View();

        #region Kitap
        public async Task<IActionResult> Kitap() => View();
        public async Task<IActionResult> GetKitaplar()
        {
            var result = await _genericServiceBookStore.KitapService.GetAllAsync();
            if (result.Data != null)
                return Json(result.Data.ToList());
            return Json(new List<KitapDto>());
        }

        public async Task<IActionResult> GetKitaplarDrop(string id)
        {
            var result = await _genericServiceBookStore.KitapService.KitaplarAsync(id);
            if (result.Data != null)
                return Json(result.Data.ToList());
            return Json(new List<KitapDto>());
        }


        public async Task<IActionResult> KitapEkle() => View();
        [HttpPost]
        public async Task<IActionResult> KitapEkle(KitapAddDto model)
        {
            var result = await _genericServiceBookStore.KitapService.KitapEkleAsync(model);
            return Json(result);
        }

        public async Task<IActionResult> KitapGuncelle(string id)
        {
            var result = await _genericServiceBookStore.KitapService.GetByIdAsync(id);
            return Json(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> KitapGuncelle(KitapUpdateDto model)
        {
            var result = await _genericServiceBookStore.KitapService.KitapGuncelleAsync(model);
            return Json(result);
        }

        public async Task<IActionResult> KitapSil(string id)
        {
            var result = await _genericServiceBookStore.KitapService.KitapSil(id);
            return Json(result);
        }



        #endregion

        #region Yazar
        public async Task<IActionResult> Yazar() => View();
        public async Task<IActionResult> GetYazarlar()
        {
            var result = await _genericServiceBookStore.YazarService.YazarlarAsync();
            if(result.Data !=null)
                return Json(result.Data.ToList());
            return Json(new List<YazarDto>());
        }

        public async Task<IActionResult> YazarEkle() => View();

        [HttpPost]
        public async Task<IActionResult> YazarEkle(YazarAddDto model)
        {
            var result = await _genericServiceBookStore.YazarService.YazarEkleAsync(model);
            return Json(result);
        }

        public async Task<IActionResult> YazarGuncelle(string id)
        {
            var result = await _genericServiceBookStore.YazarService.GetByIdAsync(id);
            return Json(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> YazarGuncelle(YazarUpdateDto model)
        {
            var result= await _genericServiceBookStore.YazarService.YazarGuncelleAsync(model);
            return Json(result);
        }

        public async Task<IActionResult> YazarSil(string id)
        {
            var result = await _genericServiceBookStore.YazarService.YazarSil(id);
            return Json(result);
        }


        #endregion

        #region İçerik
        public async Task<IActionResult> KitapIcerik() => View();
        public async Task<IActionResult> GetKitapIcerikler()
        {

            var result = await _genericServiceBookStore.KitapIcerikService.KitapIceriklerAsync();
            if (result.Data != null)
                return Json(result.Data.OrderByDescending(x => x.YazarAdi).ToList());
            return Json(new List<KitapIcerikDto>());

        }


        public async Task<IActionResult> KitapIcerikEkle() => View();

        [HttpPost]
        public async Task<IActionResult> KitapIcerikEkle(KitapIcerikAddDto model)
        {
            var result = await _genericServiceBookStore.KitapIcerikService.KitapIcerikEkleAsync(model);
            return Json(result);
        }

        public async Task<IActionResult> KitapIcerikGuncelle(string id)
        {
            var result = await _genericServiceBookStore.KitapIcerikService.UpdateGetByIdAsync(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> KitapIcerikGuncelle(KitapIcerikUpdateDto model)
        {
            var result = await _genericServiceBookStore.KitapIcerikService.KitapIcerikGuncelleAsync(model);
            return Json(result);
        }



        public async Task<IActionResult> KitapIcerikSil(string id)
        {
            var result = await _genericServiceBookStore.KitapIcerikService.KitapIcerikSilAsync(id);
            return Json(result);
        }


        #endregion
    }
}
