using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public class WebFileUploader
    {
        WebClient _Client;

        public Action<UploadFileCompletedEventArgs> CompletedAction { get; set; }
        public Action<UploadProgressChangedEventArgs> ProgressAction { get; set; }

        public WebFileUploader()
        {
            _Client = new WebClient();
        }

        private void _Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            CompletedAction(e);
        }

        private void _Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            ProgressAction(e);
        }

        public void SubscribeCompleted(Action<UploadFileCompletedEventArgs> completedAction)
        {
            CompletedAction = completedAction;
            _Client.UploadFileCompleted += _Client_UploadFileCompleted;
        }

        public void SubscribeProgress(Action<UploadProgressChangedEventArgs> progressAction)
        {
            ProgressAction = progressAction;
            _Client.UploadProgressChanged += _Client_UploadProgressChanged;
        }

        public void Upload(Uri address, string localFilename)
        {
            _Client.UploadFileAsync(address, localFilename);
        }
    }
}
