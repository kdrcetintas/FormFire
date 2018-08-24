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
    public partial class SampleForm : Form
    {
        public SampleForm()
        {
            InitializeComponent();
        }

        private void SampleForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.OpenSingleFormWithClosePrompt<AnotherSampleForm>("Are you sure for close this form",
                "FormFire.Examples");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.OpenSingleForm<AnotherSampleForm>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.OpenForm<AnotherSampleForm>();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.OpenFormWithClosePrompt<AnotherSampleForm>("Are you sure for close this form",
                "FormFire.Examples");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form = FormFireManager<Form>.Instance.CreateFormOnlyWithClosePrompt<AnotherSampleForm>("Are you sure for close this form",
                "FormFire.Examples");
            form.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var form = FormFireManager<Form>.Instance.CreateFormOnly<AnotherSampleForm>();
            form.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var forms = FormFireManager<Form>.Instance.GetForms<AnotherSampleForm>();
            foreach (var form in forms)
                MessageBox.Show(form.MainForm.ToString())
                    ;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var forms = FormFireManager<Form>.Instance.GetMinimizedForms<AnotherSampleForm>();
            foreach (var form in forms)
                MessageBox.Show(form.MainForm.ToString())
                    ;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var forms = FormFireManager<Form>.Instance.GetMaximizedForms<AnotherSampleForm>();
            foreach (var form in forms)
                MessageBox.Show(form.MainForm.ToString())
                    ;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var forms = FormFireManager<Form>.Instance.GetVisibleForms<AnotherSampleForm>();
            foreach (var form in forms)
                MessageBox.Show(form.MainForm.ToString())
                    ;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FormFireManager<Form>.Instance.CloseForms<AnotherSampleForm>();
        }
    }
}
