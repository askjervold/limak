using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    interface Viewport
    {
        /// <summary>
        /// Converts pixel resolution into meter for Farseer
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        /// <returns>Size of object in meters</returns>
        Vector2 pixelsToMeter(int width, int height);

        /// <summary>
        /// Converts a world position to coordinates in the viewport
        /// </summary>
        /// <param name="position">Position from physics engine</param>
        /// <returns>Coordinates on screen</returns>
        Point positionToScreenCoordinates(Vector2 position);

        /// <summary>
        /// Checks whether the game object is visible on the screen
        /// </summary>
        /// <param name="gameObject">Game object</param>
        /// <returns>True if the object is in the current viewport</returns>
        bool isVisible(GameObject gameObject);

    }
}
