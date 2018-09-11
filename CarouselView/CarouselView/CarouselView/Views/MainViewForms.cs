using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace CarouselView
{
    class MainViewForms:ContentPage
    {
        private Point? _dimensions;
        private Grid _baseLayout;
        private DateTime _timerStart;

        // Content variable definitions
       
        ManualCarouselView box3;
        
        TimeWrapper timer;
        public MainViewForms()
        {
            Content = new ContentView();
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

        
        private void SetupBox3(ManualCarouselView box)
        {
            Image lb1 = new Image
            {
                HeightRequest = 60,
                Source = "ongone.png"
            };
            Image lb2 = new Image
            {
                HeightRequest = 60,
                Source = "ong.jpg"
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

        private View GenerateLayout()
        {
            //--------------------Forms
            var Userlbl = new Label()
            {
                Text = "Introduce tu nombre"
            };
            var Userlblcon = new Label()
            {
                Text = "Introduce tu Contraseña"
            };
            var entrylbl = new Entry()
            {
                Placeholder = "Nombre completo"
            };
            var entrylblCon = new Entry()
            {
                IsPassword= true,
                Text="Contraseña"
            };

            var buttonCon = new Button()
            {
                CornerRadius=30,
                Text ="Iniciar Sesión",
                BackgroundColor = Color.FromHex("#3498db")
            };
            //--------------------
            _baseLayout = new Grid()
            {
                RowDefinitions = new RowDefinitionCollection {
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) },
                }
            };
            
            box3 = new ManualCarouselView { Pages = new List<Layout>() }; // Double-Width Live-Tile
            SetupBox3(box3);
            _baseLayout.Children.Add(box3, 0,0);
            return new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                Content = new StackLayout
                {
                    Padding = 20,
                    Spacing = 10,
                    HorizontalOptions= LayoutOptions.CenterAndExpand,
                    Children = { _baseLayout, Userlbl , entrylbl,
                    Userlblcon,entrylblCon,buttonCon}
                },
            };
        }
        
    }
}
