using ITI.LibrarySys.Core.Models;
using ITI.LibrarySys.EF;
using ITI.LibrarySys.Presentation;
using ITI.LibrarySys.Presentation.Filtration;
using ITI.LibrarySys.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ITI.LibrarySys.Presentation
{
    public class AuthorController : Controller
    {
        private Context context;
        private IConfiguration config;
        private UnitOfWork unitOfWork;
        public AuthorController(Context _context, IConfiguration _config, UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            context = _context;
            config = _config;
        }
        #region Author Index
        [Authorize(Roles = "Viewer, Editor")]
        [LogFiltration]
        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.Title = "Author Lists";
            var authors = unitOfWork.Authors.GetAll().ToPagedList(pageIndex, pageSize);
            return View(authors);
        }
        [HttpGet]
        public IActionResult PagedAuthor(int pageIndex = 1, int pageSize = 5)
        {
            var pagedAuthors = unitOfWork.Authors.GetAll().ToPagedList(pageIndex, pageSize);
            return PartialView("_PagedBook", pagedAuthors);
        }
        #endregion

        #region Create New Author
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AuthorModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                unitOfWork.Authors.Add(new Author { Name = model.Name });
                unitOfWork.Save();
                return View();
            }
        }
        #endregion

        #region Author Details
        [HttpGet]
        [Authorize(Roles = "Viewer, Editor")]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Author-Details";
            var author = unitOfWork.Authors.GetById(id);
            ViewBag.Books = author.Books;
            return View(author);
        }
        #endregion

        #region Author Edit
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public IActionResult Edit(int id)
        {
            var author = unitOfWork.Authors.GetById(id);
            var authorModel = new AuthorModel
            {
                Id = author.ID,
                Name = author.Name
            };
            return View(authorModel);
        }
        [HttpPost]
        public IActionResult Edit(AuthorModel model)
        {
            Author author = unitOfWork.Authors.GetById(model.Id);
            author.Name = model.Name;
            unitOfWork.Authors.Update(author);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Author
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public IActionResult Delete(int id)
        {
            var author = unitOfWork.Authors.GetById(id);
            return View(author);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            unitOfWork.Authors.Delete(author);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #endregion 
    }
}
