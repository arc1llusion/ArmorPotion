using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.TileEngine;
using ArmorPotions.Tiles;

namespace ArmorPotion.MapStuff
{
    static class MapLoader
    {
        public static Map Load(String mapTopLocation, String mapBottomLocation)
        {
            Map loadedMap = new Map(loadMap(mapTopLocation),loadMap(mapBottomLocation));

            return loadedMap;
        }

        public static Tile[,] loadMap(String fileLoaction)
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
                            int tileNum = int.Parse(tempArray[c].Substring(0,2));
                            switch (tileNum)
                            {
                                case 0:
                                   map[i,c] = null;
                                   break;
                                case 1:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 2:
                                   map[i, c] = new WallTile();
                                   break;
                                case 3:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 4:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 5:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 6:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 7:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 8:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 9:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 10:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 11:
                                   map[i, c] = new FloorTile();
                                   break;
                                case 12:
                                   map[i, c] = new FloorTile();
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
