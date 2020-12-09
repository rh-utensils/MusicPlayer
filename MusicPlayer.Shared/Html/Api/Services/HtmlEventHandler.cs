using System;
using Windows.UI.Xaml;

namespace MusicPlayer.Shared.Html.Api.Services
{
    public class HtmlEventHandler
    {
        /// <summary>
        ///     Dispatches an HTML event to the specified <see cref="EventHandler" />, (synchronously) invoking the affected
        ///     <see cref="EventHandler" /> in the appropriate order.
        /// </summary>
        /// <param name="control"><see cref="UIElement" /> to initialize the event and determine which event listeners to invoke.</param>
        /// <param name="htmlEventName">A <see cref="string" /> containing the name of the HTML event attribute to be registered.</param>
        /// <param name="eventHandler">An <see cref="EventHandler" /> that is dispatched when the specified HTML event is raised.</param>
        public static void RegisterEvents(UIElement control, (string htmlEventName, EventHandler eventHandler)[] events)
        {
            foreach (var (htmlEventName, eventHandler) in events)
            {
                control.ExecuteJavascript(
                    $"element.addEventListener('{htmlEventName}', () => element.dispatchEvent(new Event('{htmlEventName}'));");
                control.RegisterHtmlEventHandler(htmlEventName, eventHandler);
            }
        }
    }
}