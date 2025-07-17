namespace EduTracker.Models.Data;

public class Student
{
    public int Id { get; set; }
    
    public string StudentId { get; set; }
    
    public decimal TotalGrade { get; set; }

    public string StudentState { get; set; } = null!;

    public string StudentName { get; set; } = null!;


    public override string ToString()
    {
        return
            $"StudentName {StudentName}\n Student Id {StudentId}\n TotalGrade {TotalGrade}\n StudentState {StudentState}\n";
    }
}