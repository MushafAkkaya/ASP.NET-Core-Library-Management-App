using LibraryManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace LibraryManagementApp.Controllers
{
    public class BookController : Controller
    {
        // Kitapları statik olarak tanımlıyoruz
        private static List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Hobbit",
                AuthorId = 1,
                Genre = "Fantasy",
                PublishDate = new DateTime(1937, 9, 21),
                ISBN = "978-0547928227",
                CopiesAvailable = 5
            },
            new Book
            {
                Id = 2,
                Title = "The Fellowship of the Ring",
                AuthorId = 1,
                Genre = "Fantasy",
                PublishDate = new DateTime(1954, 7, 29),
                ISBN = "978-0547928210",
                CopiesAvailable = 3
            },
            new Book
            {
                Id = 3,
                Title = "The Two Towers",
                AuthorId = 1,
                Genre = "Fantasy",
                PublishDate = new DateTime(1954, 11, 11),
                ISBN = "978-0547928203",
                CopiesAvailable = 4
            },
            new Book
            {
                Id = 4,
                Title = "Harry Potter and the Philosopher's Stone",
                AuthorId = 2,
                Genre = "Fantasy",
                PublishDate = new DateTime(1997, 6, 26),
                ISBN = "978-0747532743",
                CopiesAvailable = 10
            },
            new Book
            {
                Id = 5,
                Title = "Harry Potter and the Chamber of Secrets",
                AuthorId = 2,
                Genre = "Fantasy",
                PublishDate = new DateTime(1998, 7, 2),
                ISBN = "978-0747538493",
                CopiesAvailable = 8
            },
            new Book
            {
                Id = 6,
                Title = "Harry Potter and the Prisoner of Azkaban",
                AuthorId = 2,
                Genre = "Fantasy",
                PublishDate = new DateTime(1999, 7, 8),
                ISBN = "978-0747542155",
                CopiesAvailable = 7
            },
            new Book
            {
                Id = 7,
                Title = "A Game of Thrones",
                AuthorId = 3,
                Genre = "Fantasy",
                PublishDate = new DateTime(1996, 8, 1),
                ISBN = "978-0553103540",
                CopiesAvailable = 6
            },
            new Book
            {
                Id = 8,
                Title = "A Clash of Kings",
                AuthorId = 3,
                Genre = "Fantasy",
                PublishDate = new DateTime(1998, 11, 16),
                ISBN = "978-0553108033",
                CopiesAvailable = 5
            },
            new Book
            {
                Id = 9,
                Title = "A Storm of Swords",
                AuthorId = 3,
                Genre = "Fantasy",
                PublishDate = new DateTime(2000, 8, 8),
                ISBN = "978-0553106633",
                CopiesAvailable = 4
            },
            new Book
            {
                Id = 10,
                Title = "The Silmarillion",
                AuthorId = 1,
                Genre = "Fantasy",
                PublishDate = new DateTime(1977, 9, 15),
                ISBN = "978-0618391110",
                CopiesAvailable = 2
            }
        };
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

        // GET: BookController
        // Kitap listeler
        [HttpGet]
        public IActionResult List()
        {
            //Listedeki kitapları teker teker seçip viewmodel listesine döndürerek view'a gönderir.
            var bookListViewModels = _books.Select(x => new BookListViewModel
            {
                Id = x.Id,
                Title = x.Title,
                AuthorName = _authors.FirstOrDefault(a => a.Id == x.AuthorId)?.FirstName + " " + _authors.FirstOrDefault(a => a.Id == x.AuthorId)?.LastName,
                Genre = x.Genre,
                PublishDate = x.PublishDate,
                ISBN = x.ISBN,
                CopiesAvailable = x.CopiesAvailable
            }).ToList();

            return View(bookListViewModels);
        }

        // GET: BookController/Details/5
        // Kitap detaylarını gösterir
        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id); //Detay sayfası için seçilen kitabın gelen id değerini listede karşılık gelen id değeriyle eşliyor ve listeyi bu değere atıyor.
            if (book == null)
                return NotFound(); //Eşleşen id bulunamazsa not found hatası döndürür.

            var author = _authors.FirstOrDefault(a => a.Id == book.AuthorId); //Seçilen kitabın içinde bulunan authorId ile yazar bilgileri de yakalanıyor.

            var bookDetailViewModel = new BookListViewModel //Kitap bilgileri ilgili view model ile view'e gönderiliyor.
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = author?.FirstName + " " + author?.LastName,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable
            };

            return View(bookDetailViewModel);
        }

        // GET: BookController/Create
        // Yeni kitap ekleme formunu gösterir
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        //Yeni kitap oluşturur.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAddViewModel bookAddViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    // Yazarın FullName'ini oluşturur
                    string fullName = $"{bookAddViewModel.AuthorFirstName} {bookAddViewModel.AuthorLastName}";

                    // Yazarın listede olup olmadığını kontrol eder
                    var existingAuthor = _authors.FirstOrDefault(a =>
                        $"{a.FirstName} {a.LastName}".Equals(fullName, StringComparison.OrdinalIgnoreCase));

                    if (existingAuthor != null)
                    {
                        // Yazar listede varsa, kitabın AuthorId'sini bu yazarın Id'si ile eşitler
                        bookAddViewModel.AuthorId = existingAuthor.Id;
                    }
                    else
                    {
                        // Yazar listede yoksa, yeni bir yazar oluşturur
                        var newAuthor = new Author
                        {
                            Id = _authors.Count + 1, // Yeni bir Id oluştur
                            FirstName = bookAddViewModel.AuthorFirstName,
                            LastName = bookAddViewModel.AuthorLastName,
                            DateOfBirth = bookAddViewModel.AuthorDateOfBirth
                        };

                        // Yeni yazarı listeye ekler
                        _authors.Add(newAuthor);

                        // Kitabın AuthorId'sini yeni yazarın Id'si ile eşitle
                        bookAddViewModel.AuthorId = newAuthor.Id;
                    }

                    // Kitabın Id'sini oluşturur ve listeye ekler
                    var newBook = new Book
                    {
                        Id = _books.Max(x => x.Id) + 1,
                        Title = bookAddViewModel.Title,
                        AuthorId = bookAddViewModel.AuthorId,
                        Genre = bookAddViewModel.Genre,
                        PublishDate = bookAddViewModel.PublishDate,
                        ISBN = bookAddViewModel.ISBN,
                        CopiesAvailable = bookAddViewModel.CopiesAvailable,
                    };

                    _books.Add(newBook);

                    return RedirectToAction("List");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id); // İlgili kitabı id ile bulur.
            if (book == null)
                return NotFound(); // Olmaması durumunda hata döner

            var bookEditViewModel = new BookEditViewModel //Seçilen kitabı view model ile geri döndürür
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable
            };

            return View(bookEditViewModel);
        }

        // POST: BookController/Edit/5
        // Kitapları düzenler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookEditViewModel bookEditViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    var existingBook = _books.FirstOrDefault(x => x.Id == bookEditViewModel.Id);
                    if (existingBook == null)
                        return NotFound();

                    // BookEditViewModel'i Book modeline dönüştür
                    existingBook.Title = bookEditViewModel.Title;
                    existingBook.AuthorId = bookEditViewModel.AuthorId;
                    existingBook.Genre = bookEditViewModel.Genre;
                    existingBook.PublishDate = bookEditViewModel.PublishDate;
                    existingBook.ISBN = bookEditViewModel.ISBN;
                    existingBook.CopiesAvailable = bookEditViewModel.CopiesAvailable;

                    return RedirectToAction("List");
                }

            }
            catch
            {
                return View();
            }
        }

        // POST: BookController/Delete/5
        // Kitapları siler
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var bookToDelete = _books.FirstOrDefault(x => x.Id == id);
            if (bookToDelete == null)
            {
                return NotFound(); // Kitap bulunamazsa 404 hatası döndür.
            }

            _books.Remove(bookToDelete); // Kitabı listeden kaldırır
            return RedirectToAction("List"); // List sayfasına yönlendirir.
        }
    }
}
