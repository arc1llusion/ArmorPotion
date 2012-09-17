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
        Tile linkedTile;
        bool _switchOn;
        public SwitchTile(TileType tileType, Texture2D switchOffTexture, Texture2D switchOnTexture, SwitchType setType)
            : base(tileType, switchOffTexture)
        {
            switchType = setType;
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
                linkedTile.onEvent(EventType.DoorTrigger);
            }
        }
    }
}
