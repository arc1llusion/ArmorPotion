using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ArmorPotionFramework.Game;
using ArmorPotionFramework.Input;

namespace ArmorPotionFramework.CameraSystem
{
    public class Camera : GameComponent
    {
        float _cameraX = 0;
        float _cameraY = 0;
        float _cameraW = 0;
        float _cameraH = 0;

        /// for fun assuming Y is Depth and X is width Z is height
        int _cameraYaw = 0;

        float _scale = 0;

        Vector2 _cameraCenter;

        public Camera(ArmorPotionsGame game, float _setX, float _setY, float _setW, float _setH, float _setScale) : base(game)
        {
            this._cameraX = _setX;
            this._cameraY = _setY;
            this._cameraW = _setW;
            this._cameraH = _setH;
            this._cameraCenter.X = this._cameraX+(this.CameraW/2);
            this._cameraCenter.Y = this._cameraY+(this.CameraH/2);
            this._scale = _setScale;

        }

        public float CameraX
        {
            get
            {
                return _cameraX;
            }
            set
            {
                _cameraCenter.X = value + (this.CameraW / 2);
                _cameraX = value;
            }
        }
        public float CameraY
        {
            get
            {
                return _cameraY;
            }
            set
            {
                _cameraCenter.Y = value + (this.CameraH / 2);
                _cameraY = value;
            }
        }
        public float CameraW
        {
            get
            {
                return _cameraW;
            }
            set
            {
                _cameraCenter.X = _cameraX + (value / 2);
                _cameraW = value;
            }
        }
        public float CameraH
        {
            get
            {
                return _cameraH;
            }
            set
            {
                _cameraCenter.Y = _cameraY + (value / 2);
                _cameraH = value;
            }
        }

        public Vector2 cameraCenter
        {
            get
            {
                return _cameraCenter;
            }
            set
            {
                _cameraX =(int)value.X - (this._cameraW / 2);
                _cameraY =(int)value.Y - (this._cameraH / 2);
                _cameraCenter = value;
            }
        }

        public float Scale
        {
            get 
            {
                return _scale;
            }
            set 
            {
                this._scale = value;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyboardState.IsKeyDown(Keys.M))
            {
                _scale += 0.25f;
            }
        }

    }
}
