using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ArmorPotionFramework.Input;

namespace ArmorPotionFramework.Characteristics
{
    public class HealthClock
    {
        private AttributePair _health;
        private AttributePair _shield;
        private Vector2 _position;

        private Texture2D _healthClock_Back;
        private Texture2D _healthClock_Center;
        private Texture2D _healthClock_HeartHand;
        private Texture2D _healthClock_ShieldHand;

        private Vector2 _heartRotationOrigin;
        private Vector2 _shieldRotationOrigin;

        private float _heartRotation;
        private float _shieldRotation;

        private const float SCALE = .75f;

        //timing related
        private float _currentHeartAngle;
        private float _currentShieldAngle;

        private float _targetHeartAngle;
        private float _targetShieldAngle;

        private const float DELTA_ANGLE = 1f / (2f * (float)Math.PI);

        private readonly int _maxWaitTime;
        private int _currentTime;


        public HealthClock(AttributePair health, AttributePair shield, Vector2 position, ContentManager content)
        {
            _health = health;
            _shield = shield;
            _position = position;

            _healthClock_Back = content.Load<Texture2D>(@"Gui/HealthClock_Back");
            _healthClock_Center = content.Load<Texture2D>(@"Gui/HealthClock_Center");
            _healthClock_HeartHand = content.Load<Texture2D>(@"Gui/HealthClock_HeartHand");
            _healthClock_ShieldHand = content.Load<Texture2D>(@"Gui/HealthClock_ShieldHand");

            _heartRotationOrigin = new Vector2(_healthClock_HeartHand.Width / 2, _healthClock_HeartHand.Height / 2);
            _shieldRotationOrigin = new Vector2(_healthClock_ShieldHand.Width / 2, _healthClock_ShieldHand.Height / 2);

            _heartRotation = 0f;

            _maxWaitTime = 40;
            _currentTime = _maxWaitTime;

            _currentHeartAngle = -2f * (float)Math.PI;
            _currentShieldAngle = -2f * (float)Math.PI;
        }

        public void Update(GameTime gameTime)
        {
            double healthRatio = -360 * ((double)_health.CurrentValue / (double)_health.MaximumValue);
            float radians = MathHelper.ToRadians((float)healthRatio);
            _targetHeartAngle = radians;

            double shieldRatio = -360 * ((double)_shield.CurrentValue / (double)_shield.MaximumValue);
            radians = MathHelper.ToRadians((float)shieldRatio);
            _targetShieldAngle = radians;

            _currentTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTime < 0)
            {
                _currentTime = _maxWaitTime;
                if (_currentHeartAngle < _targetHeartAngle)
                {
                    _currentHeartAngle += DELTA_ANGLE;
                    if (_currentHeartAngle > 0) _currentHeartAngle = 0;
                }

                if (_currentShieldAngle < _targetShieldAngle)
                {
                    _currentShieldAngle += DELTA_ANGLE;
                    if (_currentShieldAngle > 0) _currentShieldAngle = 0;
                }
            }

            _heartRotation = _currentHeartAngle;
            _shieldRotation = _currentShieldAngle;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_healthClock_Back, _position, null, Color.White, 0f, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
            spriteBatch.Draw(_healthClock_Center, _position, null, Color.White, 0f, Vector2.Zero, SCALE, SpriteEffects.None, 0f);
            spriteBatch.Draw(_healthClock_HeartHand, _position + _heartRotationOrigin * SCALE, null, Color.White, _heartRotation, _heartRotationOrigin, SCALE, SpriteEffects.None, 0f);
            spriteBatch.Draw(_healthClock_ShieldHand, _position + _shieldRotationOrigin * SCALE, null, Color.White, _shieldRotation, _shieldRotationOrigin, SCALE, SpriteEffects.None, 0f);
        }
    }
}


