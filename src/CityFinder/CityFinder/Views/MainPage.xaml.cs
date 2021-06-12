using System;
using CityFinder.ViewModels;
using Xamarin.Forms;

namespace CityFinder
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;
        double _bottomSheetTranslationY;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }

        void SearchBar_Focused(object sender, FocusEventArgs e) => OpenBottomSheet();

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't y + e.TotalY pan beyond the wrapped user interface element bounds.
                    var translateY = Math.Max(Math.Min(0, _bottomSheetTranslationY + e.TotalY), -Math.Abs((Height * .25) - Height));
                    BottomSheet.TranslateTo(BottomSheet.X, translateY, 20);
                    break;
                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    _bottomSheetTranslationY = BottomSheet.TranslationY;

                    //at the end of the event - snap to the closest location
                    var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(GetClosestLockState(e.TotalY + _bottomSheetTranslationY)));

                    //depending on Swipe Up or Down - change the snapping animation
                    if (IsSwipeUp(e))
                    {
                        BottomSheet.TranslateTo(BottomSheet.X, finalTranslation, 250, Easing.SpringIn);
                    }
                    else
                    {
                        BottomSheet.TranslateTo(BottomSheet.X, finalTranslation, 250, Easing.SpringOut);
                    }

                    //dismiss the keyboard after a transition
                    SearchBar.Unfocus();
                    _bottomSheetTranslationY = BottomSheet.TranslationY;

                    break;
            }

        }

        public bool IsSwipeUp(PanUpdatedEventArgs e) => e.TotalY < 0;

        //TO-DO: Make this cleaner
        public double GetClosestLockState(double TranslationY)
        {
            //Play with these values to adjust the locking motions - this will change depending on the amount of content ona  apge
            var lockStates = new double[] { 0, .5, .85 };

            //get the current proportion of the sheet in relation to the screen
            var distance = Math.Abs(TranslationY);
            var currentProportion = distance / Height;

            //calculate which lockstate it's the closest to
            var smallestDistance = 10000.0;
            var closestIndex = 0;
            for (var i = 0; i < lockStates.Length; i++)
            {
                var state = lockStates[i];
                var absoluteDistance = Math.Abs(state - currentProportion);
                if (absoluteDistance < smallestDistance)
                {
                    smallestDistance = absoluteDistance;
                    closestIndex = i;
                }
            }

            var selectedLockState = lockStates[closestIndex];
            var TranslateToLockState = GetProportionCoordinate(selectedLockState);

            return TranslateToLockState;
        }

        public double GetProportionCoordinate(double proportion) => proportion * Height;

        void OpenBottomSheet()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(GetProportionCoordinate(.85)));
            BottomSheet.TranslateTo(BottomSheet.X, finalTranslation, 150, Easing.SpringIn);
        }
    }
}
