using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using RestSharp.Authenticators;
using SparkPost;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using StoreManager.Services.MessageService;
using RazorEngine;
using RazorEngine.Templating;
using System.Drawing.Text;
using System.Configuration;
using StoreManager.Interfaces;
using Microsoft.Extensions.Configuration;

namespace StoreManager.Services.OrderControllerServices
{
    public class EmailSmtpService : IEmailSmtpService
    {
        private IConfiguration _configuration;
        private string _userName;
        private string _password;

        public void SendEmail(DTO.Order order)
        {
            var orderProductsService = new OrderProductsService();
            var orderWithProducts = orderProductsService.GetOrderWithProducts(order);
            var message = ComposeMessage(orderWithProducts);
            SendMessage(message);
        }

        private MimeMessage ComposeMessage(DTO.Order order)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Admin", "oa@sharpdeveloper.co.uk"));
            //mail.To.Add(new MailboxAddress("Customer", "Papercut@user.com"));
            mail.To.Add(new MailboxAddress("Customer", "oamoscovitch@gmail.com"));
            mail.Subject = "Invoice";
            mail.Body = new TextPart("html") { Text = GetMessageTemplate(order) };

            return mail;
        }

        private string GetMessageTemplate(DTO.Order order)
        {
            //string path = @"../../../../StoreManager/Services/OrderControllerServices/EmailTemplate.txt";
            string path = @"C:/Users/user/source/repos/Wpf/StoreManager/StoreManager/Services/OrderControllerServices/EmailTemplate.txt";
            string template = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(path));
            string returnedView = Engine.Razor.RunCompile(template, "report", typeof(DTO.Order), order, null);
            return returnedView;
        }
        private void SendMessage(MimeMessage mail)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    // XXX - Should this be a little different?
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.eu.mailgun.org", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //client.Authenticate(Secrets.MailGunUserName, Secrets.MailGunPassword);
                    ConfigureSecrets();
                    client.Authenticate(_userName, _password);

                    client.Send(mail);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                ToastService.ErrorToast(e.Message);
                return;
            }
            ToastService.SuccessToast();
        }

        private void ConfigureSecrets()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\user\AppData\Roaming\Microsoft\UserSecrets\LocalKeyVault\")
            .AddJsonFile("secrets.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            _userName = _configuration["username"];
            _password = _configuration["password"];
        }
    }
}
