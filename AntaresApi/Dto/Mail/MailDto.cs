using AntaresApi.Dto.Document;

namespace AntaresApi.Dto.Mail;

public class MailDto
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public List<_DocumentDto>? Documents { get; set; }
}