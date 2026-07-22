using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NvtLab03MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace NvtLab03MVC.Controllers
{
    public class NvtBookController : Controller
    {
        // Danh sách chọn Tác giả & Thể loại dùng chung
        private List<SelectListItem> GetAuthors() => new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Nam Cao" },
            new SelectListItem { Value = "2", Text = "Gosho Aoyama" },
            new SelectListItem { Value = "3", Text = "Thích Nhất Hạnh" }
        };

        private List<SelectListItem> GetGenres() => new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Văn học" },
            new SelectListItem { Value = "2", Text = "Truyện tranh" },
            new SelectListItem { Value = "3", Text = "Tôn giáo / Triết học" }
        };

        public IActionResult Index(int? authorId, int? genreId)
        {
            var books = NvtBook.GetBookList();

            if (authorId.HasValue && authorId > 0)
                books = books.Where(b => b.AuthorId == authorId.Value).ToList();

            if (genreId.HasValue && genreId > 0)
                books = books.Where(b => b.GenreId == genreId.Value).ToList();

            // Gán danh sách cho ComboBox
            ViewBag.Authors = GetAuthors();
            ViewBag.Genres = GetGenres();

            // LƯU Ý BỔ SUNG: Giữ lại giá trị đang chọn trên ComboBox
            ViewBag.SelectedAuthor = authorId;
            ViewBag.SelectedGenre = genreId;

            return View(books);
        }

        // Action GET: Chỉnh sửa sách theo Id (Bước 15)
        public IActionResult Edit(int id)
        {
            var book = NvtBook.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Authors = GetAuthors();
            book.Genres = GetGenres();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(NvtBook book)
        {
            if (ModelState.IsValid)
            {
                // Xử lý lưu lại dữ liệu ở đây
                return RedirectToAction("Index");
            }
            book.Authors = GetAuthors();
            book.Genres = GetGenres();
            return View(book);
        }

        // Action trả về PartialView chứa danh sách sách nổi bật cho Ajax (Bài 3)
        public IActionResult NvtPopularBook()
        {
            // Lấy 2 cuốn sách đầu tiên làm danh sách nổi bật
            var popularBooks = NvtBook.GetBookList().Take(2).ToList();
            return PartialView("NvtPopularBook", popularBooks);
        }
    }
}