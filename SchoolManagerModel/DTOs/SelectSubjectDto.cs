namespace SchoolManagerModel.DTOs;

public class SelectSubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
}