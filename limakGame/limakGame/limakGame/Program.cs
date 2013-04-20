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
<<<<<<< HEAD
            using (Limak game = new Limak())
=======
            /*using (Game1 game = new Game1())
>>>>>>> origin/limak-hd
            {
                game.Run();
            }*/
            using (TestLevelReader tlr = new TestLevelReader())
            {
                tlr.Run();
            }
        }
    }
#endif
}

