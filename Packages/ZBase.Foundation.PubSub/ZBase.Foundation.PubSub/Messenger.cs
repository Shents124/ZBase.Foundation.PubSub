using System;
using System.Buffers;
using Cysharp.Threading.Tasks;
using ZBase.Foundation.PubSub.Internals;
using ZBase.Foundation.Singletons;

namespace ZBase.Foundation.PubSub
{
    public sealed class Messenger : IDisposable
    {
        private readonly SingletonContainer<MessageBroker> _brokers = new();

        public Messenger() : this(ArrayPool<UniTask>.Shared)
        {
        }
    
        public Messenger(ArrayPool<UniTask> taskArrayPool)
        {
            MessageSubscriber = new(_brokers, taskArrayPool);
            MessagePublisher = new(_brokers, taskArrayPool);
            AnonSubscriber = new(_brokers, taskArrayPool);
            AnonPublisher = new(_brokers, taskArrayPool);
        }

        public MessageSubscriber MessageSubscriber { get; }

        public MessagePublisher MessagePublisher { get; }

        public AnonSubscriber AnonSubscriber { get; }

        public AnonPublisher AnonPublisher { get; }

        public void Dispose()
        {
            _brokers.Dispose();
        }
    }
}
