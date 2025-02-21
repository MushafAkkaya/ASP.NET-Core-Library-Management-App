using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Models
{
    public class AuthorAddViewModel
    {
        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
