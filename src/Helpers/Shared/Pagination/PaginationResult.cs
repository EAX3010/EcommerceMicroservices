namespace Shared.Pagination
{
    public class PaginationResult<IEntity>
        where IEntity : class
    {
        public PaginationResult(int pageIndex, int pageSize, long totalCount, IEnumerable<IEntity> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<IEntity> Items { get; set; }
    }
}
