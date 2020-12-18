using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Uno.UI.Runtime.WebAssembly;
using Windows.UI.Xaml.Media;

namespace MusicPlayer.Shared.Html.Api.Components
{
    [HtmlElement("canvas")]
    public class SpectrumAnalyzer : Control
    {
        public SpectrumAnalyzer(MediaElement mediaElement)
        {
            MediaElement = mediaElement;

            SizeChanged += OnSizeChanged;
            RegisterPropertyChangedCallback(ForegroundProperty, ForegroundChanged);

            var media = $"document.getElementById('{mediaElement.GetHtmlId()}')";
            var javascript = $@"
                (function initialize() {{
                    var analyser = {media}.context.analyser;
                    var ctx = element.getContext('2d');

                    var bufferLength = analyser.frequencyBinCount;
                    var dataArray = new Uint8Array(bufferLength);

                    var barHeight;
                    var x = 0;

                    function renderFrame() {{
                        requestAnimationFrame(renderFrame);

                        x = 0;

                        analyser.getByteFrequencyData(dataArray);

                        var width = element.width;
                        var height = element.height;

                        var background = element.style.backgroundColor;

                        var barColor = element.barColor;
                        var barWidth = element.barWidth;
                        var barDistance = element.barDistance;
                        var barRadius = element.barCornerRadius;

                        var bars = (width / (barWidth + barDistance)) - 0.5;

                        var grad = ctx.createLinearGradient(0, 0, 0, height);
                        grad.addColorStop(0.5, barColor);
                        grad.addColorStop(1, background);
                        ctx.fillStyle = grad;

                        for (let i = 0; i < bars; i++) {{
                            barHeight = (dataArray[i] * 2.5);

                            roundRect(ctx, x, (height - barHeight), barWidth, barHeight, barRadius);

                            x += barWidth + barDistance;
                        }}
                    }}

                    function roundRect(ctx, x, y, width, height, radius) {{
                        if (radius === undefined) radius = 5;

                        ctx.beginPath();
                        ctx.moveTo(x + radius, y);
                        ctx.lineTo(x + width - radius, y);
                        ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
                        ctx.lineTo(x + width, y + height - radius);
                        ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
                        ctx.lineTo(x + radius, y + height);
                        ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
                        ctx.lineTo(x, y + radius);
                        ctx.quadraticCurveTo(x, y, x + radius, y);
                        ctx.closePath();

                        ctx.fill();
                    }}
                }})();";

            this.ExecuteJavascript(javascript);
        }

        public IMediaElement MediaElement { get; }

        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            var size = args.NewSize;

            var newWidth = size.Width.ToString(CultureInfo.InvariantCulture);
            var newHeight = size.Height.ToString(CultureInfo.InvariantCulture);

            var javascript = $@"
                element.width = {newWidth} * devicePixelRatio;
                element.height = {newHeight} * devicePixelRatio;
                var ctx = element.getContext('2d');
                ctx.scale(devicePixelRatio, devicePixelRatio);";

            this.ExecuteJavascript(javascript);
        }

        private static void ForegroundChanged(DependencyObject dependencyobject, DependencyProperty dp)
        {
            var element = dependencyobject as ColorAnimation;

            if (!(element.Foreground is SolidColorBrush)) return;
            var brush = (SolidColorBrush)element.Foreground;

            var r = brush.Color.R;
            var g = brush.Color.G;
            var b = brush.Color.B;
            var a = brush.Color.A;
            var rgba = $"`rgba({r}, {g}, {b}, {a})`";

            element.ExecuteJavascript($"element.barColor = {rgba};");;
        }

        #region Properties

        #region BarWidthProperty

        public static readonly DependencyProperty BarWidthProperty = DependencyProperty.Register(
            "BarWidth",
            typeof(int),
            typeof(SpectrumAnalyzer),
            new PropertyMetadata(12, BarWidthChanged));

        public int BarWidth
        {
            get => (int)GetValue(BarWidthProperty);
            set
            {
                if (value < 0) value = 0;
                SetValue(BarWidthProperty, value);
            }
        }

        private static void BarWidthChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as SpectrumAnalyzer;
            var width = ((int)args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.barWidth = {width};");
        }

        #endregion

        #region BarDistanceProperty

        public static readonly DependencyProperty BarDistanceProperty = DependencyProperty.Register(
            "BarDistance",
            typeof(int),
            typeof(SpectrumAnalyzer),
            new PropertyMetadata(10, BarDistanceChanged));

        public int BarDistance
        {
            get => (int)GetValue(BarDistanceProperty);
            set
            {
                if (value < 0) value = 0;
                SetValue(BarDistanceProperty, value);
            }
        }

        private static void BarDistanceChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as SpectrumAnalyzer;
            var distance = ((int)args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.barDistance = {distance};");
        }

        #endregion

        #region BarRadiusProperty

        public static readonly DependencyProperty BarRadiusProperty = DependencyProperty.Register(
            "BarRadius",
            typeof(int),
            typeof(SpectrumAnalyzer),
            new PropertyMetadata(10, BarRadiusChanged));

        public int BarRadius
        {
            get => (int)GetValue(BarRadiusProperty);
            set
            {
                if (value < 0) value = 0;
                SetValue(BarRadiusProperty, value);
            }
        }

        private static void BarRadiusChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var element = dependencyobject as SpectrumAnalyzer;
            var radius = ((int)args.NewValue).ToString(CultureInfo.InvariantCulture);
            element?.ExecuteJavascript($"element.barRadius = {radius};");
        }

        #endregion

        #endregion
    }
}