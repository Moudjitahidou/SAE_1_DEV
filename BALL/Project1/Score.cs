using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Score
    {
        public int PremierScore;
        public int DeuxiemeScore;

        private SpriteFont _font;
        
        public Score(SpriteFont font)
        {
            _font = font;
        }
        public void Draw( SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, PremierScore.ToString(), new Vector2(20, 0), Color.White);
            spriteBatch.DrawString(_font, PremierScore.ToString(), new Vector2(60, 0), Color.White);
        }

    }
}
