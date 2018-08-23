using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FormFire.Core
{
    public class FormFireManager<T> where T : Form, new()
    {
        public FormFireManager()
        {
            InstanceForms = new List<FireForm>();
        }

        private List<FireForm> InstanceForms { get; }
        private static FormFireManager<T> InstanceContainer { get; set; }

        /// <summary>
        ///     The static instance keeper property. When you access to formanager with specificied generic type, it's will create
        ///     an instance and assign the this property.
        /// </summary>
        public static FormFireManager<T> Instance
        {
            get
            {
                if (InstanceContainer == null) InstanceContainer = new FormFireManager<T>();
                ;
                return InstanceContainer;
            }
        }

        /// <summary>
        ///     This method must be first call on each form manager. When this form is closed, FormManager will capture and close
        ///     the application.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenMainForm<TU>() where TU : T, new()
        {
            if (InstanceForms.Count() != 0) return null;

            var kForm = new FireForm
            {
                MainForm = Activator.CreateInstance<TU>(),
                IsDisposed = false,
                IsShowed = false,
                IsMain = true
            };
            ((TU)kForm.MainForm).Show();

            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     This method must be first call on each form manager. When this form is closed, FormManager will capture and close
        ///     the application.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenMainFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            if (InstanceForms.Count() != 0) return null;

            var kForm = new FireForm
            {
                MainForm = Activator.CreateInstance<TU>(),
                IsDisposed = false,
                IsShowed = false,
                IsMain = true,
                IsHasPrompt = true,
                PromptMessage = promptMessage,
                PromptTitle = promptTitle
            };
            ((TU)kForm.MainForm).Show();

            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenForm<TU>() where TU : T, new()
        {
            var kForm = new FireForm
            {
                MainForm = Activator.CreateInstance<TU>(),
                IsDisposed = false,
                IsShowed = false
            };
            ((TU)kForm.MainForm).Show();
            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            var kForm = new FireForm
            {
                MainForm = Activator.CreateInstance<TU>(),
                IsDisposed = false,
                IsShowed = false,
                IsHasPrompt = true,
                PromptTitle = promptTitle,
                PromptMessage = promptMessage
            };
            ((TU)kForm.MainForm).Show();
            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form with parameters to ctor and show it. Supports for multiple instances at the
        ///     same time.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenForm<TU>(object[] args) where TU : T, new()
        {
            var realForm = (TU)Activator.CreateInstance(typeof(TU), args);
            var kForm = Activator.CreateInstance<FireForm>();
            kForm.MainForm = realForm;
            kForm.IsDisposed = false;
            ((TU)kForm.MainForm).Show();
            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form and show it. Supports for multiple instances at the same time.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized form object</returns>
        public TU OpenFormWithClosePrompt<TU>(object[] args, string promptMessage, string promptTitle)
            where TU : T, new()
        {
            var kForm = new FireForm
            {
                MainForm = Activator.CreateInstance(typeof(TU), args),
                IsDisposed = false,
                IsShowed = false,
                IsHasPrompt = true,
                PromptTitle = promptTitle,
                PromptMessage = promptMessage
            };
            ((TU)kForm.MainForm).Show();
            InstanceForms.Add(kForm);

            AttachEvents<TU>((TU)kForm.MainForm);

            return (TU)kForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleForm<TU>() where TU : T, new()
        {
            var tType = typeof(TU);
            var findedForm =
                InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.GetType() == tType);
            if (findedForm == null)
            {
                var realForm = (TU)Activator.CreateInstance(typeof(TU));
                var kForm = Activator.CreateInstance<FireForm>();
                kForm.MainForm = realForm;
                kForm.IsDisposed = false;
                ((TU)kForm.MainForm).Show();
                InstanceForms.Add(kForm);

                AttachEvents<TU>((TU)kForm.MainForm);

                return (TU)kForm.MainForm;
            }

            ((TU)findedForm.MainForm).Show();


            AttachEvents<TU>((TU)findedForm.MainForm);

            return (TU)findedForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleFormWithClosePrompt<TU>(string promptMessage, string promptTitle) where TU : T, new()
        {
            var tType = typeof(TU);
            var findedForm =
                InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.GetType() == tType);
            if (findedForm == null)
            {
                var realForm = (TU)Activator.CreateInstance(typeof(TU));
                var kForm = Activator.CreateInstance<FireForm>();
                kForm.MainForm = realForm;
                kForm.IsDisposed = false;
                kForm.IsHasPrompt = true;
                kForm.PromptTitle = promptTitle;
                kForm.PromptMessage = promptMessage;
                ((TU)kForm.MainForm).Show();
                InstanceForms.Add(kForm);


                AttachEvents<TU>((TU)kForm.MainForm);

                return (TU)kForm.MainForm;
            }

            ((TU)findedForm.MainForm).Show();


            AttachEvents<TU>((TU)findedForm.MainForm);

            return (TU)findedForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form. Doesnt support for multiple instances, if you call this method for already
        ///     pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleFormWithClosePrompt<TU>(object[] args, string promptMessage, string promptTitle)
            where TU : T, new()
        {
            var tType = typeof(TU);
            var findedForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.GetType() == tType);
            if (findedForm == null)
            {
                var realForm = (TU)Activator.CreateInstance(typeof(TU), args);
                var kForm = Activator.CreateInstance<FireForm>();
                kForm.MainForm = realForm;
                kForm.IsDisposed = false;
                kForm.IsHasPrompt = true;
                kForm.PromptTitle = promptTitle;
                kForm.PromptMessage = promptMessage;
                ((TU)kForm.MainForm).Show();
                InstanceForms.Add(kForm);


                AttachEvents<TU>((TU)kForm.MainForm);

                return (TU)kForm.MainForm;
            }

            ((TU)findedForm.MainForm).Show();


            AttachEvents<TU>((TU)findedForm.MainForm);

            return (TU)findedForm.MainForm;
        }

        /// <summary>
        ///     Will initialize the specificied form with parameters to ctor and show it. Doesnt support for multiple instances, if
        ///     you call this method for already pre opened form or hidden form, The method will reopen the pre form again.
        /// </summary>
        /// <typeparam name="TU">Your specificied form derrived from System.Windows.Forms.Form class</typeparam>
        /// <returns>Returns initalized / finded form object</returns>
        public TU OpenSingleForm<TU>(object[] args) where TU : T, new()
        {
            var tType = typeof(TU);
            var findedForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.GetType() == tType);
            if (findedForm == null)
            {
                var realForm = (TU)Activator.CreateInstance(typeof(TU), args);
                var kForm = Activator.CreateInstance<FireForm>();
                kForm.MainForm = realForm;
                kForm.IsDisposed = false;
                ((TU)kForm.MainForm).Show();
                InstanceForms.Add(kForm);

                AttachEvents<TU>((TU)kForm.MainForm);

                return (TU)kForm.MainForm;
            }

            ((TU)findedForm.MainForm).Show();
            return (TU)findedForm.MainForm;
        }

        /// <summary>
        ///     Will return a list of initialized and not disposed forms at the manager type of TU
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of TU</returns>
        public List<TU> GetForms<TU>()
        {
            return InstanceForms.Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU))
                .Select(r => (TU)r.MainForm).ToList();
        }

        /// <summary>
        ///     Get all initialized forms by called load event
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of TU</returns>
        public List<TU> GetInitializedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsInitialized)
                .Select(r => (TU)r.MainForm).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is minimized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of TU</returns>
        public List<TU> GetMinimizedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsMinimized)
                .Select(r => (TU)r.MainForm).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is not minimized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of TU</returns>
        public List<TU> GetVisibleForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && !r.IsMinimized)
                .Select(r => (TU)r.MainForm).ToList();
        }

        /// <summary>
        ///     Get all minimized forms by called Resize event and windowstate is maximized
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>List of TU</returns>
        public List<TU> GetMaximizedForms<TU>()
        {
            return InstanceForms
                .Where(r => r.IsDisposed == false && r.MainForm.GetType() == typeof(TU) && r.IsMaximized)
                .Select(r => (TU)r.MainForm).ToList();
        }

        /// <summary>
        ///     Call the Close void of all filtered forms
        /// </summary>
        /// <typeparam name="TU">Filter forms by specificied type</typeparam>
        /// <returns>a boolean of any form is proccessed on this void</returns>
        public bool CloseForms<TU>() where TU : T, new()
        {
            var findedForms = InstanceForms.Where(r => r.IsDisposed == false && r.GetType() == typeof(TU)).ToList();
            if (findedForms.Any())
            {
                findedForms.ForEach(r => { ((TU)r.MainForm).Close(); });
                return true;
            }

            return false;
        }

        #region Events

        private void DetachEvents<TU>(object sender) where TU : T, new()
        {
            ((TU)sender).FormClosing -= FormFireManager_OnFormClosing;
            ((TU)sender).FormClosed -= FormFireManager_OnFormClosed;
            ((TU)sender).Shown -= FormFireManager_OnShown;
            ((TU)sender).Load -= FormFireManager_OnLoad;
        }

        private void AttachEvents<TU>(object sender) where TU : T, new()
        {
            DetachEvents<TU>(sender);
            ((TU)sender).FormClosing += FormFireManager_OnFormClosing;
            ((TU)sender).FormClosed += FormFireManager_OnFormClosed;
            ((TU)sender).Shown += FormFireManager_OnShown;
            ((TU)sender).Load += FormFireManager_OnLoad;
            ((TU)sender).Resize += FormFireManager_OnResize;
        }

        private void FormFireManager_OnResize(object sender, EventArgs e)
        {
            if (((T)sender).WindowState == FormWindowState.Minimized)
            {
                var findedFireForm =
                    InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
                if (findedFireForm == null) return;
                ;
                findedFireForm.IsMinimized = true;
                findedFireForm.IsMaximized = false;
            }
            else if (((T)sender).WindowState != FormWindowState.Minimized)
            {
                var findedFireForm =
                    InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
                if (findedFireForm == null) return;
                ;
                findedFireForm.IsMaximized = false;
                findedFireForm.IsMinimized = false;
            }
            else if (((T)sender).WindowState == FormWindowState.Maximized)
            {
                var findedFireForm =
                    InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
                if (findedFireForm == null) return;
                ;
                findedFireForm.IsMinimized = false;
                findedFireForm.IsMaximized = false;
            }
        }

        private void FormFireManager_OnLoad(object sender, EventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm != null) findedFireForm.IsInitialized = true;
            ;
        }

        private void FormFireManager_OnShown(object sender, EventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm != null) findedFireForm.IsShowed = false;
            ;
        }

        private void FormFireManager_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm == null) return;
            ;
            findedFireForm.IsDisposed = true;
            if (findedFireForm.IsMain) Application.Exit();
            ;
        }

        private void FormFireManager_OnFormClosing(object sender, FormClosingEventArgs e)
        {
            var findedFireForm = InstanceForms.FirstOrDefault(r => r.IsDisposed == false && r.MainForm.Equals(sender));
            if (findedFireForm == null || !findedFireForm.IsHasPrompt) return;
            ;
            if (MessageBox.Show(findedFireForm.PromptMessage, findedFireForm.PromptTitle,
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                e.Cancel = true;
        }

        #endregion
    }
}