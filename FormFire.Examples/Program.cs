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

            FormFireManager<Form>.Instance.OpenSingleFormWithClosePrompt<AnotherSampleForm>("Are you sure for close this form",
                "FormFire.Examples");

            Application.Run();
        }
    }
}
