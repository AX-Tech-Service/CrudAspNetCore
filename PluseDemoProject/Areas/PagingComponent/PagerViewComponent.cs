using Microsoft.AspNetCore.Mvc;

namespace PluseDemoProject.PagingComponent
{
    [ViewComponent(Name = "Pager")]
    public class PagerViewComponent : ViewComponent {

        public IViewComponentResult Invoke(IPagingList pagingList) {
            return View(PagingOptions.Current.ViewName, pagingList);
        }
    }
}
