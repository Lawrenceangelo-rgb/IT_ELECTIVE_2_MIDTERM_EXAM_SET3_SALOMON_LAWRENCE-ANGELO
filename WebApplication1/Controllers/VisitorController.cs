using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

[Authorize]
public class VisitorController : Controller
{
    private readonly IVisitorRepository _visitorRepo;

    public VisitorController(IVisitorRepository visitorRepo)
    {
        _visitorRepo = visitorRepo;
    }

    public IActionResult Index(string searchString)
    {
        var visitors = _visitorRepo.GetAll(searchString);
        return View(visitors);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Visitor visitor)
    {
        if (ModelState.IsValid)
        {
            visitor.EntryDateTime = System.DateTime.Now;
            visitor.Status = "Inside Building";
            _visitorRepo.Add(visitor);
            return RedirectToAction("Index");
        }
        return View(visitor);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var visitor = _visitorRepo.GetById(id);
        if (visitor == null) return NotFound();
        return View(visitor);
    }

    [HttpPost]
    public IActionResult Edit(Visitor visitor)
    {
        if (ModelState.IsValid)
        {
            _visitorRepo.Update(visitor);
            return RedirectToAction("Index");
        }
        return View(visitor);
    }

    public IActionResult Details(int id)
    {
        var visitor = _visitorRepo.GetById(id);
        if (visitor == null) return NotFound();
        return View(visitor);
    }

    public IActionResult Exit(int id)
    {
        var visitor = _visitorRepo.GetById(id);
        if (visitor != null && visitor.Status == "Inside Building")
        {
            visitor.ExitDateTime = System.DateTime.Now;
            visitor.Status = "Left Building";
            _visitorRepo.Update(visitor);
        }
        return RedirectToAction("Index");
    }
}