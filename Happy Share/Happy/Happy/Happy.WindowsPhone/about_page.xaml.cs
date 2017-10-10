using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Happy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class about_page : Page
    {
        public about_page()
        {
            this.InitializeComponent();
            // logoAnim storybord start
            LogoAnim.Begin();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += OnBackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        //application rate and review
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }


        //application share 
        private void ShareSourceLoad()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
        }

        private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share Happy Share";
            request.Data.Properties.Description = "Great App";
            request.Data.SetText("Happy Share");
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
            ShareSourceLoad();
        }



        //app feedback
        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            // Define Recipient
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Gasser Montaser Saleh",
                Address = "gassermontaser@gmail.com"
            };

            // Create email object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Great App";
            mail.Body = "Happy Share app";


            // Open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }




        //socialmedia click
        private async void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // The URI to launch
            string uriToLaunch = @"https://www.facebook.com/EGAppFactory";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://www.facebook.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void Image_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://twitter.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void TextBlock_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://www.facebook.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private void TextBlock_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            var uri = new Uri("https://twitter.com/EGAppFactory");
            IAsyncOperation<bool> x = Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
       
    }
}
