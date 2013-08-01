// Player.cs
// by Greg Berrett
//
// Represents the player.  Contains number of lives, speed, acceleration and deceleration rate
// position, and direction of the player.  The player may only accelerate and decelerate to a
// max and minimum speed.  The player is allowed only right hand turns and therefore cannot
// turn a 180.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaserWalls
{
    class Player
    {
        public float speed { get; set; } // Player speed
        private float baseSpeed;         // Base speed for deceleration limit
        private float topSpeed;          // Max speed for acceleration limit
        private Directions direction;    // Direction the player is heading in
        public int lives { get; set; }   // Number of lives left
        public bool active { get; set; } // Player state, if true display the player
        public Vector2 Position;         // Position of the player

        // Pairs a direction with its matching texture
        Dictionary<Directions, Texture2D> Textures;

        // Returns the width of the current direction's texture
        public int Width
        {
            get { return Textures[Direction].Width; }
        }

        // Returns the heighth of the current direction's texture
        public int Height
        {
            get { return Textures[Direction].Width; }
        }

        // Public accessor of direction
        public Directions Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Initilizes the Player class.  
        /// A list of textures must be passed in the order:
        ///     Up, Down, Left, Right
        /// </summary>
        public void Initialize(Directions _Direction, List<Texture2D> _Textures, Vector2 _position, int _lives, float _speed)
        {
            active = true;
            direction = _Direction;
            lives = _lives;
            speed = _speed;
            baseSpeed = _speed;
            topSpeed = baseSpeed * 4f;
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
            // Verify the player is active before updating
            if (!active)
                return;

            // Continues the player in the direction it is facing
            switch (Direction)
            {
                case Directions.Up:
                    Position.Y -= speed;
                    break;
                case Directions.Down:
                    Position.Y += speed;
                    break;
                case Directions.Left:
                    Position.X -= speed;
                    break;
                case Directions.Right:
                    Position.X += speed;
                    break;
            }
        }

        /// <summary>
        /// Draws the Player to the screen
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Verify the player is active before drawing
            if (!active)
                return;

            // Draws player based on direction player is traveling
            switch (Direction)
            {
                case Directions.Up:
                    spriteBatch.Draw(Textures[Directions.Up], Position, null, Color.White, 0f, 
                                     new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Down:
                    spriteBatch.Draw(Textures[Directions.Down], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Left:
                    spriteBatch.Draw(Textures[Directions.Left], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
                    break;
                case Directions.Right:
                    spriteBatch.Draw(Textures[Directions.Right], Position, null, Color.White, 0f,
                                     new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
                    break;
            }
        }

        /// <summary>
        /// Accelerates the player to the top speed
        /// </summary>
        public void Accelerate()
        {
            if (speed < topSpeed)
                speed += 0.5f;
        }

        /// <summary>
        /// Decelerates the player to base speed
        /// </summary>
        public void Decelerate()
        {
            if (speed > baseSpeed)
                speed -= 0.25f;
        }

        public void ChangeDirection(Directions _direction)
        {
            direction = _direction;

            switch (direction)
            {
                case Directions.Up:
                    Position.Y -= 20;
                    break;
                case Directions.Down:
                    Position.Y += 20;
                    break;
                case Directions.Left:
                    Position.X -= 20;
                    break;
                case Directions.Right:
                    Position.X += 20;
                    break;
            }
        }
    }
}
