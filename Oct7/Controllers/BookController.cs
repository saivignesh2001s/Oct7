using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oct7.Models;
using Hiding;
using Businesslogic;

namespace Oct7.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        habook nu = null;
     
       public BookController()
        {

            nu = new habook();
        }

        public List<Bookmodel> ts = new List<Bookmodel>();
        public List<Bookmodel> sd2()
        {
            List<Books> st = nu.booli();


            foreach (var item in st)
            {
                ts.Add(
                    new Bookmodel
                    {
                        BookId = item.BookId,
                        Bookname = item.Bookname,
                        Author = item.Author,
                        Price = item.Price,
                        Ctegory = item.Ctegory

                    });
            }
            return ts;
        }
public ActionResult Index()
        {
            List<Bookmodel> tst=sd2().ToList();
            return View(tst);
        }
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(FormCollection c)
        {
            try
            {
                Books b = new Books();
                b.BookId = Convert.ToInt32(Request["BookId"]);
                b.Bookname = Request["Bookname"];
                b.Author = Request["Author"];
                b.Price = Convert.ToDouble(Request["Price"]);
                b.Ctegory = Request["Ctegory"];
                bool ans = nu.insert(b);
                if (ans)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }


            }
            catch (Exception ex)
            {
                throw new Exception("It is npt valid");
            }
        }
            public ActionResult EditDetails(int id)
            {
            List<Bookmodel> sts=sd2().ToList();
             Bookmodel m = sts.Find(m1 => m1.BookId == id);
               return View(m);
           

            }
        [HttpPost]
        public ActionResult EditDetails(int id,Bookmodel m)
        {
            try { 

                Books b = new Books();
                b.BookId = m.BookId;
                b.Bookname = m.Bookname;
                b.Author = m.Author;
                b.Price = m.Price;
                b.Ctegory = m.Ctegory;

                bool ans = nu.updatebook(b,id);
                if (ans)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
           catch(Exception ex)
            {
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            List<Bookmodel> rt = sd2().ToList();
            Bookmodel hu = rt.Find(m => m.BookId == id);
            return View(hu);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                bool k = nu.delete(id);

                return RedirectToAction("Index");
            }
            catch(Exception Ex)
            {
                return View();
            }
        }
      
        public ActionResult Find()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Find(string Search)
        {
            try
            {
                Bookmodel m1 = sd2().ToList().Find(s => s.Bookname == Search);
                TempData["prodata"] = m1;
                return RedirectToAction("Showdetail");
            }
            catch(Exception ex)
            {
                return Index();
            }
        }
        public ActionResult Showdetail()
        {
            var n1 = TempData["prodata"];
            return View(n1);

        }

        



    }

}
    
