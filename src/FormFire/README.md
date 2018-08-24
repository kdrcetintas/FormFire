# FormFire

[![Chat](https://badges.gitter.im/FormFire/gitter.png)](https://gitter.im/FormFire/Lobby)
[![Hangfire](https://img.shields.io/nuget/v/FormFire.svg)](https://www.nuget.org/packages?q=formfire)

Dont waste your time for managing forms.

Easy to use for create / show / hide or access to your forms, everywhere in your application.

Supports for multiple form types (Form, XtraForm, etc..)

First Generic T is at manager for your base form class, could be System.Windows.Forms.Form or XtraForm or etc..
Second Generic T is target form class for void operations.

In your Application main void you can open your main form with or without close prompt. 

    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        FormFireManager<Form>.Instance.OpenMainForm<SampleForm>();
        // or
        FormFireManager<Form>.Instance.OpenMainFormWithClosePrompt<SampleForm>("Are you sure for close this application", "FormFire.Examples");

        Application.Run();
    }

You can still access the same instance of FormFireManager and open another form with or without single instance.
    
    private void SampleForm_Load(object sender, EventArgs e)
    {
        FormFireManager<Form>.Instance.OpenSingleForm<AnotherSampleForm>();
    }
    
Everywhere in your application you can access to your pre opened forms at the manager.

    private void button1_Click(object sender, EventArgs e)
    {
        FormFireManager<Form>.Instance.GetForms<SampleForm>().FirstOrDefault().Form<SampleForm>().Hide();
    }