namespace CommentSold.WebTest.Dto
{
    /// <summary>
    /// Base class for types that specify filter parameters for queries; provides the paging filters.
    /// </summary>
    public class GetPagedListParameters
    {
        const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
