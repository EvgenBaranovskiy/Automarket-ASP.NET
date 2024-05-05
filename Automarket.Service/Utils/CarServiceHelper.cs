using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.ViewModel.Car;

namespace Automarket.Service.Utils
{
    static public class CarServiceHelper
    {
        static public List<Car> SortByCarSortType(this List<Car> Cars, CarSortType CarSortType)
        {
            switch (CarSortType)
            {
                case CarSortType.SortByPriceAsc:
                    return Cars.OrderBy(car => car.Price).ToList();
                case CarSortType.SortByPriceDesc:
                    return Cars.OrderByDescending(car => car.Price).ToList();
                case CarSortType.SortBySpeedAsc:
                    return Cars.OrderBy(car => car.Speed).ToList();
                case CarSortType.SortBySpeedDesc:
                    return Cars.OrderByDescending(car => car.Speed).ToList();
                default:
                    throw new NotImplementedException("SortByCarSortType()");
            }
        }
        static public int GetTotalPageNumber(List<Car> cars, int amountPerPage)
        {
            return (int)Math.Ceiling((double)cars.Count / amountPerPage);
        }
        static public bool IsLastPage(List<Car> cars, int amountPerPage, int currentPage)
        {
            return GetTotalPageNumber(cars, amountPerPage) == currentPage;
        }
        static public List<CarViewModel> CreateCarsVM(List<Car> cars)
        {
            List<CarViewModel> carsPreview = new List<CarViewModel>();
            foreach (var car in cars)
            {
                carsPreview.Add(new CarViewModel(car));
            }
            return carsPreview;
        }
    }
}