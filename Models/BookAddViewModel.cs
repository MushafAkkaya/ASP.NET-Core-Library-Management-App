using System.ComponentModel.DataAnnotations;

namespace LibraryManagementApp.Models
{
    public class BookAddViewModel
    {
        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string Title { get; set; }
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }

        //Yazar adı ve soyadı kontrolleri için geçici özellikler
        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string AuthorFirstName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz!")]
        public string AuthorLastName { get; set; }
        public DateTime AuthorDateOfBirth { get; set; }
    }
}
