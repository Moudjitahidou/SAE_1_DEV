using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Ball
    {
        /*private float _timer = 1; // augmente la vitesse durant le temps
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;
        public Vector2 Position;
        public Vector2 _vitesDep;

        //public Score Score;

        public int VitesseIncremenetation = 5;

         public Ball(Texture2D texture)
           : base(texture)
         {
             Vitesse = 3;
         }

         public override void Update(GameTime gameTime, List<Sprite> sprites)
         {
             if (_startPosition == null)
             {
                 _startPosition = Position;
                 _startSpeed = Speed;

                 Restart();
             }

             if (Keyboard.GetState().IsKeyDown(Keys.Space))
                 _isPlaying = true;

             if (!_isPlaying)
                 return;

             _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

             if (_timer > VitesseIncremenetation)
             {
                 Speed++;
                 _timer = 0;
             }

             foreach (var sprite in sprites)
             {
                 if (sprite == this)
                     continue;

                 if (this._vitesDep.X > 0 && this.TouchGauche(sprite))
                     this._vitesDep.X = -this._vitesDep.X;
                 if (this._vitesDep.X < 0 && this.TouchDroit(sprite))
                     this._vitesDep.X = -this._vitesDep.X;
                 if (this._vitesDep.Y > 0 && this.TouchHaut(sprite))
                     this._vitesDep.Y = -this._vitesDep.Y;
                 if (this._vitesDep.Y < 0 && this.TouchBas(sprite))
                     this._vitesDep.Y = -this._vitesDep.Y;
             }

             if (Position.Y <= 0 || Position.Y + _texture.Height >= Game1.ScreenHeight)
                 _vitesDep.Y = -_vitesDep.Y;

             if (Position.X <= 0)
             {
                 Score.Score2++;
                 Restart();
             }

             if (Position.X + _texture.Width >= Game1.ScreenWidth)
             {
                 Score.Score1++;
                 Restart();
             }

             Position += _vitesDep * Speed;
         }

         public void Restart()
         {
             var direction = Game1.Random.Next(0, 4);

             switch (direction)
             {
                 case 0:
                     _vitesDep = new Vector2(1, 1);
                     break;
                 case 1:
                     _vitesDep = new Vector2(1, -1);
                     break;
                 case 2:
                     _vitesDep = new Vector2(-1, -1);
                     break;
                 case 3:
                     _vitesDep = new Vector2(-1, 1);
                     break;
             }

             Position = (Vector2)_startPosition;
             Speed = (float)_startSpeed;
             _timer = 0;
             _isPlaying = false;
         }
        */
     
    }
}
