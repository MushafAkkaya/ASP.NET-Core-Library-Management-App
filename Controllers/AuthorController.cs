using LibraryManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Controllers
{
    public class AuthorController : Controller
    {
        // Yazarları statik olarak tanımlıyoruz
        private static List<Author> _authors = new List<Author>
        {
            new Author
            {
                Id = 1,
                FirstName = "J.R.R.",
                LastName = "Tolkien",
                DateOfBirth = new DateTime(1892, 1, 3)
            },
            new Author
            {
                Id = 2,
                FirstName = "J.K.",
                LastName = "Rowling",
                DateOfBirth = new DateTime(1965, 7, 31)
            },
            new Author
            {
                Id = 3,
                FirstName = "George R.R.",
                LastName = "Martin",
                DateOfBirth = new DateTime(1948, 9, 20)
            }
        };

        //Yazarları listeler
        [HttpGet]
        public IActionResult List()
        {
            var authorViewModels = _authors.Select(a => new AuthorListViewModel // Listede bulunan bütün yazarları listeler.
            {
                Id = a.Id,
                FullName = a.FirstName + " " + a.LastName,
                DateOfBirth = a.DateOfBirth
            }).ToList();

            return View(authorViewModels);
        }

        [HttpGet]
        // GET: AuthorController/Details/5
        // Yazar detayları
        public IActionResult Details(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id); // Detayı gösterilecek yazarı bulur.
            if (author == null)
                return NotFound(); // Bulamazsa hata döner.

            var viewModel = new AuthorListViewModel // Yazarı ilgili view model ile döndürür.
            {
                Id = author.Id,
                FullName = $"{author.FirstName} {author.LastName}",
                DateOfBirth = author.DateOfBirth
            };

            return View(viewModel);
        }

        // GET: AuthorController/Create
        // Yeni yazar ekleme formunu gösterir
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        // Yeni yazar ekler.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorAddViewModel authorAddViewModel)
        {
            try
            {
                if (!ModelState.IsValid) // Boş bırakılmaması gereken alanları kontrol eder. Eğer boş bırakılmışsa işleme devam edilmez ve view geri döner.
                {
                    return View();
                }
                else
                {
                    var newAuthor = new Author // Id'yi 1 arttırarak yeni yazarı oluşturur.
                    {
                        Id = _authors.Max(x => x.Id) + 1,
                        FirstName = authorAddViewModel.FirstName,
                        LastName = authorAddViewModel.LastName,
                        DateOfBirth = authorAddViewModel.DateOfBirth
                    };

                    _authors.Add(newAuthor); // Yazarı listeye ekler.

                    return RedirectToAction("List"); // İşlem tamamlanınca liste sayfasına yönlendirir.
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        // Yazar düzenleme sayfasına gider
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id); // Düzenlenecek yazarı bulur ve değişkene atar.
            if (author == null)
                return NotFound(); // Bulunamazsa hata döner

            var authorEditViewModel = new AuthorEditViewModel // Seçilen yazarın bilgilerini view model ile geri döner
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth
            };

            return View(authorEditViewModel);
        }

        // POST: AuthorController/Edit/5
        // Yazar düzenler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorEditViewModel authorEditViewModel)
        {
            try
            {
                if (!ModelState.IsValid) // Boş bırakılmaması gereken alanları kontrol eder. Eğer boş bırakılmışsa işleme devam edilmez ve view geri döner.
                {
                    return View();
                }
                else
                {
                    var existingAuthor = _authors.FirstOrDefault(x => x.Id == authorEditViewModel.Id); // Düzenlenecek yazar'ın bilgilerini alır.
                    if (existingAuthor == null)
                        return NotFound(); // Bulamazsa hata döner

                    // Düzenlenmiş yazarı kaydeder.
                    existingAuthor.FirstName = authorEditViewModel.FirstName;
                    existingAuthor.LastName = authorEditViewModel.LastName;
                    existingAuthor.DateOfBirth = authorEditViewModel.DateOfBirth;

                    return RedirectToAction("List");
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: AuthorController/Delete/5
        // Yazarları siler
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var authorToDelete = _authors.FirstOrDefault(x => x.Id == id); // Silinecek yazar
            if (authorToDelete == null)
            {
                return NotFound(); // Bulunamazsa hata döner
            }

            _authors.Remove(authorToDelete); // Yazarı listeden kaldırır.
            return RedirectToAction("List"); // List sayfasına yönlendirir.
        }
    }
}
