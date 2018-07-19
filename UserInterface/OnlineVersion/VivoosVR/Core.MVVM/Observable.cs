using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public class Observable<T> : ObservableBase<T>, INotifyPropertyChanged, INotifyPropertyChanging, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        private T _Value;
        string _Error = "";
        private bool _IsValid;

        public string Error
        {
            get { return _Error; }
            set
            {
                _Error = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }
        public Subject<T> InnerValue { get; private set; }
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if (value != _IsValid)
                {
                    PropertyChanging(_IsValid, new PropertyChangingEventArgs(nameof(IsValid)));
                }

                _IsValid = value;

                if (value != _IsValid)
                {
                    PropertyChanged(_IsValid, new PropertyChangedEventArgs(nameof(IsValid)));
                }
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Value))
                {
                    if (Validation != null)
                    {
                        string ret = Validation(Value);
                        IsValid = string.IsNullOrWhiteSpace(ret);
                        return Validation(Value);
                    }
                }

                return null;
            }
        }
        public Func<T, string> Validation { get; private set; }
        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                InnerValue.OnNext(value);
            }
        }

        public Observable()
        {
            PropertyChanged += Dependent_PropertyChanged;
            PropertyChanging += Dependent_PropertyChanging;
            InnerValue = new Subject<T>();

            //Type t = typeof(T);
            //var a = t.GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(Observable<>));
            //if (a.Count() > 0)
            //{
            //    int o = 0;
            //}

            InnerValue.Subscribe(x =>
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
            });

            Value = default(T);
        }

        public Observable(T value)
        {
            PropertyChanged += Dependent_PropertyChanged;
            PropertyChanging += Dependent_PropertyChanging;
            InnerValue = new Subject<T>();

            //Type t = typeof(T);
            //var a = t.GetProperties().Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(Observable<>));

            //if (a.Count() > 0)
            //{
            //    int o = 0;
            //}

            InnerValue.Subscribe(x =>
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
            });

            Value = value;
        }

        void Dependent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        void Dependent_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
        }

        protected override IDisposable SubscribeCore(IObserver<T> observer)
        {
            return InnerValue.Subscribe(observer);
        }

        public void SubscribeValidate(Func<T, string> validation)
        {
            Validation = validation;
        }

        public override string ToString()
        {
            if (Value != null)
            {
                return Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public string Validate()
        {
            if (Validation!=null) return Validation(this.Value);

            return null;
        }
    }
}
