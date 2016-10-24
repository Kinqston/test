using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D ballTexture2D;
        private Rectangle ballRectangle;

        private Texture2D loseTexture2D;
        private Rectangle loseRectangle;

        private Texture2D winTexture2D;
        private Rectangle winRectangle;

        private Texture2D lineTexture2D;
        private Rectangle lineRectangle;
        private int ScreenWidth;
        private int ScreenHeigth;
        private Vector2 velocity;
        int score;
        MouseState prevmousestate;
        Boolean fail = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            var metric = new Android.Util.DisplayMetrics();
            Activity.WindowManager.DefaultDisplay.GetMetrics(metric);
            // установка параметров экрана
            graphics.IsFullScreen = true;
            IsMouseVisible = true;          
            graphics.PreferredBackBufferWidth = metric.WidthPixels;
            graphics.PreferredBackBufferHeight = metric.HeightPixels;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeigth = GraphicsDevice.Viewport.Height;
            ballTexture2D = Content.Load<Texture2D>("ball");
            ballRectangle = new Rectangle(20,20,50,50);

            loseTexture2D = Content.Load<Texture2D>("lose");
            loseRectangle = new Rectangle(0, 0, ScreenWidth, ScreenHeigth);

            winTexture2D = Content.Load<Texture2D>("win");
            winRectangle = new Rectangle(0, 0, ScreenWidth, ScreenHeigth);


            velocity.Y = 5f;
            velocity.X = 5f;


            lineTexture2D = Content.Load<Texture2D>("line");
            lineRectangle = new Rectangle(ScreenWidth/2-50, ScreenHeigth-30, 100, 15);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            if (fail == false && score<5)
            {
                ballRectangle.X = ballRectangle.X + Convert.ToInt32(velocity.X);
                ballRectangle.Y = ballRectangle.Y + Convert.ToInt32(velocity.Y);

                if (ballRectangle.X <= 0)
                {
                    velocity.X = -velocity.X;
                }
                if (ballRectangle.X + ballRectangle.Width >= ScreenWidth)
                {
                    velocity.X = -velocity.X;
                }
                if (ballRectangle.Y <= 0)
                {
                    velocity.Y = -velocity.Y;
                }
                if (ballRectangle.Y + ballRectangle.Height >= ScreenHeigth)
                {
                    fail = true;
                }
                // TODO: Add your update logic here
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    lineRectangle.X = lineRectangle.X + 4;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    lineRectangle.X = lineRectangle.X - 4;

                if (ballRectangle.Intersects(lineRectangle))
                {
                    velocity.Y = -velocity.Y;
                    score++;
                }
            }

          /*  MouseState mousestate = Mouse.GetState();
            if (mousestate.X != prevmousestate.X || mousestate.Y != prevmousestate.Y)
            {
                Console.WriteLine(mousestate.X);
                starposition = new Vector2(mousestate.X, mousestate.Y);
                prevmousestate = mousestate;
            }*/
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Coral);
            spriteBatch.Begin();
            if(score<5 && fail == false)
            {
                spriteBatch.Draw(ballTexture2D, ballRectangle, Color.White);
                spriteBatch.Draw(lineTexture2D, lineRectangle, Color.White);
            }
            else
            {
                if(score == 5)
                {
                    spriteBatch.Draw(winTexture2D, loseRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(loseTexture2D, loseRectangle, Color.White);
                }
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
      
    }
}
