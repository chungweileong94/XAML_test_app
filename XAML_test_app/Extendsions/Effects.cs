using Microsoft.Graphics.Canvas.Effects;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

namespace XAML_test_app.Extendsions
{
    public static class Effects
    {
        public static void SetupHostBlurBackground(this UIElement element, Color color, float tintOpacity = .8f)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                AcrylicBrush blurBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                    TintColor = color,
                    FallbackColor = color,
                    TintOpacity = tintOpacity
                };

                switch (element)
                {
                    case Border b:
                        b.Background = blurBrush;
                        break;
                    case Grid g:
                        g.Background = blurBrush;
                        break;
                    case StackPanel s:
                        s.Background = blurBrush;
                        break;
                }
            }
            else if (ApiInformation.IsMethodPresent("Windows.UI.Composition.Compositor", "CreateHostBackdropBrush"))
            {
                Compositor _compositor = Window.Current.Compositor;
                Visual rootBorderVisual = ElementCompositionPreview.GetElementVisual(element);

                GaussianBlurEffect blurEffect = new GaussianBlurEffect()
                {
                    BlurAmount = 30f,
                    BorderMode = EffectBorderMode.Hard,
                    Optimization = EffectOptimization.Balanced,
                    Source = new ArithmeticCompositeEffect()
                    {
                        MultiplyAmount = 0,
                        Source1Amount = .2f,
                        Source2Amount = tintOpacity,
                        Source1 = new CompositionEffectSourceParameter("backdrop"),
                        Source2 = new ColorSourceEffect()
                        {
                            Color = color
                        }
                    }
                };

                var backdrop = _compositor.CreateHostBackdropBrush();
                var effectFactory = _compositor.CreateEffectFactory(blurEffect);
                var brush = effectFactory.CreateBrush();
                brush.SetSourceParameter("backdrop", backdrop);

                var spriteVisual = _compositor.CreateSpriteVisual();
                spriteVisual.Brush = brush;

                ElementCompositionPreview.SetElementChildVisual(element, spriteVisual);

                var bindSizeAnimation = _compositor.CreateExpressionAnimation("visual.Size");
                bindSizeAnimation.SetReferenceParameter("visual", rootBorderVisual);
                spriteVisual.StartAnimation("Size", bindSizeAnimation);
            }
        }

        public static void SetupBlurBackground(this UIElement element, Color color, float tintOpacity = .8f)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                AcrylicBrush blurBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.Backdrop,
                    TintColor = color,
                    FallbackColor = color,
                    TintOpacity = tintOpacity
                };

                switch (element)
                {
                    case Border b:
                        b.Background = blurBrush;
                        break;
                    case Grid g:
                        g.Background = blurBrush;
                        break;
                    case StackPanel s:
                        s.Background = blurBrush;
                        break;
                }
            }
            else if (ApiInformation.IsMethodPresent("Windows.UI.Composition.Compositor", "CreateBackdropBrush"))
            {
                Compositor _compositor = Window.Current.Compositor;
                Visual rootElementVisual = ElementCompositionPreview.GetElementVisual(element);

                GaussianBlurEffect blurEffect = new GaussianBlurEffect()
                {
                    BlurAmount = 30f,
                    BorderMode = EffectBorderMode.Hard,
                    Optimization = EffectOptimization.Balanced,
                    Source = new ArithmeticCompositeEffect()
                    {
                        MultiplyAmount = 0,
                        Source1Amount = .7f,
                        Source2Amount = tintOpacity,
                        Source1 = new CompositionEffectSourceParameter("backdrop"),
                        Source2 = new ColorSourceEffect()
                        {
                            Color = color
                        }
                    }
                };

                var backdrop = _compositor.CreateBackdropBrush();
                var effectFactory = _compositor.CreateEffectFactory(blurEffect);
                var brush = effectFactory.CreateBrush();
                brush.SetSourceParameter("backdrop", backdrop);

                var spriteVisual = _compositor.CreateSpriteVisual();
                spriteVisual.Brush = brush;

                ElementCompositionPreview.SetElementChildVisual(element, spriteVisual);

                var bindSizeAnimation = _compositor.CreateExpressionAnimation("visual.Size");
                bindSizeAnimation.SetReferenceParameter("visual", rootElementVisual);
                spriteVisual.StartAnimation("Size", bindSizeAnimation);
            }
        }
    }
}
