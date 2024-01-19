using System.Net.Mail;
using AntaresApi.Dto.Common;
using AntaresApi.Dto.Mail;
using AntaresApi.Models.Document;
using AntaresApi.Models.Mail;
using AntaresApi.Models.StatusAction;
using AntaresApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AntaresApi.Services;

public class MailService : IMailService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public MailService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public IEnumerable<MailDto> GetAllMails()
    {
        List<Mail> list = _context.Mails
             .Include(m=>m.Documents)
            .ToList();
     
        return list.Select(_mapper.Map<MailDto>).ToList();
    }

    public MailDto GetMailById(long id)
    {
        Mail? mail = _context.Mails
            .Include(m=>m.Documents)
            .FirstOrDefault(c=> c.Id == id);
        if (mail == null)
        {
            throw new BadHttpRequestException($"No company #{id} has been found");
        }
        return _mapper.Map<MailDto>(mail);
    }

    public MailDto CreateMail(MailDto mail)
    {
        if (mail.Code.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Code must be fulfilled");
        }
        if (mail.Title.IsNullOrEmpty() || mail.Message.IsNullOrEmpty())
        {
            throw new BadHttpRequestException("Title and message cannot be empty");
        }
        Mail? dbMail = new Mail
        {
            Code = mail.Code!,
            Title = mail.Title!,
            Message = mail.Message!,
            Documents = new List<_Document>()
        };
        if (mail.Documents is { Count: > 0 })
        {
            foreach (var document in mail.Documents)
            {
                _Document? dbDocument = _context.Documents.FirstOrDefault(d => d.Id == document.Id);
                if (dbDocument == null)
                {
                    throw new BadHttpRequestException($"Document #{document.Id} has not been found");
                }
                dbMail.Documents.Add(dbDocument);
            }
        }

        _context.Mails.Add(dbMail);
        _context.SaveChanges();
        return _mapper.Map<MailDto>(dbMail);
    }

    public MailDto UpdateMail(MailDto mail)
    {
        Mail? dbMail = _context.Mails.FirstOrDefault(m => m.Id == mail.Id);
        if (dbMail == null)
        {
            throw new BadHttpRequestException($"Mail {mail.Id} has not been found");
        }

        if (!dbMail.Title.IsNullOrEmpty())
        {
            dbMail.Title = mail.Title!;
        }

        if (!dbMail.Message.IsNullOrEmpty())
        {
            dbMail.Message = mail.Message!;
        }

        if (!dbMail.Code.IsNullOrEmpty())
        {
            dbMail.Code = mail.Code!;
        }
        _context.SaveChangesAsync();
        return _mapper.Map<MailDto>(dbMail);
    }

    public List<IdDto> DeleteMultipleMail(List<IdDto> mails)
    {
        if(mails.Count >0)
        {
            foreach (var mail in mails)
            {
                Mail? dbMail = _context.Mails.Include(m=>m.Documents).FirstOrDefault(e => e.Id == mail.Id);
                if (dbMail != null)
                {
                    _context.Mails.Remove(dbMail);
                }
            }

            _context.SaveChanges();
        }
        return mails;
    }
    // SENDING
    public async Task SendEmailFromActionFunction(Mail mail, string emailAddress, string name)
    {
        
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Devsharks", "dzordzo221@gmail.com"));
            message.To.Add(new MailboxAddress(name, emailAddress));
            message.Subject = mail!.Title;

            var builder = new BodyBuilder();
            builder.HtmlBody = mail!.Message;
            if(mail.Documents is {Count:>0})
            {
                foreach (var attachment in mail!.Documents)
                {
                    var attachmentData = attachment.FileData; 
                    var stream = new MemoryStream(attachmentData);
                    
                    var attachmentPart = new MimePart("application", "octet-stream")
                    {
                        Content = new MimeContent(stream, ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachment.FileName 
                    };
                    
                    builder.Attachments.Add(attachmentPart);
                }
            }
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(
                    Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
                    Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"Błąd SMTP: {ex.Message}");
            throw; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            throw; 
        }
    }

}