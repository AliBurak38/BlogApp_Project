using NewBlogApp.Entitiy;
using System.ComponentModel.DataAnnotations;

namespace NewBlogApp.Models
{
    public class PostsCreateViewModel
    {

        public int PostId { get; set; } 

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur.")]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }
        
        public string? Image { get; set; }

        [Required(ErrorMessage = "Url zorunludur.")]
        [Display(Name = "Url")]
        public string? Url { get; set; }

        public DateTime PublishedOn { get; set; }

        public bool IsActive { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

    }
}
