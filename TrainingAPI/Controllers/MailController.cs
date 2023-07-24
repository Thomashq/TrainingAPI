using Microsoft.AspNetCore.Mvc;
using Utility;
using Utility.Models;

namespace TrainingAPI.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public MailController([FromServices] DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Mail>> AddMail(Mail mailToSend)
        {
            try
            {
                _dbContext.Mail.Add(mailToSend);

                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    data = mailToSend
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public List<Mail> GetNotSentMail()
        {
            return _dbContext.Mail.Where(m => m.SentDate == null).ToList();
        }

        [HttpPut]
        public async Task<ActionResult<Mail>> SentMail(Mail mailSent)
        {
            mailSent.SentDate = DateTime.Now;

            _dbContext.Mail.Update(mailSent);
            await _dbContext.SaveChangesAsync();

            return Ok(mailSent);
        }
    }
}