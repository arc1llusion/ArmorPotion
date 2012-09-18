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
                        _switchOn = !_switchOn;
                    }
                    else if (switchType == SwitchType.IceSwitch && sendEvent == EventType.IceEvent)
                    {
                        _switchOn = !_switchOn;
                    }
                    else if (switchType == SwitchType.LightningSwitch && sendEvent == EventType.LightningEvent)
                    {
                        _switchOn = !_switchOn;
                    }
                }
            }
            else
            {
            }


            if (_switchOn)
            {
                foreach (EventType type in linkedTileDict.Keys)
                {
                    foreach (Tile tile in linkedTileDict[type])
                    {
                        tile.onEvent(type);
                    }
                }
            }
        }

        public void addTile(EventType typeToTrigger, Tile tileToBeAdded)
        {
            if(linkedTileDict[typeToTrigger] == null){
                linkedTileDict.Add(typeToTrigger, new List<Tile>());
            }

            linkedTileDict[typeToTrigger].Add(tileToBeAdded);
        }

        public void parseOneselfAndAddThineSelfToThouDictionaryOfLinkedTileObjects_Cheers(Tile[,] mapOfTiles)
        {
            for (int i = 0; i <= mapOfTiles.GetLength(0) - 1; i++)
            {
                for (int c = 0; c <= mapOfTiles.GetLength(1) - 1; c++)
                {

                }
            }

        }


    }
}
