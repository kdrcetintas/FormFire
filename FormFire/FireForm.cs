using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormFire.Core
{
    class FireForm
    {
        /// <summary>
        /// MainForm is an object, not generic type because we cant deal with two generic types of detail form type and base form type at static instance list.
        /// </summary>
        public object MainForm { get; set; }

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
    }
}
