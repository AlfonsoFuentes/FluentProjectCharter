﻿using Microsoft.Extensions.Options;
using Shared.Models.IdentityModels.Requests.Mail;

namespace Server.Implementations.Storage
{
    //public class SMTPMailService : IMailService
    //{
    //    private readonly MailConfiguration _config;
    //    private readonly ILogger<SMTPMailService> _logger;

    //    public SMTPMailService(IOptions<MailConfiguration> config, ILogger<SMTPMailService> logger)
    //    {
    //        _config = config.Value;
    //        _logger = logger;
    //    }

    //    public async Task SendAsync(MailRequest request)
    //    {
    //        try
    //        {
    //            var email = new MimeMessage
    //            {
    //                Sender = new MailboxAddress(_config.DisplayName, request.From ?? _config.From),
    //                Subject = request.Subject,
    //                Body = new BodyBuilder
    //                {
    //                    HtmlBody = request.Body
    //                }.ToMessageBody()
    //            };
    //            email.To.Add(MailboxAddress.Parse(request.To));
    //            using var smtp = new SmtpClient();
    //            await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
    //            await smtp.AuthenticateAsync(_config.UserName, _config.Password);
    //            await smtp.SendAsync(email);
    //            await smtp.DisconnectAsync(true);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex.Message, ex);
    //        }
    //    }
    //}
}