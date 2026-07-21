using Microsoft.AspNetCore.Mvc;
using NvtLession2MVC.Models;
using NvtLesson2MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NvtLesson2MVC.Controllers
{
    // Đặt route gốc cho cả Controller nếu bạn muốn URL chứa "ho-so-cua-toi"
    [Route("ho-so-cua-toi")]
    public class NvtAccountController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult NvtIndex()
        {
            List<NvtAccount> accounts = new List<NvtAccount>
            {
                new NvtAccount()
                {
                    Id = 1, // 👈 Đã sửa ID thành 1
                    Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/01.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new NvtAccount()
                {
                    Id = 2, // 👈 Đã sửa ID thành 2
                    Name = "Trường Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/02.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new NvtAccount()
                {
                    Id = 3, // 👈 Đã sửa ID thành 3
                    Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/03.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
            };

            ViewBag.Accounts = accounts;
            return View();
        }

        // Cấu hình route nhận thêm {id} để hiển thị đúng đường dẫn chi tiết của từng người
        [Route("chi-tiet/{id}")]
        public IActionResult NvtProfile(int id)
        {
            // Danh sách dữ liệu mẫu đầy đủ các ID
            List<NvtAccount> accounts = new List<NvtAccount>
            {
                new NvtAccount()
                {
                    Id = 1, Name = "Hoàng Anh", Email = "anh@gmail.com", Phone = "0986456789",
                    Address = "Hà Nội", Avatar = Url.Content("~/Avatar/01.jpg"),
                    Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998, 7, 15)
                },
                new NvtAccount()
                {
                    Id = 2, Name = "Trường Giang", Email = "giang@gmail.com", Phone = "0986456789",
                    Address = "Hà Nội", Avatar = Url.Content("~/Avatar/02.jpg"),
                    Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998, 7, 15)
                },
                new NvtAccount()
                {
                    Id = 3, Name = "Hoàng Thúy", Email = "thuy@gmail.com", Phone = "0986456789",
                    Address = "Hà Nội", Avatar = Url.Content("~/Avatar/03.jpg"),
                    Gender = 1, Bio = "My name is small", Birthday = new DateTime(1998, 7, 15)
                }
            };

            // Tìm kiếm account khớp với ID được truyền vào từ nút bấm
            var account = accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy ID
            }

            ViewBag.account = account;
            return View(account);
        }
    }
}