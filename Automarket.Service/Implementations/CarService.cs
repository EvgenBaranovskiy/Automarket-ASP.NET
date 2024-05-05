using Automarket.Domain.Entity;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;
using Automarket.DAL.Interfaces;
using Automarket.Domain.ViewModel.Car;
using Automarket.Domain.Enum;
using Automarket.Service.Utils;

namespace Automarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        private readonly IFileRepository _fileRepository;

        public CarService(ICarRepository carRepository, IFileRepository fileRepository)
        {
            _carRepository = carRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<IBaseResponse<int>> CreateCar(CarViewModel model)
        {
            try
            {
                var _car = new Car();

                if (model.AvatarImage != null)
                    _car.AvatarImgUrl = await _fileRepository.SaveCarAvatarAsync(model.Id, model.AvatarImage);
                if (model.SliderImages != null)
                    _car.Images = await _fileRepository.SaveCarImagesAsync(model.Id, model.SliderImages);

                _car.Name = model.Name;
                _car.Model = model.Model;
                _car.ShortDescription = model.ShortDescription;
                _car.FullDescription = model.FullDescription;
                _car.Price = model.Price;
                _car.Speed = model.Speed;
                _car.TypeCar = (TypeCar)Enum.Parse(typeof(TypeCar), model.TypeCar);

                var newCarID = await _carRepository.CreateAsync(_car);

                return new BaseResponse<int>
                {
                    Data = newCarID,
                    Description = "Автомобіль додано до БД.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>
                {
                    Data = -1,
                    Description = $"[CreateCar] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            try
            {
                var car = await _carRepository.GetAsync(id);

                if (car == null)
                {
                    return new BaseResponse<bool>
                    {
                        Data = false,
                        Description = $"Автомобіль з таким id не знайдений.",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                var result = await _carRepository.DeleteAsync(car);

                return new BaseResponse<bool>
                {
                    Data = result,
                    Description = "Автомобіль видалено!",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = $"[DeleteCar] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<CarViewModel>> GetCarById(int id)
        {
            try
            {
                var car = await _carRepository.GetAsync(id);

                if (car == null)
                {
                    return new BaseResponse<CarViewModel>
                    {
                        Description = $"Автомобіль з таким id не знайдений.",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                return new BaseResponse<CarViewModel>
                {
                    Data = new CarViewModel(car),
                    Description = $"Знайдено автомобіль за id `{id}`.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>
                {
                    Description = $"[GetCarById] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Car>> GetByNameCar(string name)
        {
            try
            {
                var car = await _carRepository.GetByNameAsync(name);

                if (car == null)
                {
                    return new BaseResponse<Car>
                    {
                        Description = $"Автомобіль з таким ім'ям не знайдений.",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                return new BaseResponse<Car>
                {
                    Data = car,
                    Description = $"Знайдено автомобіль за ім`ям `{name}`.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>
                {
                    Description = $"[GetByNameCar] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<Car>>> GetAllCars()
        {
            try
            {
                var cars = await _carRepository.SelectAsync();
                return new BaseResponse<IEnumerable<Car>>
                {
                    Data = cars,
                    Description = $"Знайдено {cars.Count} елементів типу Car.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>
                {
                    Description = $"[GetCars] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Car>> EditCar(CarViewModel model)
        {
            try
            {
                var car = await _carRepository.GetAsync(model.Id);

                if (car == null)
                {
                    return new BaseResponse<Car>
                    {
                        Description = $"Автомобіль з таким id не знайдений.",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                if (model.AvatarImage != null)
                    car.AvatarImgUrl = await _fileRepository.SaveCarAvatarAsync(model.Id, model.AvatarImage);
                if (model.SliderImages != null)
                    car.Images = await _fileRepository.SaveCarImagesAsync(model.Id, model.SliderImages);

                car.Name = model.Name;
                car.Model = model.Model;
                car.ShortDescription = model.ShortDescription;
                car.FullDescription = model.FullDescription;
                car.Price = model.Price;
                car.Speed = model.Speed;
                car.TypeCar = (TypeCar)Enum.Parse(typeof(TypeCar), model.TypeCar);

                var response = await _carRepository.UpdateAsync(car);

                return new BaseResponse<Car>
                {
                    Data = car,
                    Description = $"Зміненно автомобіль за id `{model.Id}`.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>
                {
                    Description = $"[EditCar] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<PageCarsViewModel>> GetPageOfCars(int pageNumber,
            int countPerPage,
            CarSortType carSortType)
        {
            try
            {
                var cars = await _carRepository.SelectAsync();

                //Перевірка чи вдалося завантажити автомобілі з БД
                if (cars == null)
                {
                    return new BaseResponse<PageCarsViewModel>
                    {
                        Description = $"Не вдалося отримати жодного автомобіля з БД!",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                //Перевірка чи корректно введена сторінка
                var totalPageCount = CarServiceHelper.GetTotalPageNumber(cars, countPerPage);
                if (pageNumber <= 0 || pageNumber > totalPageCount)
                {
                    return new BaseResponse<PageCarsViewModel>
                    {
                        Description = $"Не вказаній сторінці `{pageNumber}` немає автомобілів!",
                        StatusCode = StatusCode.WrongPageNumber
                    };
                }

                //Якщо, номер сторінки корректний то формується сторінка для View
                int firstIndexOnTargetPage = (pageNumber - 1) * countPerPage;
                cars = cars.SortByCarSortType(carSortType); //Сортування
                cars = cars.Skip(firstIndexOnTargetPage).Take(countPerPage).ToList(); //Зріз

                //Формування VM
                var viewModel = new PageCarsViewModel(
                    pageNumber: pageNumber,
                    totalNumber: totalPageCount,
                    carSortType: carSortType,
                    cars: CarServiceHelper.CreateCarsVM(cars));

                return new BaseResponse<PageCarsViewModel>
                {
                    Data = viewModel,
                    Description = $"Формування VM для виводу сторінки з авто виконано успішно!",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PageCarsViewModel>
                {
                    Description = $"[GetPageOfCars] Exception: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}