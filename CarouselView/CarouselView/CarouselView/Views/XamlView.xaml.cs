using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarouselView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamlView : ContentPage
    {
        private Point? _dimensions;

        private DateTime _timerStart;
        ManualCarouselView box3;

        TimeWrapper timer;
        public XamlView()
        {
            InitializeComponent();
            timer = new TimeWrapper(new TimeSpan(0, 0, 1), true, TimerElapsedEvt);
        }
        private void TimerElapsedEvt()
        {
            var secondsSinceStart = GetSecondsSinceTimerStart;
            if (secondsSinceStart % 2 == 0)
            {
                box3.AdvancePage(1);
            }
        }

        private int GetSecondsSinceTimerStart
        {
            get
            {
                TimeSpan lapsedTime = DateTime.Now - _timerStart;
                return lapsedTime.Seconds;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _timerStart = DateTime.Now;
            timer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            timer.Stop();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!_dimensions.HasValue || (_dimensions.Value.X != width && _dimensions.Value.Y != height))
            {
                _dimensions = new Point(width, height);
                Content = GenerateLayout();
            }
        }

        private View GenerateLayout()
        {
            box3 = new ManualCarouselView { Pages = new List<Layout>() }; // Double-Width Live-Tile

            SetupBox3(box3);

            Content = new ContentView
            {
                Content = box3
            };
            return new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = Content,
            };
        }

        private void SetupBox3(ManualCarouselView box)
        {
            Label lb1 = new Label
            {
                TextColor = Color.Black,
                Text = "Title 4"
            };
            Label lb2 = new Label
            {
                TextColor = Color.White,
                Text = "Title 5"
            };
            ContentView pg1 = new ContentView
            {
                Padding = new Thickness(5),
                BackgroundColor = Color.FromHex("#92E9DC"),
                Content = lb1
            };
            ContentView pg2 = new ContentView
            {
                Padding = new Thickness(5),
                BackgroundColor = Color.FromHex("#399A8C"),
                Content = lb2
            };

            box.Pages.Add(pg1);
            box.Pages.Add(pg2);
            box.Initialise(0);
        }
    }
}