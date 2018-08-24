// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23
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
    public partial class AnotherSampleForm : Form
    {
        public AnotherSampleForm()
        {
            InitializeComponent();
        }

        private void AnotherSampleForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.GetForms<SampleForm>().FirstOrDefault().Form<SampleForm>().Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.GetForms<SampleForm>().FirstOrDefault().Form<SampleForm>().Show();
        }
    }
}
