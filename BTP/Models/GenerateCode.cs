namespace BTP.Models
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public class GenerateCode
    {

        private static GenerateCode? _instance;
        private int currentCode;
        private DateTime codeGenerationTime;

        private GenerateCode()
        {
            GenerateNewCode();
        }

        public static GenerateCode Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GenerateCode();
                }
                return _instance;
            }
        }

        private void GenerateNewCode()
        {
            Random random = new();
            currentCode = random.Next(100000, 1000000);
            codeGenerationTime = DateTime.Now;
        }

        public int StockageCodeTemporaire()
        {
            if (DateTime.Now - codeGenerationTime > TimeSpan.FromMinutes(5))
            {
                GenerateNewCode();
            }
            Console.WriteLine(currentCode);
            return currentCode;
        }

        public static int Valiny()
        {
            return Instance.StockageCodeTemporaire();
        }
        public static void SendCode(string mail_recepteur)
        {
            string mail_emetteur = "manbad614@gmail.com";
            int? code = Valiny();
            
            if (code.HasValue)
            {
                MailMessage message = new();
                message.From = new MailAddress(mail_emetteur);
                message.To.Add(new MailAddress(mail_recepteur));
                message.Subject = "Votre code de validation";
                message.Body = "Votre code est : " + code.Value.ToString();

                SmtpClient smtpClient = new("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential(mail_emetteur, "blro jmmu xwcb oqpb"); 
                smtpClient.EnableSsl = true;

                smtpClient.Send(message);
            }
        }

        public static bool IsCode(int code)
        {
            bool ans = true;
            if(code != Valiny()){
                ans = false;
            }
            Console.WriteLine(ans);
            return ans;
        }
    }
}
