using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Net;

namespace CraftMan_WebApi.Helper
{
    public class EmailHelper
    {
        private static string smtpServer;
        private static int smtpPort;
        private static string senderEmail;
        private static string senderPassword;

        public static void SendResetEmail(string email, string token)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            smtpServer = configuration["ApplicationURL:Host"].ToString();
            smtpPort = Convert.ToInt32(configuration["ApplicationURL:Port"]); // Gmail uses port 587 for TLS
            senderEmail = configuration["ApplicationURL:Mail"].ToString();
            senderPassword = configuration["ApplicationURL:Password"].ToString();

            string subject = "Password Reset Code";

            string body = $@"
                            <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        background-color: #f4f4f4;
                                        padding: 20px;
                                    }}
                                    .email-container {{
                                        max-width: 500px;
                                        margin: auto;
                                        background: #ffffff;
                                        padding: 20px;
                                        border-radius: 8px;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                        text-align: center;
                                    }}
                                    .header {{
                                        background: #007bff;
                                        color: white;
                                        padding: 10px;
                                        font-size: 20px;
                                        font-weight: bold;
                                        border-radius: 8px 8px 0 0;
                                    }}
                                    .content {{
                                        padding: 20px;
                                        font-size: 16px;
                                        color: #333;
                                    }}
                                    .code {{
                                        font-size: 22px;
                                        font-weight: bold;
                                        color: #007bff;
                                        background: #f8f9fa;
                                        padding: 10px;
                                        display: inline-block;
                                        border-radius: 5px;
                                        margin: 10px 0;
                                    }}
                                    .footer {{
                                        font-size: 14px;
                                        color: #777;
                                        margin-top: 20px;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='email-container'>
                                    <div class='header'>Password Reset Request</div>
                                    <div class='content'>
                                        <p>Hello,</p>
                                        <p>We received a request to reset your password. Use the code below to proceed:</p>
                                        <div class='code'>{token}</div>
                                        <p>This code is valid for <b>15 minutes</b>. If you didn’t request a password reset, please ignore this email.</p>
                                    </div>
                                    <div class='footer'>
                                        <p>Thank you,</p>
                                        <p><b>CraftMan</b></p>
                                    </div>
                                </div>
                            </body>
                            </html>";

            bool emailSent = EmailHelper.SendEmail(email, subject, body);
            if (emailSent)
            {
                Console.WriteLine("Password reset email sent successfully.");
            }
            else
            {
                Console.WriteLine("Failed to send password reset email.");
            }


        }



        public static bool SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true; // Set to true if sending HTML emails

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = true; // Enable SSL for Gmail
                        smtp.Send(mail);
                    }
                }
                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false; // Email sending failed
            }
        }


    }

}