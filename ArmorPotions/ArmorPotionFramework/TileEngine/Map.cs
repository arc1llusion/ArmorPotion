using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.WorldClasses;

namespace ArmorPotionFramework.TileEngine
{
    public class Map
    {
        private List<Tile[,]> _tileMaps;
        private int _width, _height;
        private World _world;

        public Map(Tile[,] mapTop, Tile[,] mapBottom, World world)
        {
            _tileMaps = new List<Tile[,]>();
            _tileMaps.Add(mapBottom);
            _tileMaps.Add(mapTop);

            if (mapTop.GetLength(0) != mapBottom.GetLength(0) || mapBottom.GetLength(1) != mapTop.GetLength(1))
                throw new Exception("Invalid map lengths.");

            _width = mapTop.GetLength(0) - 1;
            _height = mapBottom.GetLength(1) - 1;

            _world = world;
        }
        public void Update()
        {
        }

        public TileInfo? GetTile(int layer, int tileX, int tileY)
        {
            if(tileX < 0 || tileY < 0 || tileX > _width || tileY > _height)
                return null;

            Tile tile = _tileMaps[layer][tileX, tileY];

            if (tile == null) return null;

            TileInfo info = new TileInfo(new Rectangle(
                                            (tileX) * Tile.Width + 1, 
                                            (tileY) * Tile.Height + 1, 
                                            Tile.Width + 1, Tile.Height + 1), 
                                            tile.TileType);

            return info;
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            for (int i = 0; i <= _width; i++)
            {
                for (int c = 0; c <= _height; c++)
                {
                    if (_tileMaps[0][c, i] != null)
                    {
                        spritebatch.Draw(_tileMaps[0][c, i].Texture, new Vector2(i * Tile.Width, c * Tile.Height) - _world.Camera.CameraOffset, null, Color.White, 0f, Vector2.Zero, _world.Camera.Scale, SpriteEffects.None, 0f);
                    }
                    if (_tileMaps[1][c, i] != null)
                    {
                        spritebatch.Draw(_tileMaps[1][c, i].Texture, new Vector2(i * Tile.Width, c * Tile.Height) - _world.Camera.CameraOffset, null, Color.White, 0f, Vector2.Zero, _world.Camera.Scale, SpriteEffects.None, 0f);
                    }                    
                }
            }
        }
    }
}
