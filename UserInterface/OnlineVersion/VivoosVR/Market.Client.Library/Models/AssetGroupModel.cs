using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class AssetGroupModel
    {
        private int _ItemCounter;
        private object _ItemCounterLock = new object();

        public Observable<List<AssetModel>> Assets { get; set; }
        public Observable<string> Description { get; set; }
        public Observable<Guid> Id { get; set; }
        public int ItemCounter
        {
            get { return _ItemCounter; }
            set
            {
                lock (_ItemCounterLock)
                {
                    _ItemCounter = value;
                }
            }
        }
        public Observable<string> Name { get; set; }
        public Observable<int> Progress { get; set; }

        public AssetGroupModel()
        {
            Id = new Observable<Guid>();
            Name = new Observable<string>();
            Description = new Observable<string>();
            Assets = new Observable<List<AssetModel>>(new List<AssetModel>());
            Progress = new Observable<int>();
        }
    }
}
