using Microsoft.AspNetCore.Mvc;
using MvcCoreTutorial.Models.Domain;

namespace MvcCoreTutorial.Controllers
{
    public class PersonController : Controller
    {

        private readonly DatabaseContext _ctx;

        public PersonController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.View1 = "Hello Iam View Bag";
            ViewData["view2"] = "Hi Iam from View Data";
            TempData["view3"] = "Iam from Temp Data";

            return View();
        }
        //get method
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.person.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "Added Successfully!!!!";
                return RedirectToAction("AddPerson");

            }
            catch(Exception ex)
            {
                TempData["msg"] = "Could Not Added!!!!";
                return View();
            }
         
        }


        public IActionResult DisplayPersons()
        {
            var persons = _ctx.person.ToList();
            return View(persons);
        }

        public IActionResult EditPerson(int id)
        {
            var person = _ctx.person.Find(id);
            return View(person);
        }
        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.person.Update(person);
                _ctx.SaveChanges();
                TempData["msg"] = "Updated Successfully!!!!";
                return RedirectToAction("DisplayPersons");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Update!!!!";
                return View();
            }

        }


        public IActionResult DeletePerson(int id)
        {
            
            try
            {
                var person = _ctx.person.Find(id);
                if (person != null)
                {
                    _ctx.Remove(person);
                    _ctx.SaveChanges();
                }

            }
            catch (Exception ex)
            {
              
            }
            return RedirectToAction("DisplayPersons");

        }

    }
}
