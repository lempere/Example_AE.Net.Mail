using AE.Net.Mail;
using AE.Net.Mail.Imap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_AE.Net.Mail
{
    class Program
    {
        private static string sHost = "imap.gmail.com";
        private static int sPort = 993;
        private static string sUser = "@gmail.com";
        private static string sPwd = "";
        private static bool bSSL = true;

        static void Main(string[] args)
        {
             Console.WriteLine("Debug running ae.net.mail" );
           Console.WriteLine("Introduce el correo");
           sUser = Console.ReadLine();
           Console.WriteLine("Introduce el password");
           sPwd = Console.ReadLine();
           if (sUser == null || sPwd == null)
           {
               Console.WriteLine("Incorrecto user/pass");
               return;
           }
           using (var ic = new AE.Net.Mail.ImapClient(sHost, sUser, sPwd, ImapClient.AuthMethods.Login, sPort, bSSL))
           {
               Mailbox[] boxes = ic.ListMailboxes("", "*");
               foreach (Mailbox b in boxes)
               {
                   try
                   {
                       Console.WriteLine("box: " + b.Name);
                       ic.SelectMailbox(b.Name);
                       Console.WriteLine("\t" + ic.GetMessageCount());
                   }
                   catch (Exception) { continue; }
               }
               ic.SelectMailbox("INBOX");
               int messages = ic.GetMessageCount() - 1;
               Console.WriteLine("Message in INBOX " + messages);
               MailMessage[] mm = ic.GetMessages(ic.GetMessageCount() - 1, ic.GetMessageCount() - 2, false, true);
               foreach (MailMessage m in mm)
               {
                   Console.WriteLine("********" + m.Date + "*********\n");
                   
                   Console.WriteLine("Body:\n" + m.Subject.ToString());
                   Console.WriteLine("*****************\n");

               }
               Debug.WriteLine("IMAP : Found " + mm.Count() + " messages in Inbox");

               Console.WriteLine("Press key to end.");
               Console.ReadKey();
           }
        }
    }
}
