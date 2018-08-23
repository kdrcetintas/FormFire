// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23
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