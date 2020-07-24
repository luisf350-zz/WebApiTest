namespace WebApiTest.API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        private const int MAX_PAGE_SIZE = 20;

        private int _pageSize = 10;

        public string MainCategory { get; set; }

        public string SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public string OrderBy { get; set; } = "Name";

    }
}
