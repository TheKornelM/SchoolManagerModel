namespace SchoolManagerModel.DTOs;

public class GetSubjectDto
{
    public GetSubjectDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}