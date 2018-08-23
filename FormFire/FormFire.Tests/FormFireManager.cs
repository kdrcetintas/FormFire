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
    }
}