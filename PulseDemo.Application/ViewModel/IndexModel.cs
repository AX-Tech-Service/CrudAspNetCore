using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PulseDemo.Application.ViewModel
{
    public class IndexModel
    {
        public PagingList<UserModel> userModels { get; set; } 
        public string FilterKeyword { get; set; }
        public string SortKeyword { get; set; }

    }
}
