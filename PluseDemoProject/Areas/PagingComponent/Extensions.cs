using Microsoft.Extensions.DependencyInjection;
using System;

namespace PluseDemoProject.PagingComponent
{

    public static class Extensions {
        public static void AddPaging(this IServiceCollection services, Action<PagingOptions> configureOptions) {
            configureOptions(PagingOptions.Current);
        }

    }
}