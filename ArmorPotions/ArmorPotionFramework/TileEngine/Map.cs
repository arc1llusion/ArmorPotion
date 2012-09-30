using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.CameraSystem;

namespace ArmorPotionFramework.TileEngine
{
    public class Map
    {
        private List<Tile[,]> _tileMaps;
        private int _width, _height;
        private World _world;
        private Camera _camera;
        private List<Enemy> _enemies;

        public Map(Tile[,] mapTop, Tile[,] mapBottom, List<Enemy> enemies, World world)
        {
            _tileMaps = new List<Tile[,]>();
            _tileMaps.Add(mapBottom);
            _tileMaps.Add(mapTop);

            _enemies = enemies;

            if (mapTop.GetLength(0) != mapBottom.GetLength(0) || mapBottom.GetLength(1) != mapTop.GetLength(1))
                throw new Exception("Invalid map lengths.");

            _width = mapTop.GetLength(0) - 1;
            _height = mapBottom.GetLength(1) - 1;

            _world = world;
            _camera = _world.Camera;
        }

        public List<Enemy> Enemies
        {
            get { return this._enemies; }
        }

        public void Update(GameTime gameTime)
        {
            UpdateEnemies(gameTime);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            List<Enemy> removedEnemies = new List<Enemy>();
            foreach (Enemy enemy in _enemies)
            {
                enemy.Update(gameTime);

                if (!enemy.IsAlive)
                    removedEnemies.Add(enemy);
            }

            foreach (Enemy enemy in removedEnemies)
            {
                _enemies.Remove(enemy);
            }
        }

        public TileInfo? GetTile(int layer, int tileX, int tileY)
        {
            if(tileX < 0 || tileY < 0 || tileX > _width || tileY > _height)
                return null;

            Tile tile = _tileMaps[layer][tileX, tileY];

            if (tile == null) return null;

            TileInfo info = new TileInfo(new Rectangle(
                                            (tileX) * Tile.Width, 
                                            (tileY) * Tile.Height, 
                                            Tile.Width, Tile.Height), 
                                            tile);

            return info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 cameraOffset = _camera.CameraOffset;
            int mapX = (int)(Math.Floor(cameraOffset.X / Tile.Width));
            int mapY = (int)(Math.Floor(cameraOffset.Y / Tile.Height));
            int mapWidth = mapX + (_world.MaxTileWidth + 1);
            int mapHeight = mapY + (_world.MaxTileHeight + 1);

            for (int i = mapY; i <= mapHeight; i++)
            {
                for (int c = mapX; c <= mapWidth; c++)
                {
                    if (_tileMaps[0][c, i] != null)
                    {
                        spriteBatch.Draw(_tileMaps[0][c, i].Texture, new Vector2(c * Tile.Width, i * Tile.Height) - cameraOffset, null, Color.White, 0f, Vector2.Zero, _camera.Scale, SpriteEffects.None, 0f);
                    }
                    if (_tileMaps[1][c, i] != null)
                    {
                        spriteBatch.Draw(_tileMaps[1][c, i].Texture, new Vector2(c * Tile.Width, i * Tile.Height) - cameraOffset, null, Color.White, 0f, Vector2.Zero, _camera.Scale, SpriteEffects.None, 0f);
                    }                    
                }
            }

            _enemies.ForEach(enemy => enemy.Draw(gameTime, spriteBatch));
        }

        public Tile[,] getMapLevel(int levelNum)
        {
            return _tileMaps[levelNum];
        }
    }
}
