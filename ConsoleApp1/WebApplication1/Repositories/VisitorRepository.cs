using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

public interface IVisitorRepository
{
    List<Visitor> GetAll(string searchQuery);
    Visitor GetById(int id);
    void Add(Visitor visitor);
    void Update(Visitor visitor);
}

public class VisitorRepository : IVisitorRepository
{
    private static List<Visitor> _visitors = new List<Visitor>();
    private static int _passCounter = 1;

    public List<Visitor> GetAll(string searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return _visitors;

        return _visitors.Where(v =>
            v.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
            v.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
            v.PassNumber.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
            v.Company.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    public Visitor GetById(int id) => _visitors.FirstOrDefault(v => v.Id == id);

    public void Add(Visitor visitor)
    {
        visitor.Id = _visitors.Count > 0 ? _visitors.Max(v => v.Id) + 1 : 1;
        visitor.PassNumber = $"VP-{DateTime.Now.Year}-{_passCounter:D3}";
        _visitors.Add(visitor);
        _passCounter++;
    }

    public void Update(Visitor visitor)
    {
        var existing = GetById(visitor.Id);
        if (existing != null)
        {
            existing.FirstName = visitor.FirstName;
            existing.LastName = visitor.LastName;
            existing.Company = visitor.Company;
            existing.ContactNumber = visitor.ContactNumber;
            existing.PersonToVisit = visitor.PersonToVisit;
            existing.Department = visitor.Department;
            existing.Purpose = visitor.Purpose;
            existing.ValidIdPresented = visitor.ValidIdPresented;
            existing.Notes = visitor.Notes;
            existing.ExitDateTime = visitor.ExitDateTime;
            existing.Status = visitor.Status;
        }
    }
}