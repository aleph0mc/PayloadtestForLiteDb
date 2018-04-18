using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace WpfLiteDbTest
{
    public static class LiteDbHelper
    {
        private static readonly string _liteDbSource;

        static LiteDbHelper()
        {
            _liteDbSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "liteDbTest.db");
        }

        public static List<ArchivedMessage> GetAllArchivedMessageList()
        {
            List<ArchivedMessage> ret = null;
            using (var db = new LiteDatabase(_liteDbSource))
            {
                var coll = db.GetCollection<ArchivedMessage>("archivedMessage");
                ret = coll.FindAll().ToList();
            }
            return ret;
        }

        public static void SaveEmailInArchive(ArchivedMessage ArchMsg)
        {
            using (var db = new LiteDatabase(_liteDbSource))
            {
                var coll = db.GetCollection<ArchivedMessage>("archivedMessage");
                if (coll.Exists(x => x.Id.Equals(ArchMsg.Id)))
                    coll.Update(ArchMsg);
                else  // INSERT NEW INFO (ID WILL BE AUTO-INCREMENTED)
                    coll.Insert(ArchMsg);

            }
        }

        public static void DeleteArchivedMessage(ArchivedMessage ArchMsg)
        {
            using (var db = new LiteDatabase(_liteDbSource))
            {
                var coll = db.GetCollection<ArchivedMessage>("archivedMessage");
                var rec = coll.Find(x => x.Id.Equals(ArchMsg.Id)).SingleOrDefault();
                if (null != rec)
                    coll.Delete(rec.Id);
            }
        }

        public static void DeleteAllAchivedMessages()
        {
            using (var db = new LiteDatabase(_liteDbSource))
            {
                var coll = db.GetCollection<ArchivedMessage>("archivedMessage");
                coll.Delete(r => r.SentDate < DateTime.Now);
            }
        }
    }
}
