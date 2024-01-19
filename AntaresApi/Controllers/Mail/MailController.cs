using AntaresApi.Dto.Common;
using AntaresApi.Dto.Mail;
using AntaresApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntaresApi.Controllers.Mail;

[ApiController]
[Route("api/mail")]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;

    public MailController(IMailService mail)
    {
        _mailService = mail;
    }
    [HttpGet("get")]
    public ActionResult<IEnumerable<MailDto>> GetAllMail()
    {
        try
        {
            return Ok(_mailService.GetAllMails());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("get/{id}")]
    public ActionResult<MailDto> GetMailById(long id)
    {
        try
        {
            return Ok(_mailService.GetMailById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    [HttpPost("create")]
    public ActionResult<MailDto> CreateMail(MailDto actionFunction)
    {
        try
        {
            return Ok(_mailService.CreateMail(actionFunction));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("update")]
    public ActionResult<MailDto> UpdateMail(MailDto actionFunction)
    {
        try
        {
            return Ok(_mailService.UpdateMail(actionFunction));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("delete-multiple")]
    public ActionResult<List<IdDto>> DeleteMultipleMail(List<IdDto> actionFunctions)
    {
        try
        {
            return Ok(_mailService.DeleteMultipleMail(actionFunctions));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
