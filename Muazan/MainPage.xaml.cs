using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Muazan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void PlayFajrAdhan_Clicked(System.Object sender, System.EventArgs e)
        {
            PlayAdhan(true);
        }

        void PlayAdhan_Clicked(System.Object sender, System.EventArgs e)
        {
            PlayAdhan(false);
        }

        private void PlayAdhan(bool isFajr)
        {
            var prefix = isFajr ? "Fajr-" : "";
            var fileName = $"{prefix}Adhan-Makkah.mp3";
            DependencyService.Get<IAudioPlayer>().PlayAudioFile(fileName);
        }
    }
}
