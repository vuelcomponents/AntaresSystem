using System.ComponentModel.DataAnnotations;
using AntaresApi.Models.Document;

namespace AntaresApi.Models.StatusAction;

public class ActionFunction
{
    [Key]
    public long Id { get; set; }
    public string Code { get; set; }
    public StoreModel.StoreModel StoreModel { get; set; }
    public string Description { get; set; }
    public SystemFunction? SystemFunction { get; set; }
    public Company? Company { get; set; }
    public Position.Position? Position { get; set; }
    public List<Realisation>? Requirements { get; set; }
    public Mail.Mail? Mail { get; set; }

}