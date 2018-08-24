// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23
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