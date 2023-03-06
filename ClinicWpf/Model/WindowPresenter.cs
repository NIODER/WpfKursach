using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicWpf.Model
{
    public class WindowPresenter
    {
        private static WindowPresenter? instance;
        private readonly List<(Type type, Window? window)> windows;

        private WindowPresenter()
        {
            windows = new();
        }

        public static WindowPresenter Instance()
        {
            return instance ??= new WindowPresenter();
        }

        public void CloseWindow(Type type)
        {
            var window = windows.FirstOrDefault(w => w.type == type);
            if (window.window is null)
            {
                throw new NullReferenceException("No such window in windows");
            }
            window.window.Close();
        }

        public void AddWindow(Window? window, Type type)
        {
            if (window is null)
            {
                throw new NullReferenceException("Window was null");
            }
            windows.Add((type, window));
        }

        public void OpenWindow(Type type)
        {
            var window = windows.FirstOrDefault(w => w.type == type);
            if (window == default)
            {
                var w = (Window?)Activator.CreateInstance(type);
                w?.Show();
            }
            else
            {
                window.window?.Show();
            }
        }
    }
}
