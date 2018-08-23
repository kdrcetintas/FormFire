using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FormFire.Core.Exceptions;

namespace FormFire.Core.Helpers
{
    public class FireForm<T> where T : Form, new()
    {
        public FireForm(object mainForm)
        {
            MainForm = mainForm;
            History = new List<FireFormHistory>();
            History.Add(new FireFormHistory("FireForm is created"));
        }

        /// <summary>
        ///     MainForm is an object, not generic type because we cant deal with two generic types of detail form type and base
        ///     form type at static instance list.
        /// </summary>
        public object MainForm
        {
            get { return MainFormObject; }
            set
            {
                MainFormObject = value;
                AttachEvents<T>(MainFormObject);
            }
        }

        /// <summary>
        ///     Private object property for seperate public and private properties
        /// </summary>
        private object MainFormObject { get; set; }

        /// <summary>
        ///     Determine if MainForm object's Shown event is called
        /// </summary>
        public bool IsShowed { get; set; }

        /// <summary>
        ///     Determine if MainForm object's is disposed
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        ///     Determine if MainForm object's Load event is called
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        ///     Determine if MainForm object's GUI is minimized
        /// </summary>
        public bool IsMinimized { get; set; }

        /// <summary>
        ///     Determine if MainForm object's is the Main form on application
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        ///     Determine if MainForm object's GUI is maximized
        /// </summary>
        public bool IsMaximized { get; set; }

        /// <summary>
        ///     Determine if MainForm object's has prompt on FormClosing event
        /// </summary>
        public bool IsHasPrompt { get; set; }

        /// <summary>
        ///     If MainForm object's FormClosing event prompt title
        /// </summary>
        public string PromptTitle { get; set; }

        /// <summary>
        ///     If MainForm object's FormClosing event prompt message
        /// </summary>
        public string PromptMessage { get; set; }

        /// <summary>
        ///     Keeping the history objects of list for fireform.
        /// </summary>
        public List<FireFormHistory> History { get; set; }

        /// <summary>
        ///     Call the show method for MainForm object
        /// </summary>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <typeparam name="TU"></typeparam>
        public void Show()
        {
            try
            {
                ((T) MainForm).Show();
            }
            catch (Exception e)
            {
                // We are throw a custom exception for log the exceptions.
                throw new FormFireExceptions.ShowFormInstanceException(e.Message, e);
            }
        }

        public TU Form<TU>()
        {
            return (TU) MainForm;
        }

        private void DetachEvents<TU>(object sender) where TU : T, new()
        {
            ((TU) sender).FormClosing -= FormFireManager_OnFormClosing;
            ((TU) sender).FormClosed -= FormFireManager_OnFormClosed;
            ((TU) sender).Shown -= FormFireManager_OnShown;
            ((TU) sender).Load -= FormFireManager_OnLoad;
        }

        private void AttachEvents<TU>(object sender) where TU : T, new()
        {
            //this.DetachEvents<TU>(sender);

            ((TU) sender).FormClosing += FormFireManager_OnFormClosing;
            ((TU) sender).FormClosed += FormFireManager_OnFormClosed;
            ((TU) sender).Shown += FormFireManager_OnShown;
            ((TU) sender).Load += FormFireManager_OnLoad;
            ((TU) sender).Resize += FormFireManager_OnResize;
        }

        #region Event Childs

        private void FormFireManager_OnResize(object sender, EventArgs e)
        {
            if (((T) sender).WindowState == FormWindowState.Minimized)
                History.Add(new FireFormHistory("Form is minimized"));
            else if (((T) sender).WindowState != FormWindowState.Minimized)
                History.Add(new FireFormHistory("Form is come to visible"));
            else if (((T) sender).WindowState == FormWindowState.Maximized)
                History.Add(new FireFormHistory("Form is maximized"));
        }

        private void FormFireManager_OnLoad(object sender, EventArgs e)
        {
            History.Add(new FireFormHistory("Form is initialized"));
        }

        private void FormFireManager_OnShown(object sender, EventArgs e)
        {
            History.Add(new FireFormHistory("Form is showed"));
        }

        private void FormFireManager_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            History.Add(new FireFormHistory("Form is disposed"));
        }

        private void FormFireManager_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            History.Add(new FireFormHistory("Form closing event is called"));
        }

        #endregion
    }
}