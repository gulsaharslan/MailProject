using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using MailProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MailProject.Controllers
{
	public class MailController : Controller

	{

        private readonly UserManager<AppUser> _userManager;
        private readonly IMessagesService _messageService;
        private readonly MailContext _mailContext;

        public MailController(UserManager<AppUser> userManager, IMessagesService messageService, MailContext mailContext)
        {

            _messageService = messageService;
            _userManager = userManager;
            _mailContext = mailContext;
        }

        public async Task<ActionResult> Inbox()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _messageService.TGetListByUserId(user.Id);
            ViewBag.totalMail = _messageService.TCountMessage(user.Id);
            return View(values);
        }

        public async Task<ActionResult> ReadMail(int id)
        {

            var mail=_messageService.TGetByMessageId(id);
            mail.IsRead = true;
            _messageService.TUpdate(mail);
            return View(mail);
        }

        public async Task<ActionResult> Sendbox()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _messageService.TGetListBySentMessages(user.Id);
            return View(values);
        }

        public async Task<ActionResult> SentReadMail(int id)
        {

            var mail = _messageService.TGetByMessageId(id);
            return View(mail);
        }

        [HttpGet]

        public IActionResult SendMail(string selectedUserId = null)
        {
            List<SelectListItem> values = (from x in _mailContext.AppUsers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Email,
                                               Value = x.Id.ToString()

                                           }).ToList();
            ViewBag.users = values;
            
            var model = new CreateMailViewModel();
            if (selectedUserId != null)
            {
                model.ReceiverUserId = Convert.ToInt32(selectedUserId);
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> SendMail(CreateMailViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            Messages messages = new Messages()
            {
                AppUserId = user.Id,
                Subject = model.Subject,
                ReceiverUserId = model.ReceiverUserId,
                SenderUserId= user.Id,
                Body = model.Body,
                SentAt = DateTime.Now,
                IsDeleted = false,
                IsDraft=model.IsDraft,
                IsFavorite = false,
                IsRead = false,
            };

            _mailContext.Add(messages);
            _mailContext.SaveChanges();

            if (model.IsDraft)
            {
                return RedirectToAction("DraftMail", "Mail");
            }
            else
            {
                return RedirectToAction("Sendbox", "Mail");
            }
        }

        public async Task<IActionResult> DraftMail()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _messageService.TGetListDraftMail(user.Id);
            return View(values);
        }

        [HttpGet]
        public IActionResult DraftMailDetail(int id)
        {
            List<SelectListItem> value = (from x in _mailContext.AppUsers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Email,
                                               Value = x.Id.ToString()

                                           }).ToList();
            ViewBag.users = value;
            var values=_messageService.TGetByMessageId(id);
            return View(values);

        }
        [HttpPost]

        public async Task<IActionResult> DraftMailDetail(Messages messages)
        {
            var values = _messageService.TGetByMessageId(messages.MessageId);
            values.SenderUser.Email=messages.SenderUser.Email;
            values.ReceiverUser.Email = messages.ReceiverUser.Email;
            values.Body=messages.Body;
            values.Subject=messages.Subject;
            _mailContext.Update(values);
            await _mailContext.SaveChangesAsync();

            return RedirectToAction("DraftMail", "Mail");
        }

        public async Task<IActionResult> DeleteMail(List<int> messageIds)
        {
            foreach (var messageId in messageIds)
            {
               
                var trash = _mailContext.Messages.Find(messageId);
                _mailContext.Messages.Remove(trash);
            }
            _mailContext.SaveChanges();
            return NoContent();
        }

        public async Task<IActionResult> Trash()
        {
            var user =  await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _messageService.TGetListDeletedMail(user.Id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> SendTrash(List<int> messageIds)
        {
            foreach (var messageId in messageIds)
            {
                var mail = _messageService.TGetByMessageId(messageId);
                mail.IsDeleted = true;
                _messageService.TUpdate(mail);
            }

            return NoContent();
        }
    }
}

