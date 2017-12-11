using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public class WebFileDownloader
    {
        WebClient _Client;

        public Action<AsyncCompletedEventArgs> CompletedAction { get; set; }
        public Action<DownloadProgressChangedEventArgs> ProgressAction { get; set; }

        public WebFileDownloader()
        {
            _Client = new WebClient();
        }

        private void _Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            CompletedAction(e);
        }

        private void _Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressAction(e);
        }

        public void Download(Uri address, string localFilename)
        {
            _Client.DownloadFileAsync(address, localFilename);
        }

        public void SubscribeCompleted(Action<AsyncCompletedEventArgs> completedAction)
        {
            CompletedAction = completedAction;
            _Client.DownloadFileCompleted += _Client_DownloadFileCompleted;
        }

        public void SubscribeProgress(Action<DownloadProgressChangedEventArgs> progressAction)
        {
            ProgressAction = progressAction;
            _Client.DownloadProgressChanged += _Client_DownloadProgressChanged;
        }
    }
}
