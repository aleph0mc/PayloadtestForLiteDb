using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLiteDbTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // archive emails batch
            archiveEmailWithAttachments();
        }

        private void archiveEmailWithAttachments()
        {
            for (int i = 0; i < 1000; i++)
            {
                var email = new ArchivedMessage
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    MailTo = $"mail_{Guid.NewGuid().ToString()}@test.com",
                    BodyPlainText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Hic, enim rerum et quod! Explicabo, eveniet blanditiis voluptas vel architecto sed quia nostrum commodi odio praesentium mollitia nihil quibusdam facere eaque aliquam id delectus rem maxime sapiente ipsam magni voluptate qui cum voluptatem culpa impedit quam? Aliquam, reprehenderit, deserunt, ipsum exercitationem quibusdam maxime optio eius culpa quae ullam molestiae deleniti corrupti dolores facere vel dicta alias officiis autem fuga ipsa sapiente libero esse dignissimos! Delectus, dignissimos, possimus, commodi suscipit quisquam eligendi deserunt eveniet veniam et amet debitis sit voluptate eaque consectetur voluptatum omnis itaque atque quas consequuntur laboriosam laudantium a vero?",
                    SentDate = DateTime.Now,
                };
                var rnd = new Random(DateTime.Now.Millisecond);
                var attnum = rnd.Next(3);
                var attchs = new Attachment[attnum];
                for (int j = 0; j < attnum; j++)
                {
                    var bytes = rnd.Next(100, 2000000);
                    byte[] bs = new byte[bytes];
                    rnd.NextBytes(bs);

                    var attach = new Attachment
                    {
                        AttachmentName = $"file_{Guid.NewGuid().ToString()}.ecbf",
                        FileBytes = bs
                    };

                    attchs[j] = attach;
                }
                email.Attachements = attchs;

                LiteDbHelper.SaveEmailInArchive(email);
            }
        }
    }
}
