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

        Tile[,] tileMapBotom = new Tile[50, 50];
        Tile[,] tileMapTop = new Tile[50, 50];
        World _world;

        public Map(Tile[,] mapTop, Tile[,] mapBottom, World world)
        {
            tileMapTop = mapTop;
            tileMapBotom = mapBottom;
            _world = world;
        }
        public void Update()
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            for (int i = 0; i <= tileMapBotom.GetLength(1) - 1; i++)
            {
                for (int c = 0; c <= tileMapBotom.GetLength(1) - 1; c++)
                {
                    if (tileMapBotom[c, i] != null)
                    {
                        spritebatch.Draw(tileMapBotom[c, i].Texture, tileMapBotom[c, i].Position - _world.Camera.CameraOffset, null, Color.White, 0f, Vector2.Zero, _world.Camera.Scale, SpriteEffects.None, 1f);
                    }
                    if (tileMapTop[c, i] != null)
                    {
                        spritebatch.Draw(tileMapTop[c, i].Texture, tileMapTop[c, i].Position - _world.Camera.CameraOffset, null, Color.White, 0f, Vector2.Zero, _world.Camera.Scale, SpriteEffects.None, 1f);
                    }
                    
                }
            }
        }
    }
}
