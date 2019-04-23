using EchoServer.ScreenConsole.Components;
using System.Collections.Generic;

namespace EchoServer.ScreenConsole
{
    public interface IScreen
    {
        DrawArea GetComponentDimensions(IScreenComponent component);
        void AddComponent(IScreenComponent component, byte x, byte y, byte witdth, byte height);
    }

    public class Screen : IScreen
    {
        private Dictionary<IScreenComponent, DrawArea> _components;

        public Screen()
        {
            _components = new Dictionary<IScreenComponent, DrawArea>();
        }

        public void AddComponent(IScreenComponent component, byte x, byte y, byte width, byte height)
        {
            //_components.Add(component, new DrawArea()
            //{
            //    TopLeft = new Point(x, y),
            //    Width = width,
            //    Height = height
            //});
        }

        public DrawArea GetComponentDimensions(IScreenComponent component)
        {
            return _components[component];
        }
    }
}