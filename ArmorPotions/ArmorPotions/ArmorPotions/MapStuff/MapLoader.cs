using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.TileEngine;
using ArmorPotions.Tiles;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.EntityClasses;

namespace ArmorPotion.MapStuff
{
    static class MapLoader
    {
        public static Map Load(String mapTopLocation, String mapBottomLocation, String enemyLocation, World world)
        {
            Dictionary<int, Texture2D> textureDictionary = new Dictionary<int,Texture2D>();

            textureDictionary.Add(01, world.Game.Content.Load<Texture2D>(@"Tiles\FloorTile"));
            textureDictionary.Add(02, world.Game.Content.Load<Texture2D>(@"Tiles\WallTile"));
            textureDictionary.Add(03, world.Game.Content.Load<Texture2D>(@"Tiles\DoorTile"));
            textureDictionary.Add(04, world.Game.Content.Load<Texture2D>(@"Tiles\SwitchTile"));
            textureDictionary.Add(05, world.Game.Content.Load<Texture2D>(@"Tiles\HoleTile"));
            textureDictionary.Add(06, world.Game.Content.Load<Texture2D>(@"Tiles\WaterTile"));
            textureDictionary.Add(07, world.Game.Content.Load<Texture2D>(@"Tiles\LavaTile"));
            textureDictionary.Add(08, world.Game.Content.Load<Texture2D>(@"Tiles\CooledLavaTile"));
            textureDictionary.Add(09, world.Game.Content.Load<Texture2D>(@"Tiles\IceTile"));
            textureDictionary.Add(10, world.Game.Content.Load<Texture2D>(@"Tiles\DeepWaterTile"));
            textureDictionary.Add(11, world.Game.Content.Load<Texture2D>(@"Tiles\LightningSwitch"));
            textureDictionary.Add(12, world.Game.Content.Load<Texture2D>(@"Tiles\FireSwitch"));
            textureDictionary.Add(13, world.Game.Content.Load<Texture2D>(@"Tiles\IceSwitch"));
            textureDictionary.Add(14, world.Game.Content.Load<Texture2D>(@"Tiles\DoorOpenTile"));
            textureDictionary.Add(15, world.Game.Content.Load<Texture2D>(@"Tiles\LadderTile"));

            Map loadedMap = new Map(loadMap(mapTopLocation, textureDictionary), loadMap(mapBottomLocation, textureDictionary), LoadEnemies(world, enemyLocation), world);
            for (int i = 0; i <= loadedMap.getMapLevel(1).GetLength(0) - 1; i++)
            {
                for (int c = 0; c <= loadedMap.getMapLevel(1).GetLength(1) - 1; c++)
                {
                    Tile tempTile = loadedMap.getMapLevel(1)[c, i];
                    if (tempTile != null)
                    {
                        if (tempTile.TileID == 11||tempTile.TileID == 12||tempTile.TileID == 13)
                        {
                            SwitchTile switchTile = (SwitchTile)tempTile;
                            switchTile.parseOneselfAndAddThineSelfToThouDictionaryOfLinkedTileObjects_Cheers(loadedMap.getMapLevel(1));
                        }
                    }
                }
            }
            return loadedMap;
        }

        public static Tile[,] loadMap(String fileLoaction, Dictionary<int, Texture2D> textureDict)
        {
            Tile[,] map = new Tile[50, 50];
          
            using (Stream fileStream = TitleContainer.OpenStream(fileLoaction))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    for (int i = 0; i <=  map.GetLength(0) - 1; i++)
                    {
                        String temp = reader.ReadLine();
                        String[] tempArray = temp.Split('|');
                        for (int c = 0; c <= map.GetLength(1) - 1; c++)
                        {
                            int tileID = int.Parse(tempArray[c].Substring(0, 2));
                            int imageID = int.Parse(tempArray[c].Substring(2, 2));
                            switch (tileID)
                            {
                                case 0:
                                   map[c,i] = null;
                                   break;
                                case 1:
                                   map[c, i] = new FloorTile(TileType.Passable, tileID, textureDict[1]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 2:
                                   map[c, i] = new WallTile(TileType.NonPassable, tileID, textureDict[2]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 3:
                                   map[c, i] = new DoorTile(TileType.NonPassable, tileID, textureDict[3], textureDict[14]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 4:
                                   map[c, i] = new FloorTile(TileType.Passable, tileID, textureDict[4]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 5:
                                   map[c, i] = new FloorTile(TileType.Hole, tileID, textureDict[5]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 6:
                                   map[c, i] = new WaterTile(TileType.Passable, tileID, textureDict[6], textureDict[9], textureDict[10], false, false);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 7:
                                   map[c, i] = new LavaTile(TileType.Hole, tileID, textureDict[7], textureDict[8], false);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 8:
                                   map[c, i] = new LavaTile(TileType.Passable, tileID, textureDict[7], textureDict[8], true);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 9:
                                   map[c, i] = new WaterTile(TileType.Passable, tileID, textureDict[6], textureDict[9], textureDict[10], false, false);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 10:
                                   map[c, i] = new WaterTile(TileType.Passable, tileID, textureDict[6], textureDict[9], textureDict[10], true, false);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 11:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, tileID, textureDict[11], textureDict[11], SwitchType.LightningSwitch, tempArray[c].Substring(6));
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 12:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, tileID, textureDict[12], textureDict[12], SwitchType.FireSwitch, tempArray[c].Substring(6));
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 13:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, tileID, textureDict[13], textureDict[13], SwitchType.IceSwitch, tempArray[c].Substring(6));
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                                case 14:
                                   map[c, i] = new StairTile(TileType.NonPassable, tileID, textureDict[14]);
                                   map[c, i].Position = new Vector2(c*Tile.Width,i*Tile.Height);
                                   break;
                            }
                        }
                    }
                  
                }
            }
            return map;
        }

        private static List<Enemy> LoadEnemies(World world, String fileLocation)
        {
            List<Enemy> enemies = new List<Enemy>();
            using (Stream fileStream = TitleContainer.OpenStream(fileLocation))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    String enemy = "";
                    while ((enemy = reader.ReadLine()) != null)
                    {
                        String[] enemyData = enemy.Split('|');

                        Enemy loadedEnemy = world.EnemyFactory.Create(enemyData[0]);
                        loadedEnemy.Position = new Vector2(float.Parse(enemyData[1]), float.Parse(enemyData[2]));

                        enemies.Add(loadedEnemy);
                    }
                }
            }

            return enemies;
        }
    }
}
