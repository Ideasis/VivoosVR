using Core.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Market.Client.Models
{
    public class AssetAdminListModel
    {
        public Observable<bool> Available { get; set; }
        public Observable<string> Description { get; set; }
        public Observable<string> ExeName { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<ImageSource> ImageSource { get; set; }
        public Observable<DateTime> ModifyDate { get; set; }
        public Observable<string> Name { get; set; }
        public Observable<string> Url { get; set; }

        public AssetAdminListModel()
        {
            Id = new Observable<Guid>();
            Name = new Observable<string>();
            Description = new Observable<string>();
            Url = new Observable<string>();
            ExeName = new Observable<string>();
            Available = new Observable<bool>();
            ImageSource = new Observable<ImageSource>();
            ModifyDate = new Observable<DateTime>();
        }
    }
}
