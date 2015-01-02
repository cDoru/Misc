using System;
using System.Collections.Generic;
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
using Microsoft.Practices.Prism.Events;

namespace GenericEventSubscriber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EventList _eventList;
        private readonly EventAggregator _eventAggregator;

        public MainWindow()
        {
            InitializeComponent();

            _eventAggregator = new EventAggregator();

            _eventAggregator.GetEvent<HideAllDialogsEvent>().Subscribe(MyAction, ThreadOption.UIThread, true, o => true);

            _eventList = new EventList(_eventAggregator);
            _eventList.Register<HideAllDialogsEvent, object>(MyAction);
        }

        private void MyAction(object ob)
        {
            Text.Text = ob.ToString();
        }

        private void Subscribe_OnClick(object sender, RoutedEventArgs e)
        {
            _eventList.Subscribe();
        }

        private void Unsubscribe_OnClick(object sender, RoutedEventArgs e)
        {
            _eventList.Unsubscribe();
        }

        private void Event_OnClick(object sender, RoutedEventArgs e)
        {
            _eventAggregator.GetEvent<HideAllDialogsEvent>().Publish(TextBox.Text);
        }
    }

    public class EventList : IDisposable
    {
        private readonly EventAggregator _eventAggregator;

        public EventList(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        readonly List<EventSubscription> _subscriptions = new List<EventSubscription>();

        public void Register<T, TPayload>(
            Action<TPayload> action)
            where T : CompositePresentationEvent<TPayload>, new()
        {
            AddSubscription<T, TPayload>(
                e => e.GetEvent<T>().Subscribe(action),
                action);
        }

        public void Register<T, TPayload>(
            Action<TPayload> action,
            ThreadOption threadOption)
            where T : CompositePresentationEvent<TPayload>, new()
        {
            AddSubscription<T, TPayload>(
                e => e.GetEvent<T>().Subscribe(action, threadOption),
                action);
        }

        public void Register<T, TPayload>(
            Action<TPayload> action,
            ThreadOption threadOption,
            bool keepSubscriberReferenceAlive)
            where T : CompositePresentationEvent<TPayload>, new()
        {
            AddSubscription<T, TPayload>(
                e => e.GetEvent<T>().Subscribe(action, threadOption, keepSubscriberReferenceAlive),
                action);
        }

        public void Register<T, TPayload>(
            Action<TPayload> action,
            ThreadOption threadOption,
            bool keepSubscriberReferenceAlive,
            Predicate<TPayload> filter)
            where T : CompositePresentationEvent<TPayload>, new()
        {
            AddSubscription<T, TPayload>(
                e => e.GetEvent<T>().Subscribe(action, threadOption, keepSubscriberReferenceAlive, filter),
                action);
        }

        private void AddSubscription<T, TPayload>(Action<EventAggregator> subscribe, Action<TPayload> action)
            where T : CompositePresentationEvent<TPayload>, new()
        {
            _subscriptions.Add(new EventSubscription(
                subscribe,
                e => e.GetEvent<T>().Unsubscribe(action)
                ));
        }

        public void Subscribe()
        {
            _subscriptions.ForEach(e => e.Subscribe(_eventAggregator));
        }

        public void Unsubscribe()
        {
            _subscriptions.ForEach(e => e.Unsubscribe(_eventAggregator));
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }

    public class EventSubscription
    {
        private readonly Action<EventAggregator> _subscribe;
        private readonly Action<EventAggregator> _unsubscribe;

        public EventSubscription(Action<EventAggregator> subscribe, Action<EventAggregator> unsubscribe)
        {
            _unsubscribe = unsubscribe;
            _subscribe = subscribe;
        }

        public void Subscribe(EventAggregator eventAggregator)
        {
            _subscribe(eventAggregator);
        }

        public void Unsubscribe(EventAggregator eventAggregator)
        {
            _unsubscribe(eventAggregator);
        }
    }


    public class HideAllDialogsEvent : CompositePresentationEvent<object>
    {
    }
}
