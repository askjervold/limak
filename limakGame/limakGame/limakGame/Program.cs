using System;

namespace limakGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Limak game = new Limak())
            {
                game.Run();
            }
        }
    }
#endif
}