// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FormFire.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FormFire.Tests
{
    [TestClass]
    public class FormFireManager
    {
        [TestMethod]
        public void TestInstanceContainer()
        {
            FormFireManager<Form>.Instance.OpenForm<TestForm>();
            FormFireManager<SubFormBase>.Instance.OpenForm<TestForm>();
            Assert.IsTrue(FormFireManager<Form>.Instance.GetForms<TestForm>().Any());
            Assert.IsTrue(FormFireManager<SubFormBase>.Instance.GetForms<TestForm>().Any());
        }


        [TestMethod]
        public void TestOpenFormMethods()
        {
            List<TestForm> formList = new List<TestForm>();
            formList.Add(FormFireManager<Form>.Instance.OpenForm<TestForm>());
            formList.Add(FormFireManager<Form>.Instance.OpenFormWithClosePrompt<TestForm>("Prompt message", "Prompt title"));
            formList.Add(FormFireManager<Form>.Instance.OpenForm<TestForm>());
            Assert.IsTrue(formList.Any());
        }

        [TestMethod]
        public void TestGiveFormMethods()
        {
            FormFireManager<Form>.Instance.OpenForm<TestForm>();
            Assert.IsTrue(FormFireManager<Form>.Instance.GetForms<TestForm>().Any());
        }

        [TestMethod]
        public void TestCreateFormsOnlyMethods()
        {
            var createdForm = FormFireManager<Form>.Instance.CreateFormOnly<TestForm>();
            createdForm.Show();
            Assert.IsNotNull(createdForm);
        }

    }
}