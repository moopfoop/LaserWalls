// LaserWalls.cs
// by Greg Berrett
//
// Represents the main loop of the game.  Initializes the components and makes
// calls to update them to the screen.  Reads input from the keybaord and a
// gamepad to update the player's state.
//
// Controls:
// Up:          Up Arrow / Dpad.Up
// Down:      Down Arrow / Dpad.Down
// Left:      Left Arrow / Dpad.Left
// Right:    Right Arrow / Dpad.Right
// Accelerate: Space Bar / Button.A

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
    public class LaserWalls : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;  // Keys pressed
        GamePadState gamePadState;    // Gamepad pressed
        
        List<Texture2D> textures;  // List of player textures
        Player player;             // current player

        Constants settings;  // List of constants used for settings

        Directions previousDirection;  // player's previous Direction

        public LaserWalls()
        {
            graphics = new GraphicsDeviceManager(this);

            // Set to full screen
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.IsFullScreen = true;

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

            settings = new Constants();

            // Initialize player
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

            player.Initialize(Directions.Right, textures, new Vector2(100, 100), 3, settings.BaseSpeed);

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

            // Moves the player along
            player.Update();

            // TODO: Add your update logic here

            // Check to see if game is exited
            if (CheckExitKey())
                ExitGame();

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

            // Accelerates the player if gamepad.A or space bar is pressed, decelerates otherwise
            if (keyboardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A))
                player.Accelerate();
            else
                player.Decelerate();
        }

        /// <summary>
        /// Checks for the Escape key in order to exit the game
        /// </summary>
        /// <returns>Returns true if the escape key is pressed, false otherwise</returns>
        private bool CheckExitKey()
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
                return true;
            return false;
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        private void ExitGame()
        {
            Exit();
        }
    }
}
