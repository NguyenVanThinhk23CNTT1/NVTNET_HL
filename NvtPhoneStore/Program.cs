using Microsoft.EntityFrameworkCore;
using NvtPhoneStore.Models;

namespace NvtPhoneStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================================================
            // 1. ĐĂNG KÝ DỊCH VỤ (TẤT CẢ PHẢI NẰM TRÊN builder.Build())
            // =========================================================
            builder.Services.AddControllersWithViews();

            // Đăng ký DbContext ở đây (TRƯỚC builder.Build)
            builder.Services.AddDbContext<NvtPhoneStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NvtDbConnect")));


            // =========================================================
            // 2. TẠO APP (BUILD)
            // =========================================================
            var app = builder.Build(); // <-- Ranh giới đóng các dịch vụ lại


            // =========================================================
            // 3. CẤU HÌNH PIPELINE (MIDDLEWARE)
            // =========================================================
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=NvtProduct}/{action=NvtIndex}/{id?}"); // Đã chỉnh route mặc định vào NvtProduct

            app.Run();
        }
    }
}