using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace RxExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            new[] {"A", "b", "c"}
                .ToObservable()
                .Convert(ToLower)
                .ToLower()
                .ToUpper()
                .Subscribe(Console.WriteLine);

            Console.ReadLine();
        }

        private static string ToLower(string input)
        {
            return input.ToLower();
        }
    }

    public static class Extensions
    {
        public static IObservable<TU> Convert<T, TU>(this IObservable<T> source, Func<T, TU> convertFunc)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new AnonymousObservable<TU>(observer => source.Subscribe(
                x => observer.OnNext(convertFunc(x)),
                observer.OnError,
                observer.OnCompleted));            
        }

        public static IObservable<string> ToLower(this IObservable<string> source)
        {
            return source.Convert(x => x.ToLower());
        }

        public static IObservable<string> ToUpper(this IObservable<string> source)
        {
            return source.Convert(x => x.ToUpper());
        }
    }

}
