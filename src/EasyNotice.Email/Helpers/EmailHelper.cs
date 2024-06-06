using MimeKit;
using System.Linq;

namespace EasyNotice.Email
{
    internal class EmailHelper
    {
        public static MimeMessage CreateMimeMessage(EmailSendRequest input, EmailOptions option)
        {
            MimeMessage mimeMessage = new MimeMessage();
            var fromMailAddress = new MailboxAddress(option.FromName ?? option.FromAddress, option.FromAddress);
            mimeMessage.From.Add(fromMailAddress);
            foreach (var toMailAddress in option.ToAddress.Select(MailboxAddress.Parse))
            {
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
                const string charset = "GB18030";
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
