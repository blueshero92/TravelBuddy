namespace TravelBuddy.GCommon.Pagination
{
    //Paginated list is made generic to be able to use it for any type of data we want to paginate.
    public class PaginatedList<T> : List<T>
    {
        //The current page index.
        public int PageIndex { get; set; }

        //The total number of pages.
        public int TotalPages { get; set; }

        //Constructor to initialize the paginated list with items, total count, page index, and page size.
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        //Property to check if there is a previous page.
        public bool HasPreviousPage => PageIndex > 1;

        //Property to check if there is a next page.
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            List<T> items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }


    }
}
