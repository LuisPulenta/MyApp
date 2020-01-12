using System;
using Xamarin.Forms;

namespace MyApp.Prism.Views
{
    public partial class VisitDetailPage : ContentPage
    {
        public VisitDetailPage()
        {
            InitializeComponent();
        }
        async void OnButtonClicked0(object sender, EventArgs args)
        {
            note.Text = "0";
        }
        async void OnButtonClicked1(object sender, EventArgs args)
        {
            note.Text = "1";
        }
        async void OnButtonClicked2(object sender, EventArgs args)
        {
            note.Text = "2";
        }
        async void OnButtonClicked3(object sender, EventArgs args)
        {
            note.Text = "3";
        }
        async void OnButtonClicked4(object sender, EventArgs args)
        {
            note.Text = "N/C";
        }
    }
}
