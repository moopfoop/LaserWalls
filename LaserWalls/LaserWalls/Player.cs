using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaserWalls
{
    class Player
    {
        private Directions Direction;    // Direction the play is heading in
        public float speed { get; set; } // Player speed
        public int lives { set; }        // Number of lives left
        public bool active { get; }      // Player state, if true display the player
        
        // Pairs a direction with its matching texture
        Dictionary<Directions, Texture2D> Textures;

        // Returns the width of the current direction's texture
        public int Width
        {
            get { return Textures[Direction].Width; }
        }

        // Returns the heighth of the current direction's texture
        public int Heighth
        {
            get { return Textures[Direction].Width; }
        }

        /// <summary>
        /// Initilizes the Player class.  
        /// A list of textures must be passed in the order:
        ///     Up, Down, Left, Right
        /// </summary>
        /// <param name="_Direction">Initial direction</param>
        /// <param name="Textures">Player's textures (Up, Down, Left Right)</param>
        /// <param name="_lives">Initial number of lives</param>
        /// <param name="_speed">Initial speed</param>
        public void Initialize(Directions _Direction, List<Texture2D> _Textures, int _lives, float _speed)
        {
            Direction = _Direction;
            lives = _lives;
            speed = _speed;

            // Load the textures
            Directions d = Directions.Up;
            for (int i = 0; i < _Textures.Count; i++)
            {
                Textures[d++] = _Textures[i];
            }
        }

        /// <summary>
        /// Updates the Player 
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Draws the Player to the screen
        /// </summary>
        public void Draw()
        {
        }
    }
}
