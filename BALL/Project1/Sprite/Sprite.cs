using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Content;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Sprite
{
    internal class Sprite : Game
    {
        public Vector2 Position;
        //public Entrer Entrer;

        private Vector2 _positionPerso;
        private Vector2 _positionPerso1;
        private AnimatedSprite _perso;
        private AnimatedSprite _perso1;
        private int _sensPerso;
        private int _vitessePerso;

        private KeyboardState _keyboardState;
        private int deltaSeconds;

        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLine;
        private TiledMap _tiledMap;

        public Sprite()
        {

        }



        public void LoadContent(Vector2 positionPerso, Vector2 positionPerso1, int vitessePerso, Game1 game)
        {
            _vitessePerso = vitessePerso;
            _positionPerso1 = positionPerso1;
            _positionPerso = positionPerso;
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ALLSTARSspritesheet.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _perso1 = new AnimatedSprite(spriteSheet);
            

        }
        public void DeplacementsPerso(float detaSeconds, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer mapLine, Game1 game)
        {
            float walkSpeed = deltaSeconds * _vitessePerso;

            KeyboardState keyboardState = Keyboard.GetState();
            _keyboardState = Keyboard.GetState();
            J2Deplacement();
            //float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            //Console.WriteLine($"Position X : {_positionPerso.X} \nPosition Y : {_positionPerso.Y}");
            String animation = "normal";

            if (_keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed; /*1;*/
                if (IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Down) && !_keyboardState.IsKeyDown(Keys.Up))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Left) && !_keyboardState.IsKeyDown(Keys.Right))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Right) && !_keyboardState.IsKeyDown(Keys.Left))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.3);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
            }

        }

        public void J2Deplacement()
        {
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensPerso = 1;
                _positionPerso.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Left)) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensPerso = -1;
                _positionPerso.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Up)) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensPerso = -1;
                _positionPerso.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Down)) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensPerso = 1;
                _positionPerso.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }

        }
        
        public void PourColision(string normal, string super)
        {
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            //Console.WriteLine($"Position X : {_positionPerso.X} \nPosition Y : {_positionPerso.Y}");
            String animation = "normal";

            if (_keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed; /*1;*/
                if (IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Down) && !_keyboardState.IsKeyDown(Keys.Up))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Left) && !_keyboardState.IsKeyDown(Keys.Right))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.Right) && !_keyboardState.IsKeyDown(Keys.Left))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.3);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
            }
        }

        
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            TiledMapTile? til;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
            { return false; }
            if (!tile.Value.IsBlank)
                return true;
            //return false;
            if (mapLine.TryGetTile(x, y, out til) == false)
            { return false; }
            if (!til.Value.IsBlank)
                return true;
            return false;

        }
    }
}
