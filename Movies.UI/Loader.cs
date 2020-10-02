using System.Windows.Input;

namespace Movies.UI
{
    public static class Loader
    {
        public static void StartLoading()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
        }

        public static void FinishLoading()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
    }
}