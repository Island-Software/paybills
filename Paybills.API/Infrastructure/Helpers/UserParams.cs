namespace Paybills.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}