using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace PluseDemoProject.PagingComponent
{

    public class PagingList<T> : List<T>, IPagingList<T> where T : class {
        public int PageIndex { get; }
        public int PageCount { get; }
        public int TotalRecordCount { get; }
        public string Action { get; set; }
        public string PageParameterName { get; set; }

        public PagingList(List<T> list, int pageIndex, int pageSize, int totalRecordCount)
            : base(list) {
            TotalRecordCount = totalRecordCount;
            PageIndex = pageIndex;
            PageCount = (int)Math.Ceiling(totalRecordCount / (double)pageSize);
            Action = "Index";
            PageParameterName = PagingOptions.Current.PageParameterName;}

        public RouteValueDictionary RouteValue { get; set; }

        public RouteValueDictionary GetRouteValueForPage(int pageIndex) {

            var dict = this.RouteValue == null ? new RouteValueDictionary() :
                                                 new RouteValueDictionary(this.RouteValue);

            dict[this.PageParameterName] = pageIndex;

            return dict;
        }

        public int NumberOfPagesToShow { get; set; } = PagingOptions.Current.DefaultNumberOfPagesToShow;

        public int StartPageIndex {
            get {
                var half = (int)((this.NumberOfPagesToShow - 0.5) / 2);
                var start = Math.Max(1, this.PageIndex - half);
                if (start + this.NumberOfPagesToShow - 1 > this.PageCount) {
                    start = this.PageCount - this.NumberOfPagesToShow + 1;
                }
                return Math.Max(1, start);
            }
        }

        public int StopPageIndex => Math.Min(this.PageCount, this.StartPageIndex + this.NumberOfPagesToShow - 1);

    }
}