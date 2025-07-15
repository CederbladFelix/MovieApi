namespace Movies.Core.Models.DTOs
{
    public class PaginationOptionsDto
    {
        private const int MaxPageSize = 100;

        private int pageSize = 10;
        private int currentPage = 1;

        public int CurrentPage
        {
            get => currentPage;
            set => currentPage = (value < 1) ? 1 : value;
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
