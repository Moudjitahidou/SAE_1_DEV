using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Sprite
    {
        private Texture2D _texture;
        private SpriteBatch spriteBatch;

        public Vector2 Position;
        
        public float Speed = 2f;

        //public Input Keys;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            //if (Keys == null)
              //  return;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += Speed;
            }
        }

        private void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(_texture,Position);/* ,Color.White*/
            spriteBatch.End();
        }
    }
}
