using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MarathonSkills
{
    public enum AnimationSlide
    {
        None = 0,
        SlideToLeft = 1,
        SlideToTop = 2,
        SlideToRight = 3,
        SlideToBottom = 4,
        SlideFromLeft = 5,
        SlideFromTop = 6,
        SlideFromRight = 7,
        SlideFromBottom = 8
    }

    public enum AnimationFade
    {
        None = 0,
        FadeIn = 1,
        FadeOut = 2
    }

    public class Animation
    {
        private Storyboard _storyboard;
        private double _totalSeconds;

        public event EventHandler Completed;

        public Animation()
        {
            _storyboard = new Storyboard();
            _storyboard.Completed += (sender, e) =>
            {
                Completed?.Invoke(sender, e);
            };
            _totalSeconds = 0;
        }

        public async Task StartAsync(FrameworkElement element)
        {
            _storyboard.Begin(element);
            await Task.Delay((int)(_totalSeconds * 1000));
        }

        public void Start(FrameworkElement element)
        {
            _storyboard.Begin(element);
            Task.Delay((int)(_totalSeconds * 1000));
        }

        public Animation AddSlide(AnimationSlide animation, double offset, double seconds = .25)
        {
            switch (animation)
            {
                case AnimationSlide.None:
                    break;
                case AnimationSlide.SlideToLeft:
                    ThicknessAnimation animSlideToLeft = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = new Thickness(-offset, 0, 0, 0),
                        From = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideToLeft, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideToLeft);
                    break;
                case AnimationSlide.SlideToTop:
                    ThicknessAnimation animSlideToTop = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = new Thickness(0, -offset, 0, 0),
                        From = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideToTop, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideToTop);
                    break;
                case AnimationSlide.SlideToRight:
                    ThicknessAnimation animSlideToRight = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = new Thickness(0, 0, -offset, 0),
                        From = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideToRight, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideToRight);
                    break;
                case AnimationSlide.SlideToBottom:
                    ThicknessAnimation animSlideToBottom = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = new Thickness(0, 0, 0, -offset),
                        From = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideToBottom, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideToBottom);
                    break;
                case AnimationSlide.SlideFromLeft:
                    ThicknessAnimation animSlideFromLeft = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        From = new Thickness(-offset, 0, 0, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideFromLeft, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideFromLeft);
                    break;
                case AnimationSlide.SlideFromTop:
                    ThicknessAnimation animSlideFromTop = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        From = new Thickness(0, -offset, 0, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideFromTop, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideFromTop);
                    break;
                case AnimationSlide.SlideFromRight:
                    ThicknessAnimation animSlideFromRight = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        From = new Thickness(0, 0, -offset, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideFromRight, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideFromRight);
                    break;
                case AnimationSlide.SlideFromBottom:
                    ThicknessAnimation animSlideFromBottom = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        From = new Thickness(0, 0, 0, -offset),
                        To = new Thickness(0),
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animSlideFromBottom, new PropertyPath("Margin"));
                    _storyboard.Children.Add(animSlideFromBottom);
                    break;
                default:
                    break;
            }
            _totalSeconds = _totalSeconds > seconds ? _totalSeconds : seconds;
            return this;
        }

        public Animation AddFade(AnimationFade animation, double seconds = .25)
        {
            switch (animation)
            {
                case AnimationFade.None:
                    break;
                case AnimationFade.FadeIn:
                    DoubleAnimation animeFadeIn = new DoubleAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = 1,
                        From = 0,
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animeFadeIn, new PropertyPath("Opacity"));
                    _storyboard.Children.Add(animeFadeIn);
                    break;
                case AnimationFade.FadeOut:
                    DoubleAnimation animFadeOut = new DoubleAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(seconds),
                        To = 0,
                        From = 1,
                        DecelerationRatio = 0
                    };
                    Storyboard.SetTargetProperty(animFadeOut, new PropertyPath("Opacity"));
                    _storyboard.Children.Add(animFadeOut);
                    break;
                default:
                    break;
            }
            _totalSeconds = _totalSeconds > seconds ? _totalSeconds : seconds;
            return this;
        }

    }

    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate () { };


        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

}
