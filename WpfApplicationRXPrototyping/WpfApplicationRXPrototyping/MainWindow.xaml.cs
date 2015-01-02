using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
//using System.Reactive.Concurrency;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using Moq;
using Moq.Language.Flow;

namespace WpfApplicationRXPrototyping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Random random = new Random(DateTime.Now.Millisecond);

        private bool buttonEnabled = true;

        private readonly IObservable<string> observablePropertyChanges; 

        private ObservableCollection<string> collection = new ObservableCollection<string>();

        private IObservableRepository<int> observableRepository;

        public ObservableCollection<string> Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        public bool ButtonEnabled
        {
            get
            {
                return buttonEnabled;
            }
            set
            {
                buttonEnabled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ButtonEnabled"));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            observablePropertyChanges = this.ObservablePropertyChanges();
            
            observableRepository = new ObservableRepository<int>(new TestRepository());
            observableRepository.GetAll().Subscribe((i) => collection.Add(i.ToString()));

            observableRepository.SaveAsync(3);

            this.DataContext = this;
        }

        private string GetString(int time)
        {
            System.Threading.Thread.Sleep(time);
            return string.Format("{0} {1}", time, System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var ob1 = observablePropertyChanges
                .Where(p => p == "ButtonEnabled" && this.ButtonEnabled == false)
                .Timeout(new TimeSpan(0, 0, 3))
                .Subscribe(_ => { }, ex => ButtonEnabled = true);

            ButtonEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public static class Extensions
    {
        public static IObservable<string> ObservablePropertyChanges(this INotifyPropertyChanged source)
        {
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                handler => (sender1, args) => handler(sender1, args),
                x => source.PropertyChanged += x,
                x => source.PropertyChanged -= x)
                .Select(event1 =>
                    event1.EventArgs.PropertyName);
        }


        public static IDisposable SubscribeObservableCollection<T>(this IObservable<T> source,
                                                                   ObservableCollection<T> collection)
        {
            return source.SubscribeObservableCollection(collection, DispatcherPriority.Normal);
        }

        public static IDisposable SubscribeObservableCollection<T>(this IObservable<T> source,
                                                                   ObservableCollection<T> collection,
                                                                   DispatcherPriority dispatcherPriority)
        {
            return source.ObserveOnDispatcher(dispatcherPriority)
                         .Subscribe(s => collection.Add(s));
        }
    }

    public class TestRepository : IRepository<int>
    {
        public IEnumerable<int> GetAll()
        {



            return Enumerable.Range(1, 200);
        }
    }

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }

    public interface IObservableRepository<T>
    {
        IObservable<T> GetAll();
        Task SaveAsync(T entity);
    }

    public class ObservableRepository<T> : IObservableRepository<T>
    {
        private IRepository<T> repository;

        private IObservable<T> observable;

        public ObservableRepository(IRepository<T> repository)
        {
            this.repository = repository;
            observable = repository.GetAll().ToObservable();
        }

        public IObservable<T> GetAll()
        {
            return observable;
        }

        public async Task SaveAsync(T entity)
        {
            //Task.Delay() to simulate some load
            await Task.Delay(4000);
        }
    }

}
