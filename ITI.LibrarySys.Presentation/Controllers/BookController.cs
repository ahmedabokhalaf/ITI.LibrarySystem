using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using ITI.LibrarySys.EF;
using Microsoft.Extensions.Configuration;
using ITI.LibrarySys.Presentation.Filtration;
using ITI.LibrarySys.Core.Models;
using Microsoft.AspNetCore.Http;
using ITI.LibrarySys.Presentation.Models;

namespace ITI.LibrarySys.Presentation
{
    [Authorize]
    public class BookController : Controller
    {
        private Context context;
        private IConfiguration config;
        private UnitOfWork unitOfWork;
        public BookController(Context _context, IConfiguration _config, UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            context = _context;
            config= _config;
        }

        #region New Version
        #region Book Index
        [Authorize(Roles = "Viewer, Editor")]
        [LogFiltration]
        public IActionResult Index(int pageIndex=1, int pageSize=5)
        {
            ViewBag.Title = "Book Lists";
            var books = unitOfWork.Books.GetAll().ToPagedList(pageIndex, pageSize);
            //var books = context.Books.ToPagedList(pageIndex, pageSize);
            return View(books);
        }
        [HttpGet]
        public IActionResult PagedBook(int pageIndex = 1, int pageSize= 5)
        {
            var pagedBooks = unitOfWork.Books.GetAll().ToPagedList(pageIndex, pageSize);
            return PartialView("_PagedBook", pagedBooks);
        }
        #endregion

        #region Create New Book
        [HttpGet]
        [Authorize(Roles ="Editor")]
        public IActionResult Create()
        {
            ViewBag.Authors = context.Authors
                .Select(a => new SelectListItem(a.Name, a.ID.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookModel model)
        {
            List<BookImages> Images = new List<BookImages>();
            if (ModelState.IsValid == false)
            {
                ViewBag.Authors = context.Authors
                    .Select(a => new SelectListItem(a.Name, a.ID.ToString()));
                //ViewBag.Done = false;
                return View();
            }
            else
            {
                //to load file form PC to the App
                foreach (IFormFile file in model.Images)
                {
                    string FileName = Guid.NewGuid().ToString() + file.FileName;//to add file without any errors
                    Images.Add(new BookImages { Path = FileName });//for insert the file name in the BookImages Table
                    FileStream f = new FileStream(
                        Path.Combine(Directory.GetCurrentDirectory(), "Content", "Images", FileName),
                        FileMode.OpenOrCreate, FileAccess.ReadWrite);//to load file in stream
                    file.CopyTo(f);//to copy stream in the file
                    f.Position = 0;//to tell stream that it finished
                }
                unitOfWork.Books.Add(new Book
                {
                    Title = model.Title,
                    Discription = model.Description,
                    AuthorID = model.AuthorID,
                    BookImages = Images
                });
                unitOfWork.Save();
                //ViewBag.Done = true;
                return View();
            }
        }
        #endregion

        #region Book Details
        [HttpGet]
        [Authorize(Roles = "Viewer, Editor")]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Book-Details";
            var book = unitOfWork.Books.GetById(id);
            ViewBag.Images = config.GetSection("Images").Value.ToString();
            return View(book);
        }
        #endregion

        #region Book Edit
        [HttpGet]
        [Authorize(Roles ="Editor")]
        public IActionResult Edit(int id)
        {
            ViewBag.Authors = context.Authors
                .Select(a => new SelectListItem(a.Name, a.ID.ToString()));
            var book = unitOfWork.Books.GetById(id);
            var bookModel = new BookModel
            {
                Id = book.ID,
                Title = book.Title,
                AuthorID = book.AuthorID,
                Description = book.Discription,
            };
            return View(bookModel);
        }
        [HttpPost]
        public IActionResult Edit(BookModel model)
        {
            List<BookImages> Images = new List<BookImages>();
            //to load file form PC to the App
            foreach (IFormFile file in model.Images)
            {
                string FileName = Guid.NewGuid().ToString() + file.FileName;//to add file without any errors
                Images.Add(new BookImages { Path = FileName });//for insert the file name in the BookImages Table
                FileStream f = new FileStream(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content", "Images", FileName),
                    FileMode.OpenOrCreate, FileAccess.ReadWrite);//to load file in stream
                file.CopyTo(f);//to copy stream in the file
                f.Position = 0;//to tell stream that it finished
            }
            Book book = unitOfWork.Books.GetById(model.Id);
            book.Title = model.Title;
            book.AuthorID = model.AuthorID;
            book.Discription = model.Description;
            book.BookImages = Images;
            unitOfWork.Books.Update(book);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Book
        [HttpGet]
        [Authorize(Roles ="Editor")]
        public IActionResult Delete(int id)
        {
            var book = unitOfWork.Books.GetById(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            unitOfWork.Books.Delete(book);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #endregion 
        #endregion

        #region Old Version
        //#region Book Index
        //public IActionResult Index()
        //{
        //    ViewBag.Title = "Book Lists";
        //    List<Book> books = new List<Book>();
        //    books = context.Books.ToList();
        //    return View(books);
        //}
        //#endregion

        //#region Book Details
        //public IActionResult Details(int id)
        //{
        //    ViewBag.Title = "Book-Details";
        //    var book = context.Books.FirstOrDefault(b => b.ID == id);
        //    return View(book);
        //}
        //#endregion

        //#region Create Book
        //public ActionResult Create()
        //{
        //    List<Author> authors = new List<Author>();
        //    authors = context.Authors.ToList();
        //    return View(authors);
        //}
        //public ActionResult SaveBook()
        //{
        //    string title = Request.Form["bookTitle"];
        //    int author = int.Parse(Request.Form["bookAuthor"].ToString());
        //    string disc = Request.Form["bookDisc"];
        //    if (title != null && author != 0)
        //    {
        //        Book book = new Book();
        //        book.Title = title;
        //        book.AuthorID = author;
        //        book.Discription = disc;
        //        context.Books.Add(book);
        //        context.SaveChanges();
        //    }
        //    return Redirect("Index");
        //}
        //#endregion

        //#region Edit Book
        //public IActionResult Edit(int id)
        //{
        //    var book = context.Books.FirstOrDefault(b => b.ID == id);
        //    return View(book);
        //}

        //public IActionResult EditBook()
        //{
        //    int id = int.Parse(Request.Form["bookId"].ToString());
        //    string title = Request.Form["bookTitle"].ToString();
        //    string author = Request.Form["bookAuthor"].ToString();
        //    Book book = context.Books.FirstOrDefault(b => b.ID == id);
        //    if (title != null && author != null)
        //    {
        //        book.Title = title;
        //        book.Author.Name = author;
        //        context.Books.Update(book);
        //        context.SaveChanges();
        //    }
        //    return Redirect("Index");
        //}
        //#endregion

        //#region Delete Book
        //public IActionResult Delete(int id)
        //{
        //    var book = context.Books.FirstOrDefault(b => b.ID == id);
        //    return View(book);
        //}

        //public IActionResult DeleteBook()
        //{
        //    int id = int.Parse(Request.Form["bookId"].ToString());
        //    Book book = context.Books.FirstOrDefault(b => b.ID == id);
        //    context.Books.Remove(book);
        //    context.SaveChanges();
        //    return Redirect("Index");
        //}
        //#endregion 
        #endregion

    }
}
