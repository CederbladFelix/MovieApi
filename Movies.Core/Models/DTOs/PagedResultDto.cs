public class PagedResultDto<T>
{
    public IEnumerable<T> Data { get; set; }
    public PaginationMetadataDto Meta { get; set; }

    public PagedResultDto(IEnumerable<T> data, int totalItems, int currentPage, int pageSize)
    {
        Data = data;
        Meta = new PaginationMetadataDto
        {
            TotalItems = totalItems,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }
}
