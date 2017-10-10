using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Happy.Data;
using Happy.Common;
using SQLite;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.Graphics.Display;
// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace Happy
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    /// 
    public sealed partial class HubPage : Page
    {
        private  TextBox textBox;
        private string momentText;
        private ListView mylist=new ListView();
        private myClass myclass=new myClass();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        /// <summary>
        /// Gets the NavigationHelper used to aid in navigation and process lifetime management.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the DefaultViewModel. This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            mcObject = new myClass();      
            mcObject.loadFromDB(mylist);
            //addingMoments();
            //momentsList.Source = mcObject.RefreshItems();
        }
        public async void addingMoments() 
        {
            List<MomentList> momentList = await MomentItem.addItem();
            mylist.ItemsSource = momentList;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-4");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
         
        }

        /// <summary>
        /// Invoked when a HubSection header is clicked.
        /// </summary>
        /// <param name="sender">The Hub that contains the HubSection whose header was clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }
        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage4));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage2));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage3));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BasicPage1));
        }
        myClass mcObject;
        private  void postButton(object sender, RoutedEventArgs e)
        {
            //myclass.DeleteAllmoments();
            if (!textBox.Text.Equals(""))
            {
                //mylist.Items.Clear();
                myclass.addListItem(mylist, textBox);
                //myclass.loadFromDB(mylist);
                addingMoments();
                //myclass.shareMoment(textBox);
                //shareMoment();
                sharingMomentAsImage(textBox.Text);
            }
            
            
        }
     

        private void post_text_Loaded(object sender, RoutedEventArgs e)
        {
            textBox = sender as TextBox;
        }

        private void my_list_Loaded(object sender, RoutedEventArgs e)
        {
            mylist = sender as ListView;
            //myclass.loadFromDB(mylist);
            addingMoments();
        }

        private void post_text_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
               
                    
               
            }
        }

        private void refreshClick(object sender, RoutedEventArgs e)
        {
           
        }
        
        public async void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string FileTitle = "Happy Share";
            string FileDescription = "Don't look for Happiness. Create It...Share It!"; //This is an optional value.

            DataPackage data = args.Request.Data;
            data.Properties.Title = FileTitle;
            data.Properties.Description = FileDescription;

            DataRequestDeferral waiter = args.Request.GetDeferral();

            try
            {
                StorageFile image = await ApplicationData.Current.LocalFolder.GetFileAsync("Image.png");
                data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(image);
                data.SetBitmap(RandomAccessStreamReference.CreateFromFile(image));

                List<IStorageItem> files = new List<IStorageItem>();
                files.Add(image);
                data.SetStorageItems(files);
            }
            finally
            {
                waiter.Complete();
            }

        }

        private void Share(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MomentList moment = button.DataContext as MomentList;
            momentText = moment.momentText;
            sharingMomentAsImage(momentText);
        }
        private async void sharingMomentAsImage(String momentText)
        {
            RenderedGrid.Visibility = Visibility.Visible;
            quoteText.Text = momentText;
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(RenderedGrid);
            RenderedGrid.Visibility = Visibility.Collapsed;
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Image.png", CreationCollisionOption.ReplaceExisting);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();
            }
            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
            DataRequestedEventArgs>(this.dtm_DataRequested);
            DataTransferManager.ShowShareUI();
        }
        
    }
      
}
