using PluseDemoProject.PagingComponent;

namespace PluseDemoProject.Areas.ViewModel
{
    public class IndexModel
    {
        //public PagingList<UserModel> userModels { get; set; }
        //public string FilterKeyword { get; set; }
        //public string SortKeyword { get; set; }
        //public PagingList<UserModel> aUserModels { get; set; }
        public PagingList<UserModel> aUserModels { get; set; }
        public string stFilterKeyword { get; set; }
        public string stSortKeyword { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
