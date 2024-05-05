namespace Automarket.Domain.ViewModel.Order
{
	public class PageOrdersViewModel
	{
		public int CurrentNumber { get; }
		public bool HasPreviousPage { get; }
		public bool HasNextPage { get; }
		public int TotalNumber { get; }
		public List<OrderViewModel> Orders { get; }
		public PageOrdersViewModel(int pageNumber, int totalNumber, List<OrderViewModel> orders)
		{
			CurrentNumber = pageNumber;
			TotalNumber = totalNumber;
			Orders = orders;
			HasPreviousPage = CurrentNumber != 1;
			HasNextPage = CurrentNumber != TotalNumber;
		}
	}
}
