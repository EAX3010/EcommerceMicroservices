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
        public int PageIndex { get; }
        public int PageSize { get; }
        public long TotalCount { get; }
        public IEnumerable<IEntity> Items { get; }
    }
}
