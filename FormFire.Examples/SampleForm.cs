using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FormFire.Core;

namespace FormFire.Examples
{
    public partial class SampleForm : Form
    {
        public SampleForm()
        {
            InitializeComponent();
        }

        private void SampleForm_Load(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.OpenSingleFormWithClosePrompt<AnotherSampleForm>("Are you sure for close this form",
                "FormFire.Examples");
        }
    }
}
