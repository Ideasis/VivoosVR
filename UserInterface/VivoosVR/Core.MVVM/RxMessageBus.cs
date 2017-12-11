using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public static class RxMessageBus
    {
        private static Subject<IMessageBase> Subject = new Subject<IMessageBase>();

        public static IObservable<T> AsObservable<T>()
        {
            return Subject
                .Where(m => m is T)
                .Select(m => (T)m);
        }

        public static void Send<T>(T message) where T : IMessageBase
        {
            Subject.OnNext(message);
        }

        public static IDisposable Subscribe<T>(Action<T> action) where T : IMessageBase
        {
            return AsObservable<T>().Subscribe<T>(action);
        }

        public static IMessageBase WaitForMessage<T>()
        {
            return Subject.Wait<IMessageBase>();
        }
    }
}
