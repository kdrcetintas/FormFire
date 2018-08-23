using System;
using System.Windows.Forms;

namespace FormFire.Core.Helpers
{
    public static class FireFormHelper<T> where T : Form, new()
    {
        public static FireForm<T> Create<TU>(object[] args = null)
        {
            return new FireForm<T>(Activator.CreateInstance(typeof(TU), args))
            {
                IsDisposed = false,
                IsMain = false
            };
        }
    }
}