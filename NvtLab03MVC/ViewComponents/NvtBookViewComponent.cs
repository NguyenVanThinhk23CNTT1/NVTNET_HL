using Microsoft.AspNetCore.Mvc;
using NvtLab03MVC.Models;
using System.Threading.Tasks;

namespace NvtLab03MVC.ViewComponents
{
    public class NvtBookViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy dữ liệu sách từ Model gửi sang View của Component
            var books = NvtBook.GetBookList();
            return View("NvtDefault", books);
        }
    }
}