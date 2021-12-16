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
using StoreInventory.Services.MessageService;
using RazorEngine;
using RazorEngine.Templating;
using System.Drawing.Text;

namespace StoreInventory.Services.OrderControllerServices
{
    internal class EmailSmtpService
    {
        public static void SendEmail(DTO.Order order)
        {
            var orderProductsService = new OrderProductsService();
            var orderWithProducts = orderProductsService.GetOrderWithProducts(order);
            var message = ComposeMessage(orderWithProducts);
            SendMessage(message);
        }

        private static MimeMessage ComposeMessage(DTO.Order order)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Admin", "oa@sharpdeveloper.co.uk"));
            mail.To.Add(new MailboxAddress("Customer", "oamoscovitch@gmail.com"));
            mail.Subject = "Invoice";
            mail.Body = new TextPart("html") { Text = GetMessageTemplate(order) };
            
            return mail;
        }

        private static string GetMessageTemplate(DTO.Order order)
        {
            string path = @"../../../../StoreInventory/Services/OrderControllerServices/Html.txt";
            string template = System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(path));
            string returnedView = Engine.Razor.RunCompile(template, "report", typeof(DTO.Order), order, null);
            return returnedView;
        }

        private static void SendMessage(MimeMessage mail)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    // XXX - Should this be a little different?
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.eu.mailgun.org", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(Secrets.MailGunUserName, Secrets.MailGunPassword);

                    client.Send(mail);
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
                ToastService.ErrorToast();
                return;
            }
            ToastService.SuccessToast();
        }
    }
}
