using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using notification_services.Infrastructure;
using notification_services.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_services.Application.UseCases.Notification.Command.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationCommandDto>
    {
        private readonly NotifContext _context;

        public CreateNotificationCommandHandler(NotifContext context)
        {
            _context = context;
        }

        public async Task<CreateNotificationCommandDto> Handle(CreateNotificationCommand request, CancellationToken cancellation)
        {
            var noList = _context.Notifs.ToList();
            
            var no = new NotifEn
            {
                Title = request.Data.Attributes.Title,
                Message = request.Data.Attributes.Message,
            };

            if (!noList.Any(i => i.Title == request.Data.Attributes.Title))
            {
                _context.Notifs.Add(no);
            }
            await _context.SaveChangesAsync();

            var anotherno = _context.Notifs.First(i => i.Title == request.Data.Attributes.Title);
            foreach (var i in request.Data.Attributes.Targets)
            {
                _context.Logs.Add(new LogsEn
                {
                    Notification_Id = anotherno.Id,
                    Type = request.Data.Attributes.Type,
                    From = request.Data.Attributes.From,
                    Target = i.Id,
                    Email_Destination = i.Email_Destination
                });
                await _context.SaveChangesAsync();
                await SendMail("iniemail@email.com", i.Email_Destination, request.Data.Attributes.Title, request.Data.Attributes.Message);
            }
            await _context.SaveChangesAsync();

            return new CreateNotificationCommandDto
            {
                Success = true,
                Message = "Notification successfully created"
            };
        }

        public async Task<List<UserEn>> GetUserData()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync("http://user-service/user");
            return JsonConvert.DeserializeObject<List<UserEn>>(data);
        }

        public async Task SendMail(string from, string to, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("b0549629350b6a", "b4ff41cb5cfb79"),
                EnableSsl = true
            };
            await client.SendMailAsync(from, to, subject, body);
            Console.WriteLine("Sent");
        }
    }
}