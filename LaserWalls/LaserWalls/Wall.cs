using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LaserWalls
{
    class Wall
    {
        private Texture2D Texture; // Texture of this wall
        public Rectangle Border;   // Border defining the visible and physical part
                                   // of the wall
        private Directions Direction;  // Direction the player is traveling, used to fill gap
                                       // when the player turns

        public void Initialize(int x, int y, Texture2D _texture)
        {
            Texture = _texture;
            Border = new Rectangle(x, y, 0, 20);
        }

        public void Update(Directions direction, int position)
        {
            // Sets Direction so the gap created when turning can be filled
            Direction = direction;

            if (direction == Directions.Up)
            {
                Border.Width = 20;
                int y = Border.Y;
                Border.Height += y - position;
                Border.Y = position;  
            }

            if (direction == Directions.Down)
            {
                Border.Width = 20;
                Border.Height = position - Border.Y;
            }
            
            // Records the value of x before updating then sets the new
            // width by adding the distance between x and the new position
            // of x.  Finally sets the new value of x.
            if (direction == Directions.Left)
            {
                int x = Border.X;              
                Border.Width += x - position;
                Border.X = position;      
            }

            // Expands the width by subtracting the position of the left 
            // side of the bar from the new position giving it its width
            if (direction == Directions.Right)
                Border.Width = position - Border.X;
        }

        public void Turn(int position)
        {
            switch (Direction)
            {
                case Directions.Up:
                    int y = Border.Y;
                    Border.Height += y - position + 7;
                    Border.Y = position - 7;    
                    break;
                case Directions.Down:
                    Border.Height = position + 13 - Border.Y;
                    break;
                case Directions.Left:
                    int x = Border.X;              
                    Border.Width += x - position + 10;
                    Border.X = position - 10;    
                    break;
                case Directions.Right:
                    Border.Width = position + 10 - Border.X;                    
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Border, Color.White);
            //spriteBatch.Draw(Texture, Border, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height /2),
            //                 SpriteEffects.None, 0f);             
        }
    }
}
