using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using MusicPlayer.Shared.Html.Api.Components;
using Uno.Extensions;

namespace MusicPlayer.Shared.Html.Api
{
    /// <inheritdoc />
    public class PictureInPictureWindow : IPictureInPictureWindow
    {
        private Video _videoElement;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PictureInPictureWindow" /> class.
        /// </summary>
        private PictureInPictureWindow()
        {
            OnResize += OnResizeEvent;
        }

        /// <inheritdoc />
        public int Width { get; private set; }

        /// <inheritdoc />
        public int Height { get; private set; }

        /// <inheritdoc />
        public event EventHandler<HtmlCustomEventArgs> OnResize;

        /// <summary>
        ///     Request async a new instance of the <see cref="PictureInPictureWindow" /> class.
        /// </summary>
        /// <param name="videoElement">A <see cref="Video" /> element which request the a picture in picture window.</param>
        /// <returns>A new <see cref="PictureInPictureWindow" /> class.</returns>
        public static async Task<PictureInPictureWindow> RequestPictureInPictureAsync(Video videoElement)
        {
            var size = await videoElement.ExecuteJavascriptAsync(@"
                (async () => {
                    var pictureInPictureWindow = await element.requestPictureInPicture();
                    pictureInPictureWindow.addEventListener('resize', evt => {
                        var payload = evt.target.width + ',' + evt.target.height;
                        element.dispatchEvent(new CustomEvent('pictureInPictureWindowResize', {detail: payload}));
                    }

                    return pictureInPictureWindow.width + ',' + pictureInPictureWindow.height;
                })();");

            var width = int.Parse(size.Split(',')[0]);
            var height = int.Parse(size.Split(',')[1]);

            var pictureInPictureWindow = new PictureInPictureWindow();

            pictureInPictureWindow.Width = width;
            pictureInPictureWindow.Height = height;
            videoElement.RegisterHtmlCustomEventHandler("resize", pictureInPictureWindow.OnResize);

            return new PictureInPictureWindow();
        }

        /// <summary>
        ///     Update the <see cref="Width" /> and <see cref="Height" /> properties when the <see cref="OnResize" /> event was
        ///     fired.
        /// </summary>
        private void OnResizeEvent(object sender, HtmlCustomEventArgs e)
        {
            var size = e.Detail;

            Width = int.Parse(size.Split(',')[0]);
            Height = int.Parse(size.Split(',')[1]);
        }
    }
}