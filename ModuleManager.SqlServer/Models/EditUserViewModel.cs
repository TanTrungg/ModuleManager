using System.ComponentModel.DataAnnotations;

namespace ModuleManager.SqlServer.Models
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Hình ảnh hiện tại")]
        public string ImageUrl { get; set; }

        [Display(Name = "Hình ảnh mới")]
        public IFormFile ImageFile { get; set; }
    }
}
