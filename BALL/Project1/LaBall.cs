using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class LaBall  
    {
         private float _timer = 1; // augmente la vitesse durant le temps
         private Vector2? _startPosition = null;
         private float? _startSpeed;
         private bool _isPlaying;
         public Vector2 Position;
         public Vector2 _vitesDep;
         public float Speed;

         public int VitesseIncremenetation = 10;


        public void Update(Game gametime )
        {
            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;

                Restart();  //crée une méthode commencement pour jouer
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;
            if (!_isPlaying)///////////
                return;

            if (_timer > VitesseIncremenetation)
            {
                Speed++;
                _timer = 0;
            }
        }

        public void Restart()
        {

            var direction = Game1.Hasard.Next(0, 4);

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
        
    }
}
