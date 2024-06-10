using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuleManager.SqlServer.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Cryptography;

namespace ModuleManager.SqlServer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Where(u => u.UserRoles.All(ur => ur.Role.RoleName != "Admin"))
                .OrderByDescending(u => u.UserId)
                .ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null || user.UserRoles.Any(ur => ur.Role.RoleName == "Admin"))
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa người dùng thành công.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(int[] userIds)
        {
            var users = await _context.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToListAsync();

            foreach (var user in users)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa người dùng thành công.";

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
