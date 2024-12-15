namespace SchoolManagerModel.DTOs;

public class SelectSubjectDto(int id, string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public bool IsSelected { get; set; } = false;
}