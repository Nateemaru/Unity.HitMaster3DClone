using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace _Scripts.Services.EventBusService
{
    public static class EventBus
    {
        private static Dictionary<Type, SubscribersList<IGlobalSubscriber>> _subscribers
            = new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();
        
        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type t in subscriberTypes)
            {
                if (!_subscribers.ContainsKey(t))
                {
                    _subscribers[t] = new SubscribersList<IGlobalSubscriber>();
                }
                _subscribers[t].Add(subscriber);
            }
        }
        
        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type t in subscriberTypes)
            {
                if (_subscribers.ContainsKey(t))
                    _subscribers[t].Remove(subscriber);
            }
        }
        
        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            if (_subscribers.ContainsKey(typeof(TSubscriber)))
            {
                SubscribersList<IGlobalSubscriber> subscribers = _subscribers[typeof(TSubscriber)];

                if (!subscribers.List.IsNullOrEmpty())
                {
                    subscribers.Executing = true;
                    foreach (IGlobalSubscriber subscriber in subscribers.List)
                    {
                        try
                        {
                            if(subscriber != null)
                                action.Invoke(subscriber as TSubscriber);
                            else
                                Unsubscribe(subscriber as TSubscriber);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }
                    }
                    subscribers.Executing = false;
                    subscribers.Cleanup();
                }
            }
        }
    }
}