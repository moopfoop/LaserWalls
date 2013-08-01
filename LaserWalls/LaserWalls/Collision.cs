using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LaserWalls
{
    class Collision
    {
        /// <summary>
        /// Detects if the objects have collided with each other and
        /// updates their status accordingly.  Returns true if a
        /// collision was detected, false otherwise.
        /// </summary>
        /// <param name="player"></param>
        public bool Detect(Player player)
        {
            bool detection;

            detection = PlayerWallDetection(player);

            // detection = value of detection.  if true, detection = true
            // if false check PlayerWallDetection and set detection to its value
            // detection = detection ? detection : PlayerWallDetection(player);

            return detection;
        }

        private bool PlayerWallDetection(Player player)
        {
            // Dimensions of the border are (20, 20), (1900, 20)
            // (20, 980), and (1900, 980)

            Rectangle playerOutline = new Rectangle((int)player.Position.X, (int)player.Position.Y,
                                                    player.Width, player.Height);

            // Checks for right wall collision
            if (player.Position.X > 1900 - (player.Width / 2))
            {
                player.active = false;
                return true;
            }

            // Checks for left wall collision
            if (player.Position.X < 20 + (player.Width / 2))
            {
                player.active = false;
                return true;
            }

            // Checks for top wall collision
            if (player.Position.Y < 20 + (player.Height / 2))
            {
                player.active = false;
                return true;
            }

            // Checks for bottom wall collision
            if (player.Position.Y > 980 - (player.Height / 2))
            {
                player.active = false;
                return true;
            }
            return false;
        }
    }
}
