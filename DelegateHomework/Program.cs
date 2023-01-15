using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

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

            Task task = Task.Run(() =>
            {
                Image.Download();
            });

            Thread.Sleep(100);

            Console.WriteLine("Нажмите клавишу А для выхода или любую другую клавишу для проверки статуса скачивания");
            while (Console.ReadKey().Key.ToString() != "F")
                Console.WriteLine("Состояние загрузки картинки: " + Image.isStatusDownload.ToString());

        }

        public class ImageDownloader
        {
            string remoteUri = "https://img2.akspic.ru/crops/4/0/6/8/6/168604/168604-plamya-ogon-ulichnyj_fonar-gaz-teplo-7680x4320.jpg";
            string fileName = "bigimage.jpg";

            public event Action ImageStarted;
            public event Action ImageCompleted;

            public bool isStatusDownload;

            public void Download()
            {
                var myWebClient = new WebClient();

                ImageStarted?.Invoke();
                Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n", fileName, remoteUri);
                var result = myWebClient.DownloadFileTaskAsync(remoteUri, fileName);
                while (!result.IsCompleted)
                {
                    isStatusDownload = false;
                }
                isStatusDownload = true;
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