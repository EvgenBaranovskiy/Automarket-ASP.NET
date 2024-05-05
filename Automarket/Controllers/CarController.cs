using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.ViewModel.Car;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class CarController : Controller
    {
        ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarAsync(int id)
        {
            var response = await _carService.GetCarById(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetPageAsync(int number = 1, CarSortType sortType = CarSortType.SortByPriceAsc)
        {
            var response = await _carService.GetPageOfCars(number, countPerPage: 6, sortType);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("~/Views/Shared/Error.cshtml");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCarAsync(int id)
        {
            var response = await _carService.GetCarById(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCarAsync(CarViewModel carVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.EditCar(carVM);

                switch (response.StatusCode)
                {
                    case Domain.Enum.StatusCode.OK:
                        return RedirectToAction("GetCar", new { id = carVM.Id });
                    case Domain.Enum.StatusCode.InternalServerError:
                        ModelState.AddModelError("InternalServerError", response.Description);
                        break;
                    default:
                        ModelState.AddModelError(response.StatusCode.ToString(), response.Description);
                        break;
                }
            }
            else
            {
                //Підвантаження встановленних зображень у випадку проблем з валідацією
                var response = await _carService.GetCarById(carVM.Id);
                carVM.SliderPathImages = response.Data.SliderPathImages;
                carVM.AvatarImgPath = response.Data.AvatarImgPath;
            }

            return View(carVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCarAsync()
        {
            return View(new CarViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCarAsync(CarViewModel carVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.CreateCar(carVM);
                switch (response.StatusCode)
                {
                    case Domain.Enum.StatusCode.OK:
                        return RedirectToAction("GetCar", new { id = response.Data });
                    case Domain.Enum.StatusCode.InternalServerError:
                        ModelState.AddModelError("InternalServerError", response.Description);
                        break;
                    default:
                        ModelState.AddModelError(response.StatusCode.ToString(), response.Description);
                        break;
                }
            }

            return View(carVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCar(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK) 
            {
                return RedirectToAction("GetPage");
            }

            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0) { return View(); }

            var response = await _carService.GetCarById(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}