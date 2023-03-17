using DATA.DAL;
using DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATA.Controllers
{
    public class DataController : Controller
    {
        private readonly DataDAL _dal;
        public DataController(DataDAL dal)
        {
            _dal = dal; 
        }
        // GET: DataController
        public IActionResult Index()
        {
            List<Data> data = new List<Data>();
            data = _dal.GetAll();
            return View(data);
        }

        // GET: DataController/Details/5
        public ActionResult Details(int id)
        {
 
            return View();
        }

        // GET: DataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Data data)
        {
            try
            {
                bool result = _dal.Insert(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DataController/Edit/5
        public IActionResult Edit(int id)
        {
            Data data = _dal.GetById(id);
            return View(data);
        }

        // POST: DataController/Edit/5
        [HttpPost]
        public IActionResult Edit(Data data)
        {
            try
            {
                bool result = _dal.Update(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DataController/Delete/5
        public IActionResult Delete(int id)
        {
            Data data = _dal.GetById(id);
            return View(data);

        }

        // POST: DataController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Data data)
        {
            try
            {
                bool result = _dal.Delete(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
