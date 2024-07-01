

namespace courseProject.Repository.GenericRepository
{
    public class Pagination<T> 
    {

        public IReadOnlyList<T> Items { get; set; }


        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 3;
        public static int maxPageSize { get; set; } = 15;
        public int TotalCount { get; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber * PageSize < TotalCount;

       

        public  Pagination(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount , int totalPages)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        // Creates a paginated result from a given data set.
        public static async Task<Pagination<T>> CreateAsync(IReadOnlyList<T> data, int? pageNumber=null , int? pageSize=null , int defaultPageNumber = 1, int defaultPageSize = 10 )
        {
            // Get the total count of items in the data set
            var totalCount = data.Count();

            // Determine the page number to use, defaulting to the provided default if null
            var page = pageNumber ?? defaultPageNumber;

            // Determine the page size to use, defaulting to the provided default if null
            var size = pageSize ?? defaultPageSize;

            // Ensure the page size does not exceed the maximum allowed page size
            size = size> maxPageSize ? maxPageSize : size;

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)size);

            // Get the items for the current page
            var items = data.Skip((page - 1) * size).Take(size).ToList();

            // Return a new Pagination object with the paginated data and metadata
            return new Pagination<T>(items, page, size, totalCount , totalPages);
        }


        

       

    }
}
