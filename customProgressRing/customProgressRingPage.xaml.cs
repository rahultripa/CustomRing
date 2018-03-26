using System;
using Xamarin.Forms;

namespace customProgressRing
{
    public partial class customProgressRingPage : ContentPage
    {
        public customProgressRingPage()
        {
            InitializeComponent();
        }
        void Progress_Tabbed(object sender, System.EventArgs e)
        {
            progressRing.ProgressValue = 0;
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(.2), OnTimer);
        }

        private bool OnTimer()
        {
            progressRing.ProgressValue = progressRing.ProgressValue + .02;
            return true;
        }
    }
}
