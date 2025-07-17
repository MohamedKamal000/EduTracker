using EduTracker.Models.Data;
using EFCore.BulkExtensions;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace ExcelReader;

public class InsertBulkData
{
    private int BatchSize { get; init; }
    private int TableRowsCount { get; init; }

    public InsertBulkData(int batchSize, int tableRowsCount)
    {
        BatchSize = batchSize;
        TableRowsCount = tableRowsCount;
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }


    public void InsertData()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        options.UseMySql("",
            new MySqlServerVersion(new Version(8, 0, 42)));
        using var context = new ApplicationDbContext(options.Options);

        var numberOfInserts = Math.Ceiling((double)TableRowsCount / BatchSize);
        for (int i = 0; i < numberOfInserts; i++)
        {
            context.BulkInsert(GetStudents(i));
            Console.Clear();
        }

    }


    private List<Student> GetStudents(int currentBatch)
    {
        using var stream = File.Open("D:\\نتيجة_الثانوية_24.xlsx", FileMode.Open, FileAccess.Read);
        using var reader = ExcelReaderFactory.CreateReader(stream);

        var config = new ExcelDataSetConfiguration
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        };

        var dataset = reader.AsDataSet(config);
        var table = dataset.Tables[0];

        List<Student> students = new List<Student>();

        var rows = table.Rows;
        var rowsCount = rows.Count;

        for (int i = 1 + BatchSize * currentBatch;
             i < BatchSize * currentBatch + BatchSize && i < rowsCount;
             i++)
        {
            var row = rows[i];
            students.Add(new Student()
            {
                StudentId = row[0].ToString(),
                StudentName = row[1].ToString(),
                TotalGrade = Convert.ToDecimal(row[2]),
                StudentState = row[3].ToString()
            });
        }

        Console.WriteLine($"Finished Reading {currentBatch} Batch");
        return students;
    }
}