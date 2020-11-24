using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MusicPlayer
{
    public sealed partial class MainPage : Page
    {
        public static bool IsSongNavigationVisible;

        public MainPage()
        {
            InitializeComponent();
        }

        public static MainPage GetCurrentPage()
        {
            var frame = (Frame) Window.Current.Content;
            var mainPage = (MainPage) frame.Content;
            return mainPage;
        }

        #region StateChange

        public enum State
        {
            Undefined,
            Desktop,
            Tablet,
            Phone
        }

        public static event EventHandler<PropertyChangedEventArgs> StateChanged;

        private static void RaiseStateChanged([CallerMemberName] string name = null)
        {
            StateChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        private static State _state = State.Undefined;

        public static State CurrentState
        {
            get => _state;
            set
            {
                if (Equals(value, _state) || value == State.Undefined) return;

                _state = value;

                VisualStateManager.GoToState(GetCurrentPage(), value.ToString(), false);
                RaiseStateChanged(value.ToString());
            }
        }

        private void MainPage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var oldState = CurrentState;

            if (e.NewSize.Width < 800) CurrentState = State.Phone;
            else if (e.NewSize.Width < 1300) CurrentState = State.Tablet;
            else CurrentState = State.Desktop;

            if (Equals(CurrentState, oldState)) return;

            switch (CurrentState)
            {
                case State.Desktop:
                    break;
                case State.Tablet:
                case State.Phone:
                    break;
                case State.Undefined:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}