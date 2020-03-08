using System.Collections.Generic;
using System.IO;

namespace ThuanTanUmbraco.Mailer
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string SenderDisplayName { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Cc { get; set; }

        public string Body { get; set; }
        public List<string> AttachFiles { get; set; }
        public AttachFile AttachFileStream { get; set; }
        public bool IsBodyHtml { get;  set; }
    }

    public class AttachFile
    {
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}