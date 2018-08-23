// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FormFire.Core.Exceptions;
using FormFire.Core.Helpers;

namespace FormFire.Core
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

    public class FireForm<T> where T : Form, new()
    {
        public FireForm(object mainForm)
        {
            this.MainForm = mainForm;
            this.History = new List<FireFormHistory>();
            this.History.Add(new FireFormHistory("FireForm is created"));
        }

        /// <summary>
        /// Call the show method for MainForm object
        /// </summary>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <typeparam name="TU"></typeparam>
        public void Show()
        {
            try
            {
                ((T)this.MainForm).Show();
            }
            catch (Exception e)
            {
                // We are throw a custom exception for log the exceptions.
                throw new FormFireExceptions.ShowFormInstanceException(e.Message, e);
            }
        }

        public TU Form<TU>()
        {
            return (TU) this.MainForm;
        }

        /// <summary>
        /// MainForm is an object, not generic type because we cant deal with two generic types of detail form type and base form type at static instance list.
        /// </summary>
        public object MainForm
        {
            get
            {
                return this.MainFormObject;
            }
            set
            {
                this.MainFormObject = value;
                this.AttachEvents<T>(this.MainFormObject);
            }
        }

        /// <summary>
        /// Private object property for seperate public and private properties
        /// </summary>
        private object MainFormObject { get; set; }

        /// <summary>
        /// Determine if MainForm object's Shown event is called
        /// </summary>
        public bool IsShowed { get; set; }

        /// <summary>
        /// Determine if MainForm object's is disposed
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        /// Determine if MainForm object's Load event is called
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// Determine if MainForm object's GUI is minimized
        /// </summary>
        public bool IsMinimized { get; set; }

        /// <summary>
        /// Determine if MainForm object's is the Main form on application
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Determine if MainForm object's GUI is maximized
        /// </summary>
        public bool IsMaximized { get; set; }

        /// <summary>
        /// Determine if MainForm object's has prompt on FormClosing event
        /// </summary>
        public bool IsHasPrompt { get; set; }

        /// <summary>
        /// If MainForm object's FormClosing event prompt title
        /// </summary>
        public string PromptTitle { get; set; }

        /// <summary>
        /// If MainForm object's FormClosing event prompt message
        /// </summary>
        public string PromptMessage { get; set; }

        /// <summary>
        /// Keeping the history objects of list for fireform.
        /// </summary>
        public List<FireFormHistory> History { get; set; }

        private void DetachEvents<TU>(object sender) where TU : T, new()
        {
            ((TU)sender).FormClosing -= FormFireManager_OnFormClosing;
            ((TU)sender).FormClosed -= FormFireManager_OnFormClosed;
            ((TU)sender).Shown -= FormFireManager_OnShown;
            ((TU)sender).Load -= FormFireManager_OnLoad;
        }

        private void AttachEvents<TU>(object sender) where TU : T, new()
        {
            //this.DetachEvents<TU>(sender);

            ((TU)sender).FormClosing += FormFireManager_OnFormClosing;
            ((TU)sender).FormClosed += FormFireManager_OnFormClosed;
            ((TU)sender).Shown += FormFireManager_OnShown;
            ((TU)sender).Load += FormFireManager_OnLoad;
            ((TU)sender).Resize += FormFireManager_OnResize;
        }

        #region Event Childs
        private void FormFireManager_OnResize(object sender, EventArgs e)
        {
            if (((T)sender).WindowState == FormWindowState.Minimized)
            {
                this.History.Add(new FireFormHistory("Form is minimized"));
            }
            else if (((T)sender).WindowState != FormWindowState.Minimized)
            {
                this.History.Add(new FireFormHistory("Form is come to visible"));
            }
            else if (((T)sender).WindowState == FormWindowState.Maximized)
            {
                this.History.Add(new FireFormHistory("Form is maximized"));
            }
        }

        private void FormFireManager_OnLoad(object sender, EventArgs e)
        {
            this.History.Add(new FireFormHistory("Form is initialized"));
        }

        private void FormFireManager_OnShown(object sender, EventArgs e)
        {
            this.History.Add(new FireFormHistory("Form is showed"));
        }

        private void FormFireManager_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            this.History.Add(new FireFormHistory("Form is disposed"));
        }

        private void FormFireManager_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            this.History.Add(new FireFormHistory("Form closing event is called"));
        }
        #endregion
    }
}
