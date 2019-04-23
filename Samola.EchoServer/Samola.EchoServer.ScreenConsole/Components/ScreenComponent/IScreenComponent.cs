namespace EchoServer.ScreenConsole.Components
{
    /// <summary>
    /// Represents a console GUI component
    /// </summary>
    public interface IScreenComponent
    {
        /// <summary>
        /// Refreshes the component contents to the rendering device
        /// </summary>
        void Initialize();

        /// <summary>
        /// Refreshes the changed component contents to the rendering device
        /// </summary>
        void Refresh();
    }
}