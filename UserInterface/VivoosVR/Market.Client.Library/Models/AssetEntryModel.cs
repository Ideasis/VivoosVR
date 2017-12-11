using Core.MVVM;
using Market.Client.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Market.Client.Models
{
    public class AssetEntryModel
    {
        public Observable<bool> AutomaticUpload { get; set; }
        public Observable<Visibility> AutomaticUploadVisibility { get; set; }
        public Observable<bool> Available { get; set; }
        public Observable<BindingList<AssetCommandModel>> Commands { get; set; }
        public Observable<BindingList<AssetDefaultModel>> Defaults { get; set; }
        public Observable<string> Description { get; set; }
        public Observable<DateTime> EntryDate { get; set; }
        public Observable<string> Exe { get; set; }
        public Observable<Guid> GroupId { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<byte[]> Image { get; set; }
        public Observable<string> ImagePath { get; set; }
        public Observable<DateTime> ModifyDate { get; set; }
        public Observable<string> Name { get; set; }
        public Observable<string> Url { get; set; }
        public Observable<string> ZipPath { get; set; }
        public Observable<ImageSource> ImageSource { get; }

        public AssetEntryModel()
        {
            Id = new Observable<Guid>();
            Image = new Observable<byte[]>();
            Name = new Observable<string>("");
            Description = new Observable<string>("");
            Url = new Observable<string>("");
            Exe = new Observable<string>("");
            Available = new Observable<bool>(true);
            ImagePath = new Observable<string>("");
            GroupId = new Observable<Guid>(Guid.Empty);
            ZipPath = new Observable<string>("");
            AutomaticUpload = new Observable<bool>(false);
            AutomaticUploadVisibility = new Observable<Visibility>(AutomaticUpload.Value ? Visibility.Visible : Visibility.Hidden);
            Commands = new Observable<BindingList<AssetCommandModel>>(new BindingList<AssetCommandModel>());
            Defaults = new Observable<BindingList<AssetDefaultModel>>(new BindingList<AssetDefaultModel>());
            EntryDate = new Observable<DateTime>();
            ModifyDate = new Observable<DateTime>();
            ImageSource = new Observable<ImageSource>();

            AutomaticUpload.Subscribe(x =>
            {
                AutomaticUploadVisibility.Value = x ? Visibility.Visible : Visibility.Hidden;
            });

            Image.Subscribe(x =>
                {
                    ImageSource.Value = Image.Value.ConvertToImageSource();
                });

            Image.SubscribeValidate(x =>
            {
                if (x == null || x.Length == 0)
                {
                    return "Resim mutlaka seçilmelidir.";
                }
                else if (x.Length > Market.Client.Properties.Settings.Default.MaxImageLength)
                {
                    return string.Format("Resim {0}KB üzerinde verilemez.", Market.Client.Properties.Settings.Default.MaxImageLength / 1000);
                }
                return null;
            });

            ImageSource.SubscribeValidate(x =>
                {
                    return Image.Validate();
                });

            Name.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "İsim alanı mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Description.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Açıklama alanı mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Url.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "İnternet adresi alanı mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Exe.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Çalıştırılacak program alanı mutlaka dolu olmalıdır.";
                }
                else if (!x.EndsWith(".exe"))
                {
                    return "Çalıştırılacak program mutlaka '.exe' uzantılı olmalıdır.";
                }
                return null;
            });

            ZipPath.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Zip dosyası alanı mutlaka dolu olmalıdır.";
                }
                else if (!x.EndsWith(".zip"))
                {
                    return "Zip dosyası mutlaka '.zip' uzantılı olmalıdır.";
                }
                return null;
            });

            GroupId.SubscribeValidate(x =>
            {
                if (GroupId.Value == Guid.Empty)
                {
                    return "Grup mutlaka seçilmelidir.";
                }
                return null;
            });

            ImagePath.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Dosya yolu boş bırakılamaz.";
                    }

                    if (!File.Exists(x))
                    {
                        return "Dosya bulunmadı.";
                    }

                    return null;
                });
        }

        public bool GetIsValid()
        {
            return
                Image.Value != null &&
                Image.Value.Length > 0 &&
                Image.Value.Length <= Market.Client.Properties.Settings.Default.MaxImageLength &&
                !string.IsNullOrWhiteSpace(Name.Value) &&
                !string.IsNullOrWhiteSpace(Description.Value) &&
                !string.IsNullOrWhiteSpace(Url.Value) &&
                !string.IsNullOrWhiteSpace(Exe.Value) &&
                ((!string.IsNullOrWhiteSpace(ZipPath.Value) && AutomaticUpload.Value) || !AutomaticUpload.Value) &&
                Exe.Value.EndsWith(".exe") &&
                GroupId.Value != Guid.Empty;
        }
    }
}
