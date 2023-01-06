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
        private AnimatedSprite _perso;
        private AnimatedSprite _perso1;
        
        private TiledMapTileLayer mapLayer;//pour colision

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
            _positionPerso = new Vector2(250, 400);
            _positionPerso1 = new Vector2(250, 60);
            _vitessePerso = 100;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("map_tennis");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ALLSTARSspritesheet.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _perso1 = new AnimatedSprite(spriteSheet);
            
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("mur");///pour colision
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
             deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds);
            //float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            //float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            String animation = "ALLSTARSspritesheet.png";

            if (_keyboardState.IsKeyDown(Keys.Right) || (_keyboardState.IsKeyDown(Keys.Left)) || (_keyboardState.IsKeyDown(Keys.Up)) || (_keyboardState.IsKeyDown(Keys.Down)))
            {
                _tiledMapRenderer.Update(gameTime);
                _perso.Play("blue_breathing"); // une des animations définies dans « persoAnimation.sf »
                _perso.Update(deltaSeconds); // time écoulé
                J2Deplacement();
                PourColision("blue_normal_strike","blue_super_strike");
            }

            if (_keyboardState.IsKeyDown(Keys.S) || _keyboardState.IsKeyDown(Keys.F) || (_keyboardState.IsKeyDown(Keys.E)) || _keyboardState.IsKeyDown(Keys.V))
            {
                _tiledMapRenderer.Update(gameTime);
                _perso1.Play("red_breathing"); // une des animations définies dans « persoAnimation.sf »
                _perso1.Update(deltaSeconds); // time écoulé
                J1Deplacement();
                PourColision("red_normal_strike","red_super_strike");

            }
            

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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        
 
        public void J2Deplacement()
        {
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {   _sensPerso = 1;
                _positionPerso.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Left)) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {   _sensPerso = -1;
                _positionPerso.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Up)) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {   _sensPerso = -1;
                _positionPerso.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Down)) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {   _sensPerso = 1;
                _positionPerso.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }

        }
        public void J1Deplacement()
        {
            if (_keyboardState.IsKeyDown(Keys.F) && !(_keyboardState.IsKeyDown(Keys.S)))
            {                _sensPerso = 1;
                _positionPerso1.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if (_keyboardState.IsKeyDown(Keys.S) && !_keyboardState.IsKeyDown(Keys.F))
            {                _sensPerso = -1;
                _positionPerso1.X += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.E)) && !(_keyboardState.IsKeyDown(Keys.V)))
            {               _sensPerso = -1;  
                _positionPerso1.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }
            else if ((_keyboardState.IsKeyDown(Keys.V)) && !(_keyboardState.IsKeyDown(Keys.E)))
            {               _sensPerso = 1;
                _positionPerso1.Y += _sensPerso * _vitessePerso * deltaSeconds;
            }
        }
        public void PourColision(string normal,string super)
        {
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            //Console.WriteLine($"Position X : {_positionPerso.X} \nPosition Y : {_positionPerso.Y}");
            String animation = "normal";

            if (keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.5);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && !_keyboardState.IsKeyDown(Keys.Up))
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
            if (keyboardState.IsKeyDown(Keys.Left) && !_keyboardState.IsKeyDown(Keys.Right))
            {
                _sensPerso = 1;
                //_perso.Play("super");
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth-1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "super"; /// changer blue...
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                if (IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;

            }
            if (keyboardState.IsKeyDown(Keys.Right) && !_keyboardState.IsKeyDown(Keys.Left))
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
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
            
        }
        
    }
}