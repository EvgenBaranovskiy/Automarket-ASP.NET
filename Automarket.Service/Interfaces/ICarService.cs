using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModel.Car;

namespace Automarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetAllCars(); 
        Task<IBaseResponse<PageCarsViewModel>> GetPageOfCars(int pageNumber, int countPerPage, CarSortType carSortType); 
        Task<IBaseResponse<int>> CreateCar(CarViewModel model);
        Task<IBaseResponse<bool>> DeleteCar(int id);
        Task<IBaseResponse<CarViewModel>> GetCarById(int id);
        Task<IBaseResponse<Car>> GetByNameCar(string name);
        Task<IBaseResponse<Car>> EditCar(CarViewModel model);
    }
}
