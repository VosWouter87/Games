namespace MaskEdit
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Generator.KingMoveOptions();
            ApplicationConfiguration.Initialize();
            Application.Run(new Mask());
        }
    }
}