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
         private float _timer = 1 ; // augmente la vitesse durant le temps
         private Vector2? _startPosition = null;
         private float? _startSpeed;
         private bool _isPlaying;
         public Vector2 Position;
         public Vector2 _vitesDep;
         public float Speed;

         public int VitesseIncremenetation = 10;
        private object _spriteBatch;

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

            /*

            //int conteur = 0;
            int x = d.Next(0, TAILLE_FENETRE - RAYON * 2);
            balle.Position = new Vector2f(x, 0);
            Vector2f deplacement = new Vector2f(1, 1);
            int xMax = TAILLE_FENETRE - RAYON * 2;
            int yMax = TAILLE_FENETRE - RAYON * 2;
            while (fenetre.IsOpen)
            {
                balle.Position = balle.Position + deplacement;
                fenetre.DispatchEvents();

                if (balle.Position.X > xMax)
                {
                    deplacement.X = -deplacement.X;
                    conteur = conteur + 1;
                }
                if (balle.Position.X < 0)
                {
                    deplacement.X = -deplacement.X;
                }
                if (balle.Position.Y > yMax)
                {
                    deplacement.Y = -deplacement.Y;
                }
                if (balle.Position.Y < 0)
                    deplacement.Y = -deplacement.Y;

                fenetre.Clear(Color.Black);

                //Font laPolice = new Font("./police/police.TTF");
                //Text leTexte = new Text("Hellow", laPolice);
                //fenetre.Draw(leTexte);

                fenetre.Draw(balle);

                fenetre.Display();

                //fenetre.TextEntered += OnTextEntered
            }
            */






        }




            
        
    }
}
