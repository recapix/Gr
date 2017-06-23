using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GR.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using GR.Data.Entities;
using GR.Data.Repository;
using GR.Data.UnityOfWork;

namespace GR.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;
        private IRepository<Author> repoAuthor;
        private IRepository<Book> repoBook;
        public BookController(IUnityOfWork UnityOfWork)
        {
            _UnityOfWork = UnityOfWork;
            this.repoAuthor = _UnityOfWork.AuthorsRepository;
            this.repoBook = _UnityOfWork.BooksRepository;
        }

        public IActionResult Index()
        {
            var model = new List<BookListingViewModel>();
            repoBook.GetAll().ToList().ForEach(b =>
            {
                var book = new BookListingViewModel
                {
                    Id = b.Id,
                    BookName = b.Name,
                    Publisher = b.Publisher,
                    ISBN=b.ISBN
                };
                var author = repoAuthor.Get(b.AuthorId);
                book.AuthorName = $"{author.FirstName} {author.LastName}";
                model.Add(book);
                _UnityOfWork.SaveChanges();
            });
            return View("Index", model);
        }

        public PartialViewResult EditBook(Guid id)
        {
            var model = new EditBookViewModel()
            {
                Authors = repoAuthor.GetAll().Select(a => new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                }).ToList()
            };
            var book = repoBook.Get(id);
            if(book != null)
            {
                model.BookName = book.Name;
                model.ISBN = book.ISBN;
                model.Publisher = book.Publisher;
                model.AuthorId = book.AuthorId;
            }
            return PartialView("_EditBook",model);
        }
        [HttpPost]
        public ActionResult EditBook(Guid id, EditBookViewModel model)
        {
            var book = repoBook.Get(id);
            if (book != null)
            {
                book.Name = model.BookName;
                book.ISBN = model.ISBN;
                book.Publisher = model.Publisher;
                book.AuthorId = model.AuthorId;
                book.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                book.ModifiedDate = DateTime.UtcNow;
                repoBook.Update(book);
                _UnityOfWork.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult DeleteBook(Guid id)
        {
            Book book = repoBook.Get(id);            
            return PartialView("_DeleteBook",book?.Name);
        }
        [HttpPost]
        public ActionResult DeleteBook(Guid id, IFormCollection form)
        {
            Book book = repoBook.Get(id);
            if(book != null)
            {
                repoBook.Delete(book);
            }            
            return RedirectToAction("Index");
        }

    }
}
