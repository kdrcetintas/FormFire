using System;

namespace FormFire.Core.Helpers
{
    public class FireFormHistory
    {
        public FireFormHistory(string actionMessage)
        {
            ActionMessage = actionMessage;
            ActionUtcDateTime = DateTime.UtcNow;
            ActionLocalDateTime = DateTime.Now;
        }

        /// <summary>
        ///     Keep the auto generated action utc date time value
        /// </summary>
        public DateTime ActionUtcDateTime { get; set; }

        /// <summary>
        ///     Keep the auto generated action local date time value
        /// </summary>
        public DateTime ActionLocalDateTime { get; set; }

        /// <summary>
        ///     Keep the ctor parametered string text for description about action
        /// </summary>
        public string ActionMessage { get; set; }
    }
}