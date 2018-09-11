using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace CarouselView
{
    class CarouselPage : ContentView, IManualCarouselPage
    {

        public event Action PageAppearing;
        public event Action PageDisappearing;
        public event Action PageAppeared;
        public event Action PageDisappeared;

        public CarouselPage() : base()
        {

        }

        #region IManualCarouselPage implementation
        public void OnPageAppearing()
        {
            if (PageAppearing != null)
            {
                PageAppearing();
            }
        }
        public void OnPageDisappearing()
        {
            if (PageDisappearing != null)
            {
                PageDisappearing();
            }
        }
        public void OnPageAppeared()
        {
            if (PageAppeared != null)
            {
                PageAppeared();
            }
        }
        public void OnPageDisappeared()
        {
            if (PageDisappeared != null)
            {
                PageDisappeared();
            }
        }
        #endregion
    }
}
