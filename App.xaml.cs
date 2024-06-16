
namespace MemoryGameTestDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        /// <summary>
        /// Override method to manually set the window size
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns>A window with set dimensions</returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            const int newWidth = 600;
            const int newHeight = 800;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}
