using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace OmnomPseudocode
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Class Variables for Game1
        SpriteFont gill14;
        float candyDistance;

        GamePadState pad1;

        ButtonSprite upButton, downButton, leftButton, rightButton;

        PlayerSprite omnom, candy;
        bool candyMoved;

        Rectangle screenSize;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // set candyMoved to false
            candyMoved = false;

            screenSize = GraphicsDevice.Viewport.Bounds;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the font into the gill14 class variable
            gill14 = Content.Load<SpriteFont>("Gill14");

            // create a new upButton ButtonSprite at position 700, 10 and load the Up texture
            upButton = new ButtonSprite(Content.Load<Texture2D>("Up"), new Vector2 (700, 10));

            // create a new downButton ButtonSprite at position 700, 110 and load the Down texture
            downButton = new ButtonSprite(Content.Load<Texture2D>("Down"), new Vector2(700, 110));

            // create a new leftButton ButtonSprite at position 700, 110 and load the Left texture
            leftButton = new ButtonSprite(Content.Load<Texture2D>("Left"), new Vector2(650, 60));

            // create a new rightButton ButtonSprite at position 750, 60 and load the Right texture
            rightButton = new ButtonSprite(Content.Load<Texture2D>("Right"), new Vector2(750, 60));

            // create a new omnom PlayerSprite at position 10, 217 and load the omnom texture
            omnom = new PlayerSprite(Content.Load<Texture2D>("candy"), new Vector2(10, 217));

            // create a new candy PlayerSprite at position 60, 224 and load the candy texture
            candy = new PlayerSprite(Content.Load<Texture2D>("candy"), new Vector2(60, 224));

        }

        protected override void Update(GameTime gameTime)
        {
            Vector2 candyVector;

            pad1 = GamePad.GetState(PlayerIndex.One);

            // store the new data about what gamepad one is doing in the pad1 variable
            //GamePadState = pad1;

            // update omnom, giving it information about the DPad Left, Right, Down and Up buttonstates
            omnom.UpdateMe(pad1.DPad.Left, pad1.DPad.Right, pad1.DPad.Down, pad1.DPad.Up, screenSize.Width, screenSize.Height);
            // update the candy, giving it information about the X, B, A and Y buttonstates
            candy.UpdateMe(pad1.Buttons.X, pad1.Buttons.B, pad1.Buttons.A, pad1.Buttons.Y, screenSize.Width, screenSize.Height);

            // Work out the line between the candy's position and omnom's and store it in candyVector
            candyVector = omnom.Position - candy.Position;

            // Work out the length of candyVector and store it in candyDistance
            candyDistance = (MathF.Pow(candyVector.X, 2) + MathF.Pow(candyVector.Y, 2));

            // if candyMoved is false
            if (candyMoved == false)
            {
                // if the A button is pressed OR the B button is pressed OR the X button is pressed OR the Y button is pressed
                if (pad1.Buttons.A == ButtonState.Pressed || pad1.Buttons.B == ButtonState.Pressed || pad1.Buttons.X == ButtonState.Pressed || pad1.Buttons.Y == ButtonState.Pressed)
                {
                    candyMoved = true;
                }
            }


            // if the back button is pressed
            if (pad1.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                // set candy moved to false
                candyMoved = false;
            }


            // if the candy is touching omnom
            if (candy.CollisionRect.Intersects(omnom.CollisionRect))
            {
                // set candy moved to false
                candyMoved = false;
                _spriteBatch.Begin();
                _spriteBatch.DrawString(gill14, "YOU WIN!", new Vector2(200, 200), Color.Goldenrod);
                _spriteBatch.End();
            }

            if (pad1.Buttons.Start == ButtonState.Pressed)
            {
                Initialize();
                LoadContent();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // if candyMoved is false
            if (candyMoved == false)
            {
                // clear the screen cornflower blue
                GraphicsDevice.Clear(Color.CornflowerBlue);

            }
            else
            {
                // clear the screen black 
                GraphicsDevice.Clear(Color.Black);

            }


            _spriteBatch.Begin();
            // draw omnom
            omnom.DrawMe(_spriteBatch);

            // if candyMoved is false
            if (candyMoved == false)
            {
                // draw the candy
                candy.DrawMe(_spriteBatch);
            }


            // if Y is pressed
            if (pad1.Buttons.Y == ButtonState.Pressed)
            {
                // draw the upButton with a yellow tint
                upButton.DrawMe(_spriteBatch, Color.Yellow);
            }
            // else
            else
            {
                // draw the upButton with a white tint
                upButton.DrawMe(_spriteBatch, Color.White);
            }

            // endif

            // if A is pressed
            if (pad1.Buttons.A == ButtonState.Pressed)
            {
                // draw the downButton with a green tint
                downButton.DrawMe(_spriteBatch, Color.Green);
            }
            // else
            else
            {
                // draw the Button with a white tint
                downButton.DrawMe(_spriteBatch, Color.White);
            }


            // if X is pressed
            if (pad1.Buttons.X == ButtonState.Pressed)
            {
                // draw the downButton with a blue tint
                leftButton.DrawMe(_spriteBatch, Color.Blue);

                if (candy.Position.X <= 0) leftButton.DrawMe(_spriteBatch, Color.Gray);
            }
            // else
            else
            {
                // draw the Button with a white tint
                leftButton.DrawMe(_spriteBatch, Color.White);
            }


            // if B is pressed
            if (pad1.Buttons.B == ButtonState.Pressed)
            {
                // draw the downButton with a red tint
                rightButton.DrawMe(_spriteBatch, Color.Red);

                if (candy.Position.X >= screenSize.Width - candy.Art.Width) rightButton.DrawMe(_spriteBatch, Color.Gray);
            }
            
            // else
            else
            {
                // draw the Button with a white tint
                rightButton.DrawMe(_spriteBatch, Color.White);
            }

            //pad1.DPad.Left, pad1.DPad.Right, pad1.DPad.Down, pad1.DPad.Up
            // if Up on the DPad is released AND Down on the DPad is released AND Left on the DPad is released AND Right on the DPad is released 
            if (pad1.DPad.Left == ButtonState.Released && pad1.DPad.Right == ButtonState.Released && pad1.DPad.Up == ButtonState.Released && pad1.DPad.Down == ButtonState.Released)
            {
                // draw the candyDistance variable as a string in the top left
                _spriteBatch.DrawString(gill14, "you are " + (int)candyDistance + " away from the candy",new Vector2(0,0) ,Color.Red);
            }

            if (pad1.DPad.Left == ButtonState.Pressed && pad1.DPad.Right == ButtonState.Pressed && pad1.DPad.Up == ButtonState.Pressed && pad1.DPad.Down == ButtonState.Pressed)
            {
                
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
