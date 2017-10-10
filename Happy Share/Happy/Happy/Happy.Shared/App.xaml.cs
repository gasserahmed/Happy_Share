using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Happy.Common;
using SQLite;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
#if WINDOWS_PHONE_APP
#else
using Windows.UI.ApplicationSettings;
#endif
// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace Happy
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Initializes the singleton instance of the <see cref="App"/> class. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = false;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(HubPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

#if WINDOWS_PHONE_APP
            //none
#else
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
#endif

        }
#if WINDOWS_PHONE_APP
        //none
#else
        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            // Add an About command
            var about = new SettingsCommand("About", "About", (handler) =>
            {

                About flyout = new About();
                flyout.Title = "About";
                flyout.Show();
                //var settings = new SettingsFlyout();
                //settings.Content = new about();
                //settings.HeaderBrush = new SolidColorBrush(_background);
                //settings.Background = new SolidColorBrush(_background);
                //settings.HeaderText = "About";
                //settings.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(about);

           
        }

#endif


#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
        
    }
    public class myClass
    {
        SQLiteAsyncConnection con;
        public myClass() {
            con = new SQLiteAsyncConnection("moment.db");
        }
        public async void addListItem(ListView lv, TextBox textBox)
        {
            con = new SQLiteAsyncConnection("moment.db");
            if (!textBox.Text.Equals(""))
            {
                ListViewItem lstItem = new ListViewItem();
                lstItem.Content = textBox.Text + "\n" + DateTime.Now.ToString(@"M/d/yyyy hh:mm tt");
                //lv.Items.Add(lstItem);
                await con.CreateTableAsync<moment>();
                moment m = new moment()
                {
                    momentText = textBox.Text,
                    momentDate = DateTime.Now.ToString(@"M/d/yyyy hh:mm tt")
                };
                await con.InsertAsync(m);
                textBox.Text = "";
            }
        }
        public async void loadFromDB(ListView lv)
        {
           
                lv.Items.Clear();
                con = new SQLiteAsyncConnection("moment.db");
                await con.CreateTableAsync<moment>();
                var query = con.Table<moment>().OrderByDescending(x =>x.itemID);
                var result = await query.ToListAsync();
           
                foreach (var moment in result)
                {
                    if (moment != null)
                    {
                        ListViewItem lstItem = new ListViewItem();
                        lstItem.Content = moment.momentText + "\n" + moment.momentDate+"\n";
                        lv.Items.Add(lstItem);
                    }
                }
             //await   RefreshItems();
        }
       /* public async Task<List<moment>> RefreshItems()
        {
            con = new SQLiteAsyncConnection("moment.db");
            await con.CreateTableAsync<moment>();
            var query = con.Table<moment>().OrderByDescending(x => x.itemID);
            var result = await query.ToListAsync(); 
            //foreach (var moment in result)
            //{
                //SortAscending(result);
            //    if (moment != null)
            //    {
            //        ListViewItem lstItem = new ListViewItem();
            //        lstItem.Content = moment.momentText + "\n" + moment.momentDate + "\n";
            //        //lv.Items.Add(lstItem);
            //    }
            //}
                return SortAscending(result);
        }*/
        static List<moment> SortAscending(List<moment> list)
        {
            list.Sort((a, b) => a.momentDate.CompareTo(b.momentDate));
            return list;
        }

        public void DeleteAllmoments()
        {
            
                con = new SQLiteAsyncConnection("moment.db");
           
                con.DropTableAsync<moment>();
                con.CreateTableAsync<moment>();
              
        }
        public void tipsAddListItem(ListView lv)
        {
            ListViewItem lstItem = new ListViewItem();
            //lstItem.Content = textBox.Text;
            lv.Items.Add(lstItem);
        }
        TextBox textBox;
        public void shareMoment(TextBox textBox_child)
        {
            textBox = textBox_child;
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
            DataRequestedEventArgs>(this.ShareTextHandler);
            DataTransferManager.ShowShareUI();
        }
        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareTextHandler);
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share Text Example";
            request.Data.Properties.Description = "A demonstration that shows how to share text.";
            request.Data.SetText("Im Happy because..." + textBox.Text);
            //request.Data.SetBitmap();
        }
    }
}