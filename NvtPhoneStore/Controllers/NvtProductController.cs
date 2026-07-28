using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvtPhoneStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NvtPhoneStore.Controllers
{
    public class NvtProductController : Controller
    {
        private readonly NvtPhoneStoreDbContext _context;

        // Inject DbContext vào Controller
        public NvtProductController(NvtPhoneStoreDbContext context)
        {
            _context = context;
        }

        // 1. READ: Danh sách sản phẩm (Tích hợp Tìm kiếm, Lọc và Sắp xếp)
        public async Task<IActionResult> NvtIndex(string searchString, string category, string sortOrder)
        {
            // Khởi tạo Queryable
            var products = _context.NvtProducts.AsQueryable();

            // 1.1 TÌM KIẾM: Tìm theo tên sản phẩm
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.NvtProductName.Contains(searchString));
            }

            // 1.2 LỌC: Lọc theo Hãng/Danh mục
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.NvtCategory == category);
            }

            // 1.3 SẮP XẾP: Theo Giá bán
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(p => p.NvtPrice); // Giá giảm dần
                    break;
                default:
                    products = products.OrderBy(p => p.NvtPrice); // Giá tăng dần (mặc định)
                    break;
            }

            // 1.4 Lấy danh sách các Hãng hiện có trong CSDL để nạp vào Dropdown Filter
            ViewBag.Categories = await _context.NvtProducts
                                               .Select(p => p.NvtCategory)
                                               .Distinct()
                                               .ToListAsync();

            // Lưu trạng thái lọc/tìm kiếm hiện tại ra ViewData để hiển thị lại trên View
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentCategory"] = category;
            ViewData["CurrentSort"] = sortOrder;

            return View(await products.ToListAsync());
        }

        // 2. READ: Xem chi tiết 1 sản phẩm
        public async Task<IActionResult> NvtDetails(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.NvtProducts.FirstOrDefaultAsync(m => m.NvtProductId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // 3. CREATE: Hiển thị Form thêm mới (GET)
        public IActionResult NvtCreate()
        {
            return View();
        }

        // 3. CREATE: Lưu dữ liệu thêm mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NvtCreate(NvtProduct product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(NvtIndex));
            }
            return View(product);
        }

        // 4. UPDATE: Hiển thị Form chỉnh sửa (GET)
        public async Task<IActionResult> NvtEdit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.NvtProducts.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // 4. UPDATE: Lưu thông tin chỉnh sửa (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NvtEdit(int id, NvtProduct product)
        {
            if (id != product.NvtProductId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(NvtIndex));
            }
            return View(product);
        }

        // 5. DELETE: Thực hiện xóa sản phẩm
        public async Task<IActionResult> NvtDelete(int id)
        {
            var product = await _context.NvtProducts.FindAsync(id);
            if (product != null)
            {
                _context.NvtProducts.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(NvtIndex));
        }
    }
}