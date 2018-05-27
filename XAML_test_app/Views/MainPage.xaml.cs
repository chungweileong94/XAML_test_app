using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XAML_test_app.Extendsions;

namespace XAML_test_app.Views
{
    public sealed partial class MainPage : Page
    {
        private bool _firstLoad;
        private Compositor _compositor;

        public MainPage()
        {
            this.InitializeComponent();
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            _firstLoad = true;

            Loaded += (s, e) =>
            {
                if (Windows.System.UserProfile.UserProfilePersonalizationSettings.IsSupported())
                {
                    AutoWallpaperPanel.Visibility = Visibility.Visible;
                }

                RootBorder.SetupHostBlurBackground(((SolidColorBrush)App.Current.Resources["ApplicationPageBackgroundThemeBrush"]).Color);
            };

            //back button click event
            BackButton.Click += (s, e) =>
            {
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame.CanGoBack) rootFrame.GoBack();
            };

            //setup title bar draggable region
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            AppTitleBar.Height = coreTitleBar.Height;
            coreTitleBar.LayoutMetricsChanged += (s, args) => AppTitleBar.Height = coreTitleBar.Height;

            Window.Current.SetTitleBar(AppTitleBar);
        }

        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_firstLoad)
            {
                _firstLoad = false;
                return;
            }

            if (RegionComboBox.SelectedItem != null
                && RegionComboBox.SelectedItem == _SettingsViewModel.RegionCollection.GetDefaultRegion())
            {
                RestartTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                RestartTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var s = (RadioButton)sender;

            int currentSetting = App.Current.RequestedTheme == ApplicationTheme.Dark ? 0 : 1;

            if (s.IsChecked == true && int.Parse(s.Tag.ToString()) != currentSetting)
            {
                ThemeRestartTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ThemeRestartTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void MainContentGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var s = sender as UIElement;
            Visual visual = ElementCompositionPreview.GetElementVisual(s);
            float width = (float)s.RenderSize.Width;
            float height = (float)s.RenderSize.Height;

            visual.Opacity = 0;
            visual.CenterPoint = new System.Numerics.Vector3(width / 2, height / 2, 0);
            visual.Scale = new System.Numerics.Vector3(.5f, .5f, 0);

            var fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeAnimation.Duration = TimeSpan.FromMilliseconds(700);
            fadeAnimation.InsertKeyFrame(1f, 1f);

            var scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(500);
            scaleAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(1, 1, 0));

            visual.StartAnimation("Opacity", fadeAnimation);
            visual.StartAnimation("Scale", scaleAnimation);
        }

        private void CreaditPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var s = sender as UIElement;
            Visual visual = ElementCompositionPreview.GetElementVisual(s);
            float height = (float)s.RenderSize.Height;

            visual.Opacity = 0;
            visual.Offset = new System.Numerics.Vector3(0, height, 0);

            var fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
            fadeAnimation.Duration = TimeSpan.FromMilliseconds(700);
            fadeAnimation.InsertKeyFrame(1f, 1f);

            var offsetAnimation = _compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(500);
            offsetAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(0, 0, 0));

            visual.StartAnimation("Opacity", fadeAnimation);
            visual.StartAnimation("Offset", offsetAnimation);
        }

        private bool NavBackCancel = true;
        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            e.Cancel = NavBackCancel;

            if (e.Cancel)
            {
                NavBackCancel = false;
                double animationDuration = 300;

                #region MainContent Animation
                Visual MainContentVisual = ElementCompositionPreview.GetElementVisual(MainContentGrid);
                float MainContentWidth = (float)MainContentGrid.RenderSize.Width;
                float MainContentHeight = (float)MainContentGrid.RenderSize.Height;

                MainContentVisual.Opacity = 1;
                MainContentVisual.CenterPoint = new System.Numerics.Vector3(MainContentWidth / 2, MainContentHeight / 2, 0);
                MainContentVisual.Scale = new System.Numerics.Vector3(1f, 1f, 0);

                var MainContentfadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                MainContentfadeAnimation.Duration = TimeSpan.FromMilliseconds(animationDuration);
                MainContentfadeAnimation.InsertKeyFrame(1f, 0);

                var MainContentcaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
                MainContentcaleAnimation.Duration = TimeSpan.FromMilliseconds(animationDuration);
                MainContentcaleAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(.5f, .5f, 0));

                MainContentVisual.StartAnimation("Opacity", MainContentfadeAnimation);
                MainContentVisual.StartAnimation("Scale", MainContentcaleAnimation);
                #endregion

                #region CreditPanel Animation
                Visual creaditPanelVisual = ElementCompositionPreview.GetElementVisual(CreaditPanel);
                float creaditPanelHeight = (float)CreaditPanel.RenderSize.Height;

                creaditPanelVisual.Opacity = 1;
                creaditPanelVisual.Offset = new System.Numerics.Vector3(0, 0, 0);

                var creaditPanelVisualFadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                creaditPanelVisualFadeAnimation.Duration = TimeSpan.FromMilliseconds(animationDuration);
                creaditPanelVisualFadeAnimation.InsertKeyFrame(1f, 0);

                var creaditPaneloffsetAnimation = _compositor.CreateVector3KeyFrameAnimation();
                creaditPaneloffsetAnimation.Duration = TimeSpan.FromMilliseconds(animationDuration);
                creaditPaneloffsetAnimation.InsertKeyFrame(1f, new System.Numerics.Vector3(0, creaditPanelHeight, 0));

                creaditPanelVisual.StartAnimation("Opacity", creaditPanelVisualFadeAnimation);
                creaditPanelVisual.StartAnimation("Offset", creaditPaneloffsetAnimation);
                #endregion

                await Task.Delay((int)animationDuration);
                Frame.GoBack();
            }
        }

    }

    #region Converters
    public class AutoWallpaperButtonAvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => int.Parse(value.ToString()) == int.Parse(parameter.ToString()) ? true : false;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => (bool)value ? parameter : null;
    }
    #endregion
}
