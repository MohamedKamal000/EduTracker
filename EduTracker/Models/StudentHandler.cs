using EduTracker.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace EduTracker.Models;

public class StudentHandler
{
    public enum SearchType
    {
        Name,
        Id,
        NULL
    }
    
    private readonly ApplicationDbContext _dbContext;

    public StudentHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StudentSearchDto>>  SearchBy(SearchType searchType, string? input)
    {
        if (input == null || searchType == SearchType.NULL)
        {
            return new List<StudentSearchDto>();
        }
        
        return searchType switch
        {
            SearchType.Name => await _dbContext.Students
                .Where(s => s.StudentName.StartsWith(input))
                .Select(s => new StudentSearchDto(){Id = s.Id,StudentId = s.StudentId, StudentName = s.StudentName})
                .AsNoTracking()
                .Take(50).ToListAsync(),
            SearchType.Id => await _dbContext.Students
                .Where(s => s.StudentId.StartsWith(input))
                .Select(s => new StudentSearchDto(){Id = s.Id,StudentId = s.StudentId, StudentName = s.StudentName})
                .AsNoTracking()
                .Take(50).ToListAsync(),
            _ => new List<StudentSearchDto>()
        };
    }

    public async Task<Student?> GetStudentById(int id)
    {
        return await _dbContext.Students.FindAsync(id);
    }
}