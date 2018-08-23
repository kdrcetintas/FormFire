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
using FormFire.Core;

namespace FormFire.Examples
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            FormFireManager<Form>.Instance.OpenMainFormWithClosePrompt<SampleForm>("Are you sure for close this application", "FormFire.Examples");

            Application.Run();
        }
    }
}
