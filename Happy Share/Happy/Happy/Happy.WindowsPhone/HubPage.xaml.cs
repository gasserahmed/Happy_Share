using Happy.Common;
using Happy.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;

// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace Happy
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private TextBox textBox;
        private string momentText;
        private ListView mylist = new ListView();
        private myClass myclass = new myClass();
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public HubPage()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            mcObject = new myClass();
            mcObject.loadFromDB(mylist);

        }
        public async void addingMoments()
        {
            List<MomentList> momentList = await MomentItem.addItem();
            mylist.ItemsSource = momentList;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
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
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Shows the details of a clicked group in the <see cref="SectionPage"/>.
        /// </summary>
        /// <param name="sender">The source of the click event.</param>
        /// <param name="e">Details about the click event.</param>
        private void GroupSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(SectionPage), groupId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        /// <summary>
        /// Shows the details of an item clicked on in the <see cref="ItemPage"/>
        /// </summary>
        /// <param name="sender">The source of the click event.</param>
        /// <param name="e">Defaults about the click event.</param>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void video_page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Videos_links));
        }
        private void tips_page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(howToBeHappy));
        }
        private void quotes_page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Quotes));
        }
        private void image_page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Images));
        }

        myClass mcObject;
        private void post_button(object sender, RoutedEventArgs e)
        {
            //myclass.DeleteAllmoments();
            if (!textBox.Text.Equals(""))
            {
                //mylist.Items.Clear();
                myclass.addListItem(mylist, textBox);
                //myclass.loadFromDB(mylist);
                addingMoments();
                sharingMomentAsImage(textBox.Text);
            }
            //myclass.loadFromDB(mylist);
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

        private void About_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(about_page));
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