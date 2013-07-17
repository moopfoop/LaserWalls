using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaserWalls
{
    class Player
    {
        public Directions Direction;    // Direction the play is heading in
        public float speed { get; set; } // Player speed
        public int lives { get; set; }        // Number of lives left
        public bool active { get; set; }      // Player state, if true display the player
        public Vector2 Position;         // Position of the player

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
        public void Initialize(Directions _Direction, List<Texture2D> _Textures, Vector2 _position, int _lives, float _speed)
        {
            Direction = _Direction;
            lives = _lives;
            speed = _speed;
            Position = _position;
            Textures = new Dictionary<Directions, Texture2D>();

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
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Direction)
            {
                case Directions.Up:
                    spriteBatch.Draw(Textures[Directions.Up], Position, null, Color.White, 0f, 
                                     new Vector2(Width / 2, Heighth / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Down:
                    spriteBatch.Draw(Textures[Directions.Down], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Heighth / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Left:
                    spriteBatch.Draw(Textures[Directions.Left], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Heighth / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Right:
                    spriteBatch.Draw(Textures[Directions.Right], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Heighth / 2), 1f, SpriteEffects.None, 0f);
                    break;
            }
        }
    }
}
