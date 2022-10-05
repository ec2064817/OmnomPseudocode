
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OmnomPseudocode
{
    class ButtonSprite
    {
        public Texture2D Art;
        public Vector2 Position;

        public ButtonSprite(Texture2D tex, Vector2 pos)
        {
            // Copy the texture "tex" into the "Art" class variable
            Art = tex;
            // Copy the vector "pos" into the "Position" class variable
            Position = pos;
        }

        public void DrawMe(SpriteBatch sb, Color col)
        {
            // use the spritebatch "sb" to draw the sprite at "Position" using the texture "Art" with the tint from "col"
            sb.Draw(Art, Position, col);
        }
    }

    class PlayerSprite
    {
        public Texture2D Art;
        public Vector2 Position;
        public Rectangle CollisionRect;

        public PlayerSprite(Texture2D tex, Vector2 pos)
        {
            // Copy the texture "tex" into the "Art" class variable
            Art= tex;
            // Copy the vector "pos" into the "Position" class variable
            Position = pos;
            // create a new CollisionRect Rectangle using the X and Y from Position and the Width and Height from Art
            CollisionRect = new Rectangle((int)pos.X, (int)pos.Y, Art.Width, Art.Height);
        }

        public void UpdateMe(ButtonState leftB, ButtonState rightB, ButtonState downB, ButtonState upB, int screenWidth, int screenHeight)
        {

           

            

            

            //if (Position.X > screenWidth - Art.Width)
            //{
            //    Position.X = screenWidth - Art.Width;
            //}

            //if (Position.Y > screenWidth - Art.Width)
            //{
            //    Position.Y = screenWidth - Art.Width;
            //}

            // if leftB is pressed
            if (leftB ==  ButtonState.Pressed && Position.X > 0)
            {
                // subtract 1 from the X that belongs to Position
                Position.X-- ;
            }


            // if rightB is pressed
            if (rightB == ButtonState.Pressed && Position.X < screenWidth - Art.Width)
            {
                // add 1 to the X that belongs to Position
                Position.X++;
            }
            

            // if downB is pressed
            if (downB == ButtonState.Pressed && Position.Y !> screenHeight - Art.Height)
            {
                // add 1 to the Y that belongs to Position
                Position.Y++ ;
            }
            

            // if upB is pressed
            if (upB == ButtonState.Pressed && Position.Y > 0)
            {
                // subtract 1 from the Y that belongs to Position
                Position.Y-- ;
            }


            // set the X that belongs to CollisionRect to equal the integer version of the X that belongs to Position
            CollisionRect.X = (int)Position.X;

            // set the Y that belongs to CollisionRect to equal the integer version of the Y that belongs to Position
            CollisionRect.Y = (int)Position.Y;
        }

        public void DrawMe(SpriteBatch sb)
        {
            // use the spritebatch "sb" to draw the sprite at "Position" using the texture "Art" with a white tint
            sb.Draw(Art, Position, Color.White);
        }
    }
}
