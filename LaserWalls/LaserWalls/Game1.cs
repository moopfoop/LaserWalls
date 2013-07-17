using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LaserWalls
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;  // Keys pressed
        GamePadState gamePadState;    // Gamepad pressed
        
        List<Texture2D> textures;  // List of player textures
        Player player;             // current player

        Directions previousDirection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            textures = new List<Texture2D>();

            player = new Player();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
      
            textures.Add(Content.Load<Texture2D>("ShipUp"));
            textures.Add(Content.Load<Texture2D>("ShipDown"));
            textures.Add(Content.Load<Texture2D>("ShipLeft"));
            textures.Add(Content.Load<Texture2D>("ShipRight"));

            player.Initialize(Directions.Left, textures, new Vector2(100, 100), 3, 1f);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Stores the previous direction to prevent 180 degree turns
            previousDirection = player.Direction;

            // Get input states and store it
            gamePadState = GamePad.GetState(PlayerIndex.One);
            keyboardState = Keyboard.GetState();

            // Update any input changes to the player
            UpdatePlayer();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates any input changes to the player
        /// </summary>
        private void UpdatePlayer()
        {
            // Since newDirection is nonnullable it is set to the previous direction which is
            // the state we are trying to detect a conflict with when a key is pressed.  This
            // keeps the player moving in the same direction.
            Directions newDirection = previousDirection;

            // Read input from the keyboard/gamepad to determine potential new direction
            if (keyboardState.IsKeyDown(Keys.Up) || gamePadState.DPad.Up == ButtonState.Pressed)
                newDirection = Directions.Up;
            if (keyboardState.IsKeyDown(Keys.Down) || gamePadState.DPad.Down == ButtonState.Pressed)
                newDirection = Directions.Down;
            if (keyboardState.IsKeyDown(Keys.Left) || gamePadState.DPad.Left == ButtonState.Pressed)
                newDirection = Directions.Left;
            if (keyboardState.IsKeyDown(Keys.Right) || gamePadState.DPad.Right == ButtonState.Pressed)
                newDirection = Directions.Right;

            // Check to verify the new direction is not 180 degree of the old direction
            if (newDirection == Directions.Up && previousDirection != Directions.Down)
                player.Direction = newDirection;
            if (newDirection == Directions.Down && previousDirection != Directions.Up)
                player.Direction = newDirection;
            if (newDirection == Directions.Left && previousDirection != Directions.Right)
                player.Direction = newDirection;
            if (newDirection == Directions.Right && previousDirection != Directions.Left)
                player.Direction = newDirection;
        }
    }
}
