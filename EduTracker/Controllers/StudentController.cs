using System.Text.Json;
using EduTracker.Models;
using EduTracker.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTracker.Controllers;

public class StudentController : Controller
{
    private readonly StudentHandler _handler;
    
    public StudentController(StudentHandler handler)
    {
        _handler = handler;
    }
    
    public IActionResult Search()
    {
        return View();
    }

    public async Task<IActionResult> Details([FromRoute]int id)
    {
        var dbStd = await _handler.GetStudentById(id);

        if (dbStd == null)
        {
            return View("StudentNotFound");
        }


        var student = dbStd;

        
        return View(student);
    }

    public async Task<IActionResult> SubmitSearch()
    {
        var searchType = Request.Query["SearchType"][0];
        var input = Request.Query["Input"][0];

        var students = await _handler.SearchBy(SerializeSearchType(searchType), input);
        
        return  Json(students);
    }

    private StudentHandler.SearchType SerializeSearchType(string? input)
    {
        return input switch
        {
            "name" => StudentHandler.SearchType.Name,
            "id" => StudentHandler.SearchType.Id,
            _ => StudentHandler.SearchType.NULL
        };
    }
}