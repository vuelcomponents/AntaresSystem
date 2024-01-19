using AntaresApi.Dto.Common;
using AntaresApi.Dto.Mail;
using AntaresApi.Models.Mail;
using AntaresApi.Models.StatusAction;

namespace AntaresApi.Services.Interfaces;

public interface IMailService
{
    IEnumerable<MailDto> GetAllMails();
    MailDto GetMailById(long id);
    MailDto CreateMail(MailDto actionFunction);
    MailDto UpdateMail(MailDto company);
    List<IdDto> DeleteMultipleMail(List<IdDto> companies);
    Task SendEmailFromActionFunction(Mail mail, string mailAddress, string name);
}