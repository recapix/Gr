using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GR.Web.Models;
using GR.Data.Entities;
using GR.Data.Repository;
using GR.Data.UnityOfWork;

namespace GR.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork; 
        private IRepository<Author> repoAuthor;
        private IRepository<Book> repoBook;
        public AuthorController(IUnityOfWork UnityOfWork)
        {
            _UnityOfWork = UnityOfWork;
            this.repoAuthor = _UnityOfWork.AuthorsRepository;
            this.repoBook = _UnityOfWork.BooksRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<AuthorListingViewModel> model = new List<AuthorListingViewModel>();

            repoAuthor.GetAll().ToList().ForEach(a =>
            {
                AuthorListingViewModel author = new AuthorListingViewModel
                {
                    Id = a.Id,
                    Name = $"{a.FirstName} {a.LastName}",
                    Email = a.Email
                };
                author.TotalBooks = repoBook.GetAll().Count(x => x.AuthorId == a.Id);
                model.Add(author);
            });
            return View("Index", model);

        }

        [HttpGet]
        public PartialViewResult AddAuthor()
        {
            AuthorBookViewModel model = new AuthorBookViewModel();
            return PartialView("_AddAuthor", model);
        }

        [HttpPost]
        public ActionResult AddAuthor(AuthorBookViewModel model)
        {
            Author author = new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AddedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                ModifiedDate = DateTime.UtcNow,
                Books = new List<Book>
                {
                    new Book
                    {
                        Name = model.BookName,
                        ISBN= model.ISBN,
                        Publisher = model.Publisher,
                        IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                        AddedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow
                    }
                }
            };
            repoAuthor.Insert(author);
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditAuthor(Guid id)
        {
            AuthorViewModel model = new AuthorViewModel();
            Author author = repoAuthor.Get(id);
            if (author != null)
            {
                model.FirstName = author.FirstName;
                model.LastName = author.LastName;
                model.Email = author.Email;
            }
            return PartialView("_EditAuthor", model);
        }
        [HttpPost]
        public IActionResult EditAuthor(Guid id, AuthorViewModel model)
        {
            Author author = repoAuthor.Get(id);
            if (author != null)
            {
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
                author.Email = model.Email;
                author.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                author.ModifiedDate = DateTime.UtcNow;
                repoAuthor.Update(author);
                _UnityOfWork.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public PartialViewResult AddBook(Guid id)
        {
            BookViewModel model = new BookViewModel();
            return PartialView("_AddBook", model);
        }
        [HttpPost]
        public IActionResult AddBook(Guid id, BookViewModel model)
        {
            Book book = new Book
            {
                AuthorId = id,
                Name = model.BookName,
                ISBN = model.ISBN,
                Publisher = model.Publisher,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };
            repoBook.Insert(book);
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult AllBooks(Guid id)
        {
            var model = new List<BookListingViewModel>();

            repoBook.Query(book => book.AuthorId == id)
                .ToList()
                .ForEach(b =>
                {
                    var book = new BookListingViewModel
                    {
                        Id = b.Id,
                        BookName = b.Name,
                        Publisher = b.Publisher,
                        ISBN = b.ISBN
                    };
                    model.Add(book);
                });

            return PartialView("_AllBooks", model);
        }

    }
}
