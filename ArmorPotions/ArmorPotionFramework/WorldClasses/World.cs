using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Game;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Input;
using ArmorPotionFramework.CameraSystem;
using ArmorPotionFramework.Projectiles;

namespace ArmorPotionFramework.WorldClasses
{
    public class World
    {
        private Player _player;
        private ArmorPotionsGame _game;
        public Item item;
        private Camera _camera;
        private List<Enemy> _enemies;
        private List<Projectile> _projectiles;

        public World(ArmorPotionsGame game)
        {
            _game = game;
            _enemies = new List<Enemy>();
            _projectiles = new List<Projectile>();
            _player = new Player(this, _game.Content.Load<Texture2D>(@"Player\PlayerWalking"));

            _camera = new Camera(_game, 0, 0, _game.Window.ClientBounds.Width, _game.Window.ClientBounds.Height, 1);
            _game.Components.Add(new InputHandler(_game));
            _game.Components.Add(_camera);
        }

        public ArmorPotionsGame Game
        {
            get
            {
                return _game;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public List<Enemy> Enemies
        {
            get
            {
                return _enemies;
            }
        }

        public List<Projectile> Projectiles
        {
            get
            {
                return this._projectiles;
            }
        }

        public Camera Camera
        {
            get
            {
                return _camera;
            }
            set
            {
                _camera = value;
            }
        }

        public void Update(GameTime gameTime)
        {
            _enemies.ForEach(enemy => enemy.Update(gameTime));
            _projectiles.ForEach(enemy => enemy.Update(gameTime));
            _player.Update(gameTime);

            if (_player.Position.X > 70 &&
                _player.Position.X < 70 + 256 &&
                _player.Position.Y > 70 &&
                _player.Position.Y < 70 + 256)
            {
                if (item != null) { item.CollectedBy(_player); item = null; };
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _enemies.ForEach(enemy => enemy.Draw(gameTime, spriteBatch));
            _projectiles.ForEach(enemy => enemy.Draw(gameTime, spriteBatch));
            _player.Draw(gameTime, spriteBatch);
            if(item != null) item.DrawIcon(spriteBatch);
        }
    }
}
