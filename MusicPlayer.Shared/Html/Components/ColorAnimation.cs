using System.Globalization;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Uno.UI.Runtime.WebAssembly;

namespace MusicPlayer.Shared.Html.Api.Components
{
    [HtmlElement("canvas")]
    public class ColorAnimation : Control
    {
        public ColorAnimation()
        {
            SizeChanged += OnSizeChanged;

            var animation = $@"
                element.AnimateBackground = function (newColor, positionX, positionY, duration) {{
                    var oldColor = element.style.backgroundColor;

                    var width = element.width;
                    var height = element.height;

                    var ctx = element.getContext('2d');

                    function calcPageFillRadius(x, y) {{
                        var l = Math.max(x - 0, width - x);
                        var h = Math.max(y - 0, height - y);
                        return Math.sqrt(Math.pow(l, 2) + Math.pow(h, 2));
                    }}

                    var Circle = function (opts) {{
                        for (var key in opts) {{
                            if (opts.hasOwnProperty(key)) this[key] = opts[key];
                        }}
                        return this;
                    }}

                    Circle.prototype.draw = function () {{
                        ctx.globalAlpha = this.opacity || 1;
                        ctx.beginPath();
                        ctx.arc(this.x, this.y, this.r, 0, 2 * Math.PI, false);
                        if (this.stroke) {{
                            ctx.strokeStyle = this.stroke.color;
                            ctx.lineWidth = this.stroke.width;
                            ctx.stroke();
                        }}
                        if (this.fill) {{
                            ctx.fillStyle = this.fill;
                            ctx.fill();
                        }}
                        ctx.closePath();
                        ctx.globalAlpha = 1;
                    }}

                    var targetR = calcPageFillRadius(positionX, positionY);
                    var rippleSize = Math.min(200, (width * .4));

                    var pageFill = new Circle({{
                        x: positionX,
                        y: positionY,
                        r: 0,
                        fill: newColor
                    }});

                    var fillAnimation = anime({{
                        targets: pageFill,
                        r: targetR,
                        duration: Math.max(targetR / 2, duration),
                        easing: 'easeOutQuart'
                    }});

                    var ripple = new Circle({{
                        x: positionX,
                        y: positionY,
                        r: 0,
                        fill: oldColor,
                        stroke: {{
                            width: 3,
                            color: oldColor
                        }},
                        opacity: 1
                    }});

                    var rippleAnimation = anime({{
                        targets: ripple,
                        r: rippleSize,
                        opacity: 0,
                        duration: 900,
                        easing: 'easeOutExpo'
                    }});

                    anime({{
                        duration: duration < 900 ? 900 : duration,
                        update: () => {{
                            element.style.backgroundColor = oldColor;

                            fillAnimation.animatables[0].target.draw();
                            rippleAnimation.animatables[0].target.draw();
                        }},
                        complete: () => element.style.backgroundColor = newColor
                    }});
                }};";

            this.ExecuteJavascript(animation);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            var size = args.NewSize;

            var newWidth = size.Width.ToString(CultureInfo.InvariantCulture);
            var newHeight = size.Height.ToString(CultureInfo.InvariantCulture);

            var javascript = $@"
                element.width = {newWidth} * devicePixelRatio;
                element.height = {newHeight} * devicePixelRatio;
                var ctx = element.getContext('2d');
                ctx.scale(devicePixelRatio, devicePixelRatio);
                ctx.fillStyle = `rgba(0, 0, 0, 0)`;
                ctx.fillRect(0, 0, {newWidth}, {newHeight});";

            this.ExecuteJavascript(javascript);
        }

        #region Properties

        #region RipplePositionProperty

        public static readonly DependencyProperty RipplePositionProperty = DependencyProperty.Register(
            "RipplePosition",
            typeof(Point),
            typeof(ColorAnimation),
            new PropertyMetadata(default(Point)));

        public Point RipplePosition
        {
            get => (Point)GetValue(RipplePositionProperty);
            set => SetValue(RipplePositionProperty, value);
        }

        #endregion

        #region DurationProperty

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            "Duration",
            typeof(Duration),
            typeof(ColorAnimation),
            new PropertyMetadata(default(Duration)));

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        #endregion

        #region DefaultColorProperty

        public static readonly DependencyProperty DefaultColorProperty = DependencyProperty.Register(
            "DefaultColor",
            typeof(SolidColorBrush),
            typeof(ColorAnimation),
            new PropertyMetadata(default(SolidColorBrush), DefaultColorChanged));

        public SolidColorBrush DefaultColor
        {
            get => (SolidColorBrush)GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }

        private static void DefaultColorChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as ColorAnimation;
            var brush = (SolidColorBrush)args.NewValue;

            var r = brush.Color.R;
            var g = brush.Color.G;
            var b = brush.Color.B;
            var a = brush.Color.A;
            var rgba = $"`rgba({r}, {g}, {b}, {a})`";

            element?.ExecuteJavascript($"element.style.backgroundColor = {rgba};");
        }

        #endregion

        #region BackgroundProperty

        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            "Background",
            typeof(SolidColorBrush),
            typeof(ColorAnimation),
            new PropertyMetadata(default(SolidColorBrush), BackgroundChanged));

        public new SolidColorBrush Background
        {
            get => (SolidColorBrush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static void BackgroundChanged(DependencyObject dependencyobject,
            DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as ColorAnimation;
            var brush = (SolidColorBrush)args.NewValue;

            var duration = element.Duration.TimeSpan.TotalMilliseconds;
            var posX = element.RipplePosition.X;
            var posY = element.RipplePosition.Y;

            var r = brush.Color.R;
            var g = brush.Color.G;
            var b = brush.Color.B;
            var a = brush.Color.A;
            var rgba = $"`rgba({r}, {g}, {b}, {a})`";

            element?.ExecuteJavascript($"element.AnimateBackground({rgba}, {posX}, {posY}, {duration});");
        }

        #endregion
    
        #endregion
    }
}