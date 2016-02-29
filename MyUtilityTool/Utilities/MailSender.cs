using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilityTool.Utilities
{
    /// <summary>
    /// メールの送信処理を行うクラスです。
    /// </summary>
    public class MailSender
    {
        private readonly MailMessage _message;

        /// <summary>
        /// 送信先アドレス、送信元アドレス、件名、本文を受け取ってインスタンスを生成します。
        /// </summary>
        /// <param name="fromAddress">送信先アドレス。</param>
        /// <param name="toAdderss">送信元アドレス。</param>
        /// <param name="subject">件名。</param>
        /// <param name="boby">本文。</param>
        public MailSender(string fromAddress, string toAdderss, string subject, string boby)
        {
            _message = new MailMessage
            {
                To = { new MailAddress(toAdderss) },
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = boby
            };
        }

        /// <summary>
        /// メールを送信します。
        /// </summary>
        /// <param name="host">SMTPサーバーのホスト名。</param>
        /// <param name="smtpUser">STMPサーバーのログインユーザー。</param>
        /// <param name="smtpPassword">SMTPサーバーのパスワード。</param>
        public void Send(string host, string smtpUser, string smtpPassword)
        {
            using (var client = new SmtpClient { Host = host })
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                client.EnableSsl = false;
                client.Send(_message);
            }
        }
    }
}
