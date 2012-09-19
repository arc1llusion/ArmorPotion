using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    public enum SwitchType
    {
        LightningSwitch = 0,
        FireSwitch = 1,
        IceSwitch = 2
    }
    public class SwitchTile : Tile
    {
        SwitchType switchType;
        Dictionary<EventType, List<Tile>> linkedTileDict;
        String unparsedCoordinateString;
        bool _switchOn;
        public SwitchTile(TileType tileType, int tileID, Texture2D switchOffTexture, Texture2D switchOnTexture, SwitchType setType, String linkedTileString)
            : base(tileType, tileID, switchOffTexture)
        {
            switchType = setType;
            linkedTileDict = new Dictionary<EventType,List<Tile>>();
            unparsedCoordinateString = linkedTileString;
        }

        public override void onEvent(EventType sendEvent)
        {
            if (!_isHidden)
            {
                if (sendEvent == EventType.FireEvent || sendEvent == EventType.LightningEvent || sendEvent == EventType.IceEvent)
                {
                    if (switchType == SwitchType.FireSwitch && sendEvent == EventType.FireEvent)
                    {
                        triggerTheFuxingSwitches();
                    }
                    else if (switchType == SwitchType.IceSwitch && sendEvent == EventType.IceEvent)
                    {
                        triggerTheFuxingSwitches();
                    }
                    else if (switchType == SwitchType.LightningSwitch && sendEvent == EventType.LightningEvent)
                    {
                        triggerTheFuxingSwitches();
                    }
                }
            }
            else
            {
            }


        }

        public void triggerTheFuxingSwitches()
        {
            foreach (EventType type in linkedTileDict.Keys)
            {
                foreach (Tile tile in linkedTileDict[type])
                {
                    tile.onEvent(type);
                }
            }

        }

        public void addTile(EventType typeToTrigger, Tile tileToBeAdded)
        {
            if(!linkedTileDict.Keys.Contains(typeToTrigger)){
                linkedTileDict.Add(typeToTrigger, new List<Tile>());
            }

            linkedTileDict[typeToTrigger].Add(tileToBeAdded);
        }

        public void parseOneselfAndAddThineSelfToThouDictionaryOfLinkedTileObjects_Cheers(Tile[,] mapOfTiles)
        {
            int numberOfLinkingTiles = (int)int.Parse(unparsedCoordinateString.Substring(0,2));
            for (int i = 0; i <= numberOfLinkingTiles-1; i++)
            {
                int xCoordinate = (int)int.Parse(unparsedCoordinateString.Substring(((i * 6) + 2), 2))-1;
                int yCoordinate = (int)int.Parse(unparsedCoordinateString.Substring(((i * 6) + 4), 2))-1;
                int triggerType = (int)int.Parse(unparsedCoordinateString.Substring(((i * 6) + 6), 2));
                addTile((EventType)Enum.Parse(typeof(EventType), "" + triggerType), mapOfTiles[xCoordinate, yCoordinate]);
            }

        }


    }
}
