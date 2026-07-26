using Microsoft.AspNetCore.Mvc;
using NvtLab04MVC.Models;
using System;
using System.Linq;

namespace NvtLab04MVC.Controllers
{
    public class NvtMemberController : Controller
    {
        // 1. DANH SÁCH THÀNH VIÊN
        public IActionResult NvtIndex()
        {
            return View(NvtMember.members);
        }

        // 2. THÊM MỚI (GET)
        public IActionResult NvtCreate()
        {
            var newMember = new NvtMember
            {
                MemberId = Guid.NewGuid().ToString() // Tự động tạo Mã GUID ngẫu nhiên giống trong ảnh
            };
            return View(newMember);
        }

        // 2. THÊM MỚI (POST)
        [HttpPost]
        public IActionResult NvtCreate(NvtMember member)
        {
            NvtMember.members.Add(member);
            return RedirectToAction("NvtIndex");
        }

        // 3. CHỈNH SỬA (GET)
        public IActionResult NvtEdit(string id)
        {
            var member = NvtMember.members.FirstOrDefault(m => m.MemberId == id);
            return View(member);
        }

        // 3. CHỈNH SỬA (POST)
        [HttpPost]
        public IActionResult NvtEdit(NvtMember member)
        {
            var existMember = NvtMember.members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if (existMember != null)
            {
                existMember.Username = member.Username;
                existMember.Fullname = member.Fullname;
                existMember.Password = member.Password;
                existMember.Email = member.Email;
            }
            return RedirectToAction("NvtIndex");
        }

        // 4. XÓA THÀNH VIÊN
        public IActionResult NvtDelete(string id)
        {
            var member = NvtMember.members.FirstOrDefault(m => m.MemberId == id);
            if (member != null)
            {
                NvtMember.members.Remove(member);
            }
            return RedirectToAction("NvtIndex");
        }
    }
}