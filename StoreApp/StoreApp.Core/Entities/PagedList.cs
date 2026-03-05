namespace StoreApp.Core.Entities
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }

        public PagedList(List<T> items, MetaData metaData)
        {
            Items = items;
            MetaData = metaData;
        }
    }
}
