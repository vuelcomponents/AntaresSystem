using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.Mail;

public class Mail
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public List<_Document>? Documents { get; set; }
    
}