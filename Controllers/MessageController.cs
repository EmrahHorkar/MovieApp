//using Microsoft.AspNetCore.Mvc;
//using MovieApp.Data;
//using MovieApp.Models;
//using System.Security.Claims;

//public class MessageController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IMessageService _messageService; // Replace with your actual message service

//    public MessageController(ApplicationDbContext context, IMessageService messageService)
//    {
//        _context = context;
//        _messageService = messageService;
//    }

//    [HttpGet]
//    public IActionResult Compose(int receiverId)
//    {
//        var receiver = _context.Users.Find(receiverId);
//        if (receiver == null)
//        {
//            // Handle the case where the receiver is not found
//            return NotFound();
//        }

//        var viewModel = new ComposeMessageViewModel
//        {
//            ReceiverId = receiver.Id,
//            ReceiverFullName = $"{receiver.FirstName} {receiver.LastName}"
//        };

//        return View(viewModel);
//    }

//    [HttpPost]
//    public IActionResult Compose(ComposeMessageViewModel viewModel)
//    {
//        if (ModelState.IsValid)
//        {
//            var senderId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

//            // Create and send the message
//            var message = new UserMessage
//            {
//                SenderId = senderId,
//                ReceiverId = viewModel.ReceiverId,
//                Content = viewModel.Content,
//                SentAt = DateTime.UtcNow
//            };

//            _messageService.SendMessage(message);

//            // Redirect back to the profile page or another appropriate location
//            return RedirectToAction("Profile", new { id = viewModel.ReceiverId });
//        }

//        return View(viewModel);
//    }
//}
