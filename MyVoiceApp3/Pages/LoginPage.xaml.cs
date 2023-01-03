using MyVoiceApp3.Models;
using MyVoiceApp3.Utitlys;
using MyVoiceApp3.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyVoiceApp3.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginVM logindata = new LoginVM();
        private Api api = new Api();

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = logindata;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

             if (Preferences.Get("token", "") != null)
                await Shell.Current.GoToAsync($"//{nameof(TalkPage)}");

        }


        private async void btnlogin_Clicked(object sender, EventArgs e)
        {
            if (logindata.Email == null
               || logindata.Password == null)
            {
                ErrPopup popup = new ErrPopup(Message_Box.Blankfield);
                await DisplayAlert("Error", popup.message,"Ok");
            } else
            {
               CheckEmeil checker = new CheckEmeil();
                if (checker.IsValidEmail(logindata.Email) == false)
                {
                    ErrPopup popup = new ErrPopup(Message_Box.Notemail);
                    await DisplayAlert("Error", popup.message, "Ok");

                }
                else
                {
                    Api api = new Api();
                    await api.Login(logindata);
                    if (api.IsOk())
                    {
                        await Shell.Current.GoToAsync($"//{nameof(TalkPage)}");
                    }
                    else {
                        ErrPopup popup = new ErrPopup(Message_Box.Badlogin);
                        await DisplayAlert("Error", popup.message, "Ok");

                    }
                }
                }
                //
                //
            }

        private async void btnregister_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");

        }

        private async void btnnologin_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync($"//{nameof(TalkPage)}");
        }
    }
}