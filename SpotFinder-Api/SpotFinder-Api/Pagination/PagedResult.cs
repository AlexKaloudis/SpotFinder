namespace SpotFinder_Api.Pagination
{
    public class PagedResult<T>
    {
        public IList<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
