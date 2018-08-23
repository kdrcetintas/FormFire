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
using System.Windows.Forms;
using FormFire.Core.Exceptions;
using FormFire.Core.Helpers;

namespace FormFire.Core
{
    public class FormFireManager<T> where T : Form, new()
    {
        public FormFireManager()
        {
            InstanceForms = new List<FireForm<T>>();
        }

        private List<FireForm<T>> InstanceForms { get; }
        private static FormFireManager<T> InstanceContainer { get; }

        /// <summary>
        ///     The static instance keeper property. When you access to formanager with specificied generic type, it's will create
        ///     an instance and assign the this property.
        /// </summary>
        public static FormFireManager<T> Instance => (InstanceContainer ?? new FormFireManager<T>());

        #region OpenForm Methods
        /// <summary>
        ///     This method must be first call on each form manager. When this form is closed, FormManager will capture and close
        ///     the application.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenMainForm<TU>() where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            if (InstanceForms.Any())
            {
                throw new FormFireExceptions.CreateFormInstanceException("Main forms must be called on first for them FormFireManager");
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                fireForm.IsMain = true;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     This method must be first call on each form manager. When this form is closed, FormManager will capture and close
        ///     the application.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenMainFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            if (InstanceForms.Any())
            {
                throw new FormFireExceptions.CreateFormInstanceException("Main forms must be called on first for them FormFireManager");
            }
            if (string.IsNullOrEmpty(promptMessage))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            if (string.IsNullOrEmpty(promptTitle))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                fireForm.IsMain = true;
                fireForm.IsHasPrompt = true;
                fireForm.PromptTitle = promptTitle;
                fireForm.PromptMessage = promptMessage;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenForm<TU>() where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Will initialize the specificied form with parameters to ctor and show it. Supports for multiple instances at the
        ///     same time.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenForm<TU>(object[] args) where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>(args);
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            if (string.IsNullOrEmpty(promptMessage))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            if (string.IsNullOrEmpty(promptTitle))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                fireForm.IsHasPrompt = true;
                fireForm.PromptTitle = promptTitle;
                fireForm.PromptMessage = promptMessage;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenFormWithClosePrompt<TU>(object[] args, string promptMessage, string promptTitle) where TU : T, new()
        {
            if (InstanceForms == null)
            {
                throw new FormFireExceptions.FireFormListException("Instance Forms property is not initalized, Probably wrong call FormFireManager");
            }
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if (string.IsNullOrEmpty(promptMessage))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            if (string.IsNullOrEmpty(promptTitle))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            try
            {
                var fireForm = FireFormHelper<T>.Create<TU>(args);
                fireForm.IsHasPrompt = true;
                fireForm.PromptTitle = promptTitle;
                fireForm.PromptMessage = promptMessage;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.CreateFormInstanceException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleForm<TU>() where TU : T, new()
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.Form<TU>().GetType() == typeof(TU));
            if (findedFireForm == null)
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            findedFireForm.Show();
            AttachFireFormEvents(findedFireForm);
            return findedFireForm.Form<TU>();
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            if (string.IsNullOrEmpty(promptMessage))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            if (string.IsNullOrEmpty(promptTitle))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.Form<TU>().GetType() == typeof(TU));
            if (findedFireForm == null)
            {
                var fireForm = FireFormHelper<T>.Create<TU>();
                fireForm.IsHasPrompt = true;
                fireForm.PromptTitle = promptTitle;
                fireForm.PromptMessage = promptMessage;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            findedFireForm.Show();
            AttachFireFormEvents(findedFireForm);
            return findedFireForm.Form<TU>();
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleFormWithClosePrompt<TU>(object[] args, string promptMessage, string promptTitle) where TU : T, new()
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if (string.IsNullOrEmpty(promptMessage))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            if (string.IsNullOrEmpty(promptTitle))
            {
                throw new ArgumentNullException(nameof(promptTitle));
            }
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.Form<TU>().GetType() == typeof(TU));
            if (findedFireForm == null)
            {
                var fireForm = FireFormHelper<T>.Create<TU>(args);
                fireForm.IsHasPrompt = true;
                fireForm.PromptTitle = promptTitle;
                fireForm.PromptMessage = promptMessage;
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            findedFireForm.Show();
            AttachFireFormEvents(findedFireForm);
            return findedFireForm.Form<TU>();
        }

        /// <summary>
        ///     Will initialize the specificied form with parameters to ctor and show it. Doesnt support for multiple instances, if
        ///     you call this method for already pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <exception cref="FormFireExceptions.CreateFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.ShowFormInstanceException"></exception>
        /// <exception cref="FormFireExceptions.FireFormEventException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleForm<TU>(object[] args) where TU : T, new()
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.Form<TU>().GetType() == typeof(TU));
            if (findedFireForm == null)
            {
                var fireForm = FireFormHelper<T>.Create<TU>(args);
                InstanceForms.Add(fireForm);
                fireForm.Show();
                AttachFireFormEvents(fireForm);
                return fireForm.Form<TU>();
            }
            findedFireForm.Show();
            AttachFireFormEvents(findedFireForm);
            return findedFireForm.Form<TU>();
        }
        #endregion

        /// <summary>
        ///     Will return a list of initialized and not disposed forms at the manager type of TU
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of FireForm</returns>
        public List<FireForm<T>> GetForms<TU>()
        {
            return InstanceForms.Where(r => r.IsDisposed == false && r.MainForm.GetType().IsEquivalentTo(typeof(TU))).ToList();
        }

        /// <summary>
        ///     Get all initialized forms by called load event
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of FireForm</returns>
        public List<FireForm<T>> GetInitializedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsInitialized)
                .Select(r => r).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is minimized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of FireForm</returns>
        public List<FireForm<T>> GetMinimizedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsMinimized)
                .Select(r => r).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is not minimized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of FireForm</returns>
        public List<FireForm<T>> GetVisibleForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && !r.IsMinimized)
                .Select(r => r).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is maximized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of FireForm</returns>
        public List<FireForm<T>> GetMaximizedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsMaximized)
                .Select(r => r).ToList();
        }

        /// <summary>
        ///     Call the Close void of all filtered forms
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>a boolean of any form is proccessed on this void</returns>
        public bool CloseForms<TU>() where TU : T, new()
        {
            var findedForms = InstanceForms.Where(r => r.IsDisposed == false && r.GetType() == typeof(TU)).ToList();
            if (!findedForms.Any()) return false;
            findedForms.ForEach(r =>
            {
                ((TU)r.MainForm).Close();
                ((TU)r.MainForm).Dispose();
            });
            return true;
        }

        #region Events

        private void DetachFireFormEvents(FireForm<T> fireForm)
        {
            fireForm.Form<T>().FormClosing -= FormFireManager_OnFormClosing;
            fireForm.Form<T>().FormClosed -= FormFireManager_OnFormClosed;
            fireForm.Form<T>().Shown -= FormFireManager_OnShown;
            fireForm.Form<T>().Load -= FormFireManager_OnLoad;
        }

        private void AttachFireFormEvents(FireForm<T> fireForm)
        {
            try
            {
                if (fireForm == null)
                {
                    throw new ArgumentNullException(nameof(fireForm), "fireForm parameter must be initalized when MainForm property is initalizing");
                }
                DetachFireFormEvents(fireForm);
                fireForm.Form<T>().FormClosing += FormFireManager_OnFormClosing;
                fireForm.Form<T>().FormClosed += FormFireManager_OnFormClosed;
                fireForm.Form<T>().Shown += FormFireManager_OnShown;
                fireForm.Form<T>().Load += FormFireManager_OnLoad;
                fireForm.Form<T>().Resize += FormFireManager_OnResize;
            }
            catch (Exception exception)
            {
                throw new FormFireExceptions.FireFormEventException(exception.Message, exception);
            }
        }

        private void FormFireManager_OnResize(object sender, EventArgs e)
        {
            var findedFireForm =
                InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.Form<T>().Equals(sender));
            if (findedFireForm == null) return;
            if (((T)sender).WindowState == FormWindowState.Minimized)
            {
                findedFireForm.IsMinimized = true;
                findedFireForm.IsMaximized = false;
            }
            else if (((T)sender).WindowState != FormWindowState.Minimized)
            {
                findedFireForm.IsMaximized = false;
                findedFireForm.IsMinimized = false;
            }
            else if (((T)sender).WindowState == FormWindowState.Maximized)
            {
                findedFireForm.IsMinimized = false;
                findedFireForm.IsMaximized = false;
            }
        }

        private void FormFireManager_OnLoad(object sender, EventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm != null) findedFireForm.IsInitialized = true;
        }

        private void FormFireManager_OnShown(object sender, EventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm != null) findedFireForm.IsShowed = false;
        }

        private void FormFireManager_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm == null) return;
            findedFireForm.IsDisposed = true;
            if (findedFireForm.IsMain) Application.Exit();
        }

        private void FormFireManager_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm == null || !findedFireForm.IsHasPrompt) return;
            if (MessageBox.Show(findedFireForm.PromptMessage, findedFireForm.PromptTitle,
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                e.Cancel = true;
        }

        #endregion
    }
}