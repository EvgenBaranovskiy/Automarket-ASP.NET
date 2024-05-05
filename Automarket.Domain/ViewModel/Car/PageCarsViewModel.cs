using Automarket.Domain.Enum;

namespace Automarket.Domain.ViewModel.Car
{
    public class PageCarsViewModel
    {
        public int CurrentNumber { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
        public int TotalNumber { get; }
        public CarSortType CurrentCarSortType { get; }
        public List<CarViewModel> Cars { get; }
        public PageCarsViewModel(int pageNumber, int totalNumber,  CarSortType carSortType, List<CarViewModel> cars)
        {
            CurrentNumber = pageNumber;
            TotalNumber = totalNumber;
            CurrentCarSortType = carSortType;
            Cars = cars;
            HasPreviousPage = CurrentNumber != 1;
            HasNextPage = CurrentNumber != TotalNumber;
        }
    }
}