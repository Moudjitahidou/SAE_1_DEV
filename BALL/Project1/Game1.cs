using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private KeyboardState _keyboardState;

        private Vector2 _positionPerso;
        private Vector2 _positionPerso1;
        private Vector2 _positionBall;///////ball

        private AnimatedSprite _perso;
        private AnimatedSprite _perso1;
        private AnimatedSprite _ball;///////ball

        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLine;//pour colision

        public static Random Hasard;///BALL.2
        private int _sensPerso;
        private int _vitessePerso;
        float deltaSeconds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _positionPerso = new Vector2(250, 405);
            _positionPerso1 = new Vector2(250, 65);
            _positionBall = new Vector2(250, 200);
           
            _vitessePerso = 100;
            Hasard = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("map_tennis");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ALLSTARSspritesheet.sf", new JsonContentLoader());
            SpriteSheet spriteBall = Content.Load<SpriteSheet>("spriteBall.sf", new JsonContentLoader());///////ball
            _perso = new AnimatedSprite(spriteSheet);
            _perso1 = new AnimatedSprite(spriteSheet);
            _ball = new AnimatedSprite(spriteBall);///////ball
            var ballTexture= 


            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("mur");///pour 
            mapLine = _tiledMap.GetLayer<TiledMapTileLayer>("midline");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;
            _keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds);

            if (_keyboardState.IsKeyDown(Keys.Right) || (_keyboardState.IsKeyDown(Keys.Left)) || (_keyboardState.IsKeyDown(Keys.Up)) || (_keyboardState.IsKeyDown(Keys.Down)))
            {
                _tiledMapRenderer.Update(gameTime);
                _perso.Play("blue_breathing"); // une des animations définies dans « persoAnimation.sf »
                _perso.Update(deltaSeconds); // time écoulé
                J2Deplacement();
                PourColision("blue_normal_strike", "blue_super_strike");
            }

            if (_keyboardState.IsKeyDown(Keys.S) || _keyboardState.IsKeyDown(Keys.F) || (_keyboardState.IsKeyDown(Keys.E)) || _keyboardState.IsKeyDown(Keys.V))
            {
                _tiledMapRenderer.Update(gameTime);
                _perso1.Play("red_breathing"); // une des animations définies dans « persoAnimation.sf »
                _perso1.Update(deltaSeconds); // time écoulé
                J1Deplacement();
                PourColision1("red_normal_strike", "red_super_strike");
            }
            /////////////////////////////////////////////////////////////////////////////////////////////
            if (_keyboardState.IsKeyDown(Keys.Space))
            {
                _tiledMapRenderer.Update(gameTime);
                _ball.Play("anime1");
                _ball.Update(deltaSeconds);
                _sensPerso = 1;
                _positionBall.Y += _sensPerso * _vitessePerso * deltaSeconds;
                ushort tx = (ushort)(_positionBall.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionBall.Y / _tiledMap.TileHeight - 0.5);
                //animation = "super"; /// changer blue...
                if (!BallCollision(tx, ty))
                    _positionBall.Y -= walkSpeed; /*1;*/
                if (BallCollision(tx, ty))
                    _positionBall.Y += walkSpeed;


                /*ushort tx = (ushort)(_positionBall.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionBall.Y / _tiledMap.TileHeight - 0.5);
                if (BallCollision(tx, ty))
                    _positionBall.Y -= walkSpeed; 
                if (BallCollision(tx, ty))
                    _positionBall.Y += walkSpeed;*/
            }


           
            //////////////////////////////////////////////////////////////////////////////////////////////////
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.Draw(_perso1, _positionPerso1);
            _spriteBatch.Draw(_ball, _positionBall);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// ////////
        /// </summary>


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
        public void J1Deplacement()
        {
            if (_keyboardState.IsKeyDown(Keys.F) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                _sensPerso = 1;
                _positionPerso1.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if (_keyboardState.IsKeyDown(Keys.S) && !_keyboardState.IsKeyDown(Keys.F))
            {
                _sensPerso = -1;
                _positionPerso1.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.E)) && !(_keyboardState.IsKeyDown(Keys.V)))
            {
                _sensPerso = -1;
                _positionPerso1.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.V)) && !(_keyboardState.IsKeyDown(Keys.E)))
            {
                _sensPerso = 1;
                _positionPerso1.Y += _sensPerso * _vitessePerso * deltaSeconds;
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
        public void PourColision1(string normal, string super)
        {
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            //Console.WriteLine($"Position X : {_positionPerso.X} \nPosition Y : {_positionPerso.Y}");
            String animation = "normal";

            if (_keyboardState.IsKeyDown(Keys.E) && !_keyboardState.IsKeyDown(Keys.V))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight - 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso1.Y -= walkSpeed; /*1;*/
                if (IsCollision(tx, ty))
                    _positionPerso1.Y += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.V) && !_keyboardState.IsKeyDown(Keys.E))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight + 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso1.Y += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso1.Y -= walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.S) && !_keyboardState.IsKeyDown(Keys.F))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso1.X -= walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso1.X += walkSpeed;
            }

            if (_keyboardState.IsKeyDown(Keys.F) && !_keyboardState.IsKeyDown(Keys.S))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso1.X / _tiledMap.TileWidth + 0.3);
                ushort ty = (ushort)(_positionPerso1.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso1.X += walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso1.X -= walkSpeed;
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
        private bool BallCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
            { return false; }
            if (!tile.Value.IsBlank)
                return true;
            return false;

        }
    }
    }