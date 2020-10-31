using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace PluseDemoProject.PagingComponent
{

    public interface IPagingList<T> : IPagingList, IEnumerable<T>
    {

    }
}
