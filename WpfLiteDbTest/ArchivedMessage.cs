using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLiteDbTest
{
    public class Attachment
    {
        public string AttachmentName { get; set; }
        public byte[] FileBytes { get; set; }
    }

    public class ArchivedMessage
    {
        [BsonId]
        public Guid Id { get; set; }
        public string MailTo { get; set; }
        public string SubjectId { get; set; }
        public string BodyPlainText { get; set; }
        public DateTime SentDate { get; set; }
        public Attachment[] Attachements { get; set; }
    }
}
