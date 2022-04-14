namespace Framework.Core.Queries
{
    public abstract class SortableQueryFilter<T>
        where T : ISortablePropertyCollection
    {
        protected SortableQueryFilter(T sortablePropertyCollection)
        {
            SortDirection = SortDirection.Asc;
            SortPropertyName = sortablePropertyCollection.GetDefault();
        }

        public SortDirection SortDirection { get; set; }
        public string SortPropertyName { get; set; }
    }
}
