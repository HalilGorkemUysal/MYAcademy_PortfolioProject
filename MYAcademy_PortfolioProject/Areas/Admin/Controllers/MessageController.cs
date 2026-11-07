using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList(string search)
        {
            var values = _context.Messages.AsQueryable();

            // Gönderen veya konuya göre arama
            if (!string.IsNullOrWhiteSpace(search))
            {
                values = values.Where(x =>
                    x.SenderName.Contains(search) ||
                    x.Subject.Contains(search));
            }

            // En yeni mesaj en üstte
            var messages = values
                .OrderByDescending(x => x.SendDate)
                .ThenByDescending(x => x.MessageId)
                .ToList();

            ViewBag.Search = search;

            // Okunmamış mesaj sayısı (arama filtresinden bağımsız)
            ViewBag.UnreadCount = _context.Messages
                .Count(x => !x.IsRead);

            ViewBag.TotalCount = _context.Messages.Count();

            return View(messages);
        }



        [HttpGet]
        public IActionResult GetMessageDetail(int id)
        {
            var message = _context.Messages.Find(id);

            if (message == null)
                return NotFound();

            return Json(new
            {
                messageId = message.MessageId,
                senderName = message.SenderName,
                senderEmail = message.SenderEmail,
                subject = message.Subject,
                messageDetail = message.MessageDetail,
                sendDate = message.SendDate.ToString("dd.MM.yyyy HH:mm"),
                isRead = message.IsRead
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            var message = _context.Messages.Find(id);

            if (message == null)
                return NotFound();

            message.IsRead = true;
            _context.SaveChanges();

            return Json(new
            {
                success = true,
                unreadCount = _context.Messages.Count(x => !x.IsRead)
            });
        }


        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMessage(
            [Bind("SenderName,SenderEmail,Subject,MessageDetail")] Message message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var message = _context.Messages.Find(id);

            if (message == null)
                return NotFound();

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMessage(int id, bool? isRead)
        {
            var message = _context.Messages.Find(id);

            if (message == null)
                return NotFound();

            // Formdan durum gönderilirse onu kullan.
            // AJAX ile yalnızca id gelirse mevcut durumu tersine çevir.
            message.IsRead = isRead ?? !message.IsRead;

            _context.SaveChanges();

            // AJAX isteği ise JSON dön.
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new
                {
                    success = true,
                    isRead = message.IsRead,
                    unreadCount = _context.Messages.Count(x => !x.IsRead)
                });
            }

            return RedirectToAction("MessageList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.Messages.Find(id);

            if (message == null)
                return NotFound();

            _context.Messages.Remove(message);
            _context.SaveChanges();

            return Json(new
            {
                success = true,
                unreadCount = _context.Messages.Count(x => !x.IsRead),
                totalCount = _context.Messages.Count()
            });
        }

    }
}
