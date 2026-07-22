using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NvtLab03MVC.Models
{
    public class NvtBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int TotalPage { get; set; }
        public string Sumary { get; set; }

        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> Genres { get; set; }

        // Dữ liệu mẫu giống hệt trong ảnh giao diện
        public static List<NvtBook> GetBookList()
        {
            return new List<NvtBook>
            {
                new NvtBook { Id = 1, Title = "Chí Phèo", AuthorId = 1, GenreId = 1, Price = 500000, TotalPage = 250, Image = "chipheo.jpg", Sumary = "Tác phẩm Chí Phèo của Nam Cao" },
                new NvtBook { Id = 2, Title = "Lão Hạc", AuthorId = 1, GenreId = 1, Price = 700000, TotalPage = 400, Image = "laohac.jpg", Sumary = "Nội dung giới thiệu sách..." },
                new NvtBook { Id = 4, Title = "Conan Phiêu lưu ký", AuthorId = 2, GenreId = 2, Price = 550000, TotalPage = 180, Image = "conan.jpg", Sumary = "Truyện tranh Conan" },
                new NvtBook { Id = 6, Title = "Đường Xưa Mây Trắng", AuthorId = 3, GenreId = 3, Price = 850000, TotalPage = 500, Image = "duongxua.jpg", Sumary = "Tác phẩm Thích Nhất Hạnh" }
            };
        }

        public static NvtBook GetBookById(int id)
        {
            return GetBookList().Find(b => b.Id == id);
        }
    }
}