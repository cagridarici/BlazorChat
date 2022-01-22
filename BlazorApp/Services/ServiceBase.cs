using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public abstract class ServiceBase : IDisposable
    {
        public ServiceBase()
        {

        }

        protected virtual void SubscribeHubMethods()
        {

        }

        protected virtual void UnsubscribeHubMethods()
        {

        }

        protected void InvokeAsyncAction(Action<Task> action)
        {
            try
            {

            }
            catch (Exception e)
            {

            }
        }

        protected virtual void InvokeAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {

            }
        }

        public void Dispose()
        {
            UnsubscribeHubMethods();
        }
    }
}
