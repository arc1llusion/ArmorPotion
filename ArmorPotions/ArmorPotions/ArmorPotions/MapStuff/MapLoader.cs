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

namespace ArmorPotion.MapStuff
{
    static class MapLoader
    {
        public static Map Load(String mapTopLocation, String mapBottomLocation, World world)
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

            Texture2D[] tileTextureArray = new Texture2D[9];




            Map loadedMap = new Map(loadMap(mapTopLocation, textureDictionary), loadMap(mapBottomLocation, textureDictionary));

            return loadedMap;
        }

        public static Tile[,] loadMap(String fileLoaction, Dictionary<int, Texture2D> textureDict)
        {
            Tile[,] map = new Tile[50, 50];
          
            using (Stream fileStream = TitleContainer.OpenStream("Content/Map/Map.txt"))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    int numLines = int.Parse(reader.ReadLine());
                    int numCells = int.Parse(reader.ReadLine());
                    reader.ReadLine();
                    for (int i = 0; i <= numLines - 1; i++)
                    {
                        String temp = reader.ReadLine();
                        String[] tempArray = temp.Split('|');
                        for (int c = 0; c <= numCells - 1; c++)
                        {
                            int tileID = int.Parse(tempArray[c].Substring(0, 2));
                            int imageID = int.Parse(tempArray[c].Substring(2, 2));
                            switch (tileID)
                            {
                                case 0:
                                   map[c,i] = null;
                                   break;
                                case 1:
                                   map[c, i] = new FloorTile(TileType.Passable, textureDict[1]);
                                   break;
                                case 2:
                                   map[c, i] = new WallTile(TileType.NonPassable, textureDict[2]);
                                   break;
                                case 3:
                                   map[c, i] = new DoorTile(TileType.NonPassable, textureDict[3]);
                                   break;
                                case 4:
                                   map[c, i] = new FloorTile(TileType.Passable, textureDict[4]);
                                   break;
                                case 5:
                                   map[c, i] = new FloorTile(TileType.Hole, textureDict[5]);
                                   break;
                                case 6:
                                   map[c, i] = new WaterTile(TileType.Passable, textureDict[6], textureDict[9], false);
                                   break;
                                case 7:
                                   map[c, i] = new LavaTile(TileType.Hole, textureDict[7], textureDict[8], false);
                                   break;
                                case 8:
                                   map[c, i] = new LavaTile(TileType.Hole, textureDict[7], textureDict[8], true);
                                   break;
                                case 9:
                                   map[c, i] = new WaterTile(TileType.Passable, textureDict[6], textureDict[9], true);
                                   break;
                                case 10:
                                   map[c, i] = new FloorTile(TileType.Hole, textureDict[10]);
                                   break;
                                case 11:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, textureDict[11], textureDict[11], SwitchType.LightningSwitch);
                                   break;
                                case 12:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, textureDict[12], textureDict[12], SwitchType.LightningSwitch);
                                   break;
                                case 13:
                                   map[c, i] = new SwitchTile(TileType.NonPassable, textureDict[13], textureDict[13], SwitchType.LightningSwitch);
                                   break;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
