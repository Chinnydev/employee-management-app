using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.App.Dtos;
using EmployeeManagement.App.Models;
using EmployeeManagement.App.Repository;
using EmployeeManagement.App.Service;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EmployeeManagement.App.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        // Logger is not shown in the code snippet
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository contactRepository, IMapper mapper, IConfiguration configuration, ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ContactResult> CreateContactAsync(ContactCreateDto contactDto)
        {
            // Map the DTO to the entity using AutoMapper
            var contactRequest = _mapper.Map<ContactRequest>(contactDto);
            contactRequest.SentAt = DateTime.UtcNow;

            // Save to the database
            await _contactRepository.AddContactAsync(contactRequest);
            await _contactRepository.SaveChangesAsync();

            // Send email notification asynchronously and capture result.
            try
            {
                await SendEmailAsync(contactDto);
                return new ContactResult
                {
                    Success = true,
                    Message = "Thank you for contacting us. We have received your message!"
                };
            }
            catch (Exception ex)
            {
                // Log the full exception details for debugging
                _logger.LogError(ex, "Error sending email notification.");

                // Log exception if needed.
                return new ContactResult
                {
                    Success = false,
                    Message = "There was a problem sending the email notification: " + ex.ToString()
                };
            }
        }

        private async Task SendEmailAsync(ContactCreateDto contactDto)
        {
            // Retrieve email settings from configuration
            var fromEmail = _configuration["EmailSettings:FromEmail"];       // e.g., "your_yahoo_email@yahoo.com"
            var smtpHost = _configuration["EmailSettings:SmtpHost"];         // "smtp.mail.yahoo.com"
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]); // e.g., "465"
            var smtpUser = _configuration["EmailSettings:SmtpUser"];         // e.g., "your_yahoo_email@yahoo.com"
            var smtpPass = _configuration["EmailSettings:SmtpPass"];         // your Yahoo app-specific password

            // Create a new MIME message using MimeKit
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EmployeeManagement", fromEmail));
            message.To.Add(new MailboxAddress(contactDto.Name, contactDto.Email));
            message.Subject = "Thank you for contacting us!";

            // Set the email body (plain text)
            message.Body = new TextPart("plain")
            {
                Text = $"Dear {contactDto.Name},\n\nThank you for reaching out. We have received your message:\n\n{contactDto.Message}\n\nWe will get back to you soon.\n\nBest regards,\nEmployeeManagement Team"
            };

            using (var client = new SmtpClient())
            {
                // Accept all SSL certificates (customize this for production if needed)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                // Connect using port 465 with SSL on connect (implicit SSL)
                await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.SslOnConnect);

                // Authenticate using Yahoo credentials (make sure to use an app-specific password)
                await client.AuthenticateAsync(smtpUser, smtpPass);

                // Send the email asynchronously
                await client.SendAsync(message);

                // Disconnect gracefully
                await client.DisconnectAsync(true);
            }
        }
    }
}



