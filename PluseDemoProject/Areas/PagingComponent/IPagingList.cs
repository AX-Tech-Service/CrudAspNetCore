using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluseDemoProject.PagingComponent
{
    public interface IPagingList
    {
        string Action { get; set; }
        RouteValueDictionary GetRouteValueForPage(int pageIndex);
        int PageCount { get; }
        int PageIndex { get; }
        int TotalRecordCount { get; }
        RouteValueDictionary RouteValue { get; set; }

        int NumberOfPagesToShow { get; set; }
        int StartPageIndex { get; }
        int StopPageIndex { get; }
    }
}
