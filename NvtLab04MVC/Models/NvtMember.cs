using System;
using System.Collections.Generic;

namespace NvtLab04MVC.Models
{
    public class NvtMember
    {
        public string MemberId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Danh sách dữ liệu mẫu giống hệt trong ảnh slide B7
        public static List<NvtMember> members = new List<NvtMember>
        {
            new NvtMember { MemberId = "d9df962a-217c-4a92-bca7-e528c4f2ed78", Username = "member1", Fullname = "Thành viên 1", Password = "123456", Email = "tv1@gmail.com" },
            new NvtMember { MemberId = "a23a69d8-6b60-4c58-9654-a087a1a16e6e", Username = "member2", Fullname = "Thành viên 2", Password = "123456", Email = "tv2@gmail.com" },
            new NvtMember { MemberId = "28f922e5-9324-492b-8fbd-c1ebc8000306", Username = "member3", Fullname = "Thành viên 3", Password = "123456", Email = "tv3@gmail.com" },
            new NvtMember { MemberId = "294ec5a0-e8e7-4b5e-8fa8-41b6611f975f", Username = "member4", Fullname = "Thành viên 4", Password = "123456", Email = "tv4@gmail.com" },
            new NvtMember { MemberId = "58234e0b-6222-4a3d-ac7c-c024afbc4ca1", Username = "member5", Fullname = "Thành viên 5", Password = "123456", Email = "tv5@gmail.com" },
            new NvtMember { MemberId = "bcb50cad-c64e-4386-b67e-5de8cba85bbf", Username = "chungtv", Fullname = "Trịnh Văn Chung", Password = "******", Email = "trinhvanchung.devmaster@gmail.com" }
        };
    }
}