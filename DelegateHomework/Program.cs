using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DelegateHomework
{
    public class Program
    {
        static void Main(string[] args)
        {
            ImageDownloader Image = new ImageDownloader();
            try
            {
                Image.Subscribe();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Image.Download();

            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadKey();   
        }

        public class ImageDownloader
        {
            string remoteUri = "https://vsegda-pomnim.com/uploads/posts/2022-02/1645922176_13-vsegda-pomnim-com-p-polyarnoe-siyanie-foto-14.jpg";
            string fileName = "bigimage.jpg";

            public event Action ImageStarted;
            public event Action ImageCompleted;
           
            public void Download()
            {
                ImageStarted?.Invoke();
                var myWebClient = new WebClient();
                Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n", fileName, remoteUri);
                myWebClient.DownloadFile(remoteUri, fileName);
                Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
                ImageCompleted?.Invoke();
            }

            public void Subscribe()
            {
                ImageStarted += StartDownload;
                ImageCompleted += CompleteDownload;
            }

            private void StartDownload()
            {
                Console.WriteLine("Скачивание файла началось");
            }

            private void CompleteDownload()
            {
                Console.WriteLine("Скачивание файла завершено");
            }
        }
    }
}