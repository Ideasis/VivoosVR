using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Core.MVVM
{
    public class PagedCollection<T> : ObservableCollection<T>
    {
        public Observable<bool> CanMoveNext { get; set; }
        public Observable<bool> CanMovePrevious { get; set; }
        public Observable<int> CurrentPage { get; set; }
        public Observable<int> ItemsPerPage { get; set; }
        public RxCommand MoveFirstCommand { get; set; }
        public RxCommand MoveLastCommand { get; set; }
        public RxCommand MoveNextCommand { get; set; }
        public RxCommand MovePreviousCommand { get; set; }
        public Observable<int> TotalPageCount { get; set; }
        public List<T> WholeList { get; set; }

        public PagedCollection(IList<T> list)
        {
            MoveNextCommand = new RxCommand();
            MovePreviousCommand = new RxCommand();
            MoveFirstCommand = new RxCommand();
            MoveLastCommand = new RxCommand();

            CurrentPage = new Observable<int>(1);
            ItemsPerPage = new Observable<int>(20);
            CanMoveNext = new Observable<bool>(false);
            CanMovePrevious = new Observable<bool>(false);
            WholeList = new List<T>(list);
            double pageCount = Math.Ceiling((double)WholeList.Count / (double)ItemsPerPage.Value);
            TotalPageCount = new Observable<int>(Convert.ToInt32(pageCount));

            LoadCurrentPage();

            MoveNextCommand.Subscribe(x =>
                {
                    if (CanMoveNext.Value)
                    {
                        CurrentPage.Value++;
                        LoadCurrentPage();
                    }
                });

            MovePreviousCommand.Subscribe(x =>
                {
                    if (CanMovePrevious.Value)
                    {
                        CurrentPage.Value--;
                        LoadCurrentPage();
                    }
                });

            MoveFirstCommand.Subscribe(x =>
                {
                    if (CanMovePrevious.Value)
                    {
                        CurrentPage.Value = 1;
                        LoadCurrentPage();
                    }
                });

            MoveLastCommand.Subscribe(x =>
            {
                if (CanMoveNext.Value)
                {
                    CurrentPage.Value = TotalPageCount.Value;
                    LoadCurrentPage();
                }
            });
        }

        private void LoadCurrentPage()
        {
            this.Clear();
            WholeList.Skip((CurrentPage.Value - 1) * ItemsPerPage.Value).Take(ItemsPerPage.Value).ToList().ForEach(x => Add(x));

            CanMoveNext.Value = (CurrentPage.Value < TotalPageCount.Value);
            CanMovePrevious.Value = CurrentPage.Value > 1;
        }

        public void MoveNext()
        {
            MoveNextCommand.Execute(null);
        }

        public void MovePrevious()
        {
            MovePreviousCommand.Execute(null);
        }
    }
}
