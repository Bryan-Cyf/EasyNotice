using EasyNotice.Core;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyNotice.Email
{
    internal class EmailProvider : IEmailProvider
    {
        private readonly EmailOptions _emailOptions;
        private readonly NoticeOptions _noticeOptions;

        public EmailProvider(IOptionsMonitor<EmailOptions> emailOptions, IOptionsMonitor<NoticeOptions> noticeOptions)
        {
            _emailOptions = emailOptions.CurrentValue;
            _noticeOptions = noticeOptions.CurrentValue;
        }

        public async Task<EasyNoticeSendResponse> SendAsync(string title, Exception exception)
        {
            var response = new EasyNoticeSendResponse();
            try
            {
                response = await SendAsync(new EmailSendRequest
                {
                    Subject = title,
                    Body = $"{exception.Message}{Environment.NewLine}{exception}"
                });
            }
            catch (Exception ex)
            {
                response.ErrMsg = $"发送邮件失败,{ex.Message}";
            }
            return response;
        }

        public async Task<EasyNoticeSendResponse> SendAsync(EmailSendRequest input)
        {
            var response = new EasyNoticeSendResponse();
            try
            {
                await IntervalHelper.IntervalExcuteAsync(async () =>
                {
                    var message = CreateMimeMessage(input);
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.CheckCertificateRevocation = false;
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Timeout = 10 * 1000;
                        await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, SecureSocketOptions.Auto);
                        await client.AuthenticateAsync(_emailOptions.FromAddress, _emailOptions.Password);
                        await client.SendAsync(message);
                    }
                }, input.Subject, _noticeOptions.IntervalSeconds);

            }
            catch (Exception ex)
            {
                response.ErrMsg = $"邮件发送异常:{ex.Message}";
            }
            return response;
        }

        private MimeMessage CreateMimeMessage(EmailSendRequest input)
        {
            MimeMessage mimeMessage = new MimeMessage();
            var fromMailAddress = new MailboxAddress(_emailOptions.FromName ?? _emailOptions.FromAddress, _emailOptions.FromAddress);
            mimeMessage.From.Add(fromMailAddress);
            for (int i = 0; i < _emailOptions.ToAddress.Count; i++)
            {
                var item = _emailOptions.ToAddress[i];
                var toMailAddress = MailboxAddress.Parse(item);
                mimeMessage.To.Add(toMailAddress);
            }
            BodyBuilder bodyBuilder = new BodyBuilder()
            {
                HtmlBody = input.Body,
            };
            if (input.Attachments != null)
            {
                var attachment = bodyBuilder.Attachments.Add(input.FileName, input.Attachments);
                //解决中文文件名乱码
                var charset = "GB18030";
                attachment.ContentType.Parameters.Clear();
                attachment.ContentDisposition.Parameters.Clear();
                attachment.ContentType.Parameters.Add(charset, "name", input.FileName);
                //解决文件名不能超过41字符
                foreach (var param in attachment.ContentType.Parameters)
                {
                    param.EncodingMethod = ParameterEncodingMethod.Rfc2047;
                }
            }
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = input.Subject;
            return mimeMessage;
        }

    }
}
