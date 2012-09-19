using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Characteristics;
using ArmorPotionFramework.Inventory;
using ArmorPotionFramework.Input;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.Data;
using ArmorPotionFramework.TileEngine;
using System;


namespace ArmorPotionFramework.EntityClasses
{
    public sealed class Player : Entity
    {
        public IncrementalPair _health;
        public IncrementalPair _shield;
        public readonly InventoryManager _inventory;

        public Player(World world, Texture2D texture)
            : base(world)
        {
            int spriteWidth = 128;
            int spriteHeight = 128;
            int frameCount = 4;

            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();

            Animation animation = new Animation(frameCount, spriteWidth, spriteHeight, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(frameCount, spriteWidth, spriteHeight, 0, spriteHeight);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(frameCount, spriteWidth, spriteHeight, 0, spriteHeight * 2);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(frameCount, spriteWidth, spriteHeight, 0, spriteHeight * 3);
            animations.Add(AnimationKey.Up, animation);

            AnimatedSprite sprite = new AnimatedSprite(
                texture,
                animations,
                Color.White);

            AnimatedSprites.Add("Normal", sprite);

            _health = new IncrementalPair(IncrementalValue.Full, 3);
            _shield = new IncrementalPair(IncrementalValue.Full, 3);
            _inventory = new InventoryManager();
            _velocity = new Vector2(3, 3);

            this.XCollisionOffset = 30;
            this.TopCollisionOffset = (int)(CurrentSprite.Height / 2);
        }

        public IncrementalPair Health
        {
            get { return this._health; }
            set { this._health = value; }
        }

        public IncrementalPair Shield
        {
            get { return this._shield; }
            set { this._shield = value; }
        }

        public InventoryManager Inventory
        {
            get { return _inventory; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaX = 0;
            float deltaY = 0;

            if (InputHandler.KeyDown(Keys.W) || InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.Y >= 0.5f)
            {
                deltaY -= _velocity.Y;
                CurrentSprite.CurrentAnimation = AnimationKey.Up;
                CurrentSprite.IsAnimating = true;
            }
            else if (InputHandler.KeyDown(Keys.S) || InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.Y <= -0.5f)
            {
                deltaY += _velocity.Y;
                CurrentSprite.CurrentAnimation = AnimationKey.Down;
                CurrentSprite.IsAnimating = true;
            }

            if (InputHandler.KeyDown(Keys.A) || InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.X <= -0.5f)
            {
                deltaX -= _velocity.X;
                CurrentSprite.CurrentAnimation = AnimationKey.Left;
                CurrentSprite.IsAnimating = true;
            }
            else if (InputHandler.KeyDown(Keys.D) ||InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.X >= 0.5f)
            {
                deltaX += _velocity.X;
                CurrentSprite.CurrentAnimation = AnimationKey.Right;
                CurrentSprite.IsAnimating = true;
            }

            if ((!InputHandler.KeyDown(Keys.A) && !InputHandler.KeyDown(Keys.D) && !InputHandler.KeyDown(Keys.W) && !InputHandler.KeyDown(Keys.S)) &&
                ((InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.X < 0.5f && InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.X > -0.5f)&&
            (InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.Y < 0.5f && InputHandler.GamePadStates[(int)PlayerIndex.One].ThumbSticks.Left.Y > -0.5f)))
            {
                CurrentSprite.IsAnimating = false;
                CurrentSprite.Reset();
            }

            



            if (InputHandler.MouseButtonDown(InputHandler.MouseState.LeftButton)||InputHandler.GamePadStates[(int)PlayerIndex.One].Buttons.Y == ButtonState.Pressed)
            {
                //object o = _inventory.CurrentInstaQuip;
                //_inventory.ConsumeInstaQuip(this);
                //Damage(IncrementalValue.Full, 1);

                _inventory.ActivateTempaQuip(gameTime, this);
            }

            Vector2 currentVelocity = new Vector2(deltaX, deltaY);
            CollisionData collisionData = HandleCollisions(currentVelocity);

            if(!collisionData.IsXAxisColliding) {
                _position.X += currentVelocity.X;
            }

            if (!collisionData.IsYAxisColliding)
            {
                _position.Y += currentVelocity.Y;
            }

            if (InputHandler.KeyPressed(Keys.Left) || InputHandler.ButtonPressed(Buttons.LeftShoulder, PlayerIndex.One))
                _inventory.SelectRelativeTempaQuip(this, -1);
            else if (InputHandler.KeyPressed(Keys.Right) || InputHandler.ButtonPressed(Buttons.RightShoulder,PlayerIndex.One))
                _inventory.SelectRelativeTempaQuip(this, 1);



            /*
             * Temp Code below for Switch check, Please Remove eventually
             * 
             * 
             */
            if (InputHandler.KeyPressed(Keys.I))
            {
                for (int i = 0; i <= World.CurrentDungeon.getMapLevel(1).GetLength(0) - 1; i++)
                {
                    for (int c = 0; c <= World.CurrentDungeon.getMapLevel(1).GetLength(1) - 1; c++)
                    {
                        if (World.CurrentDungeon.getMapLevel(1)[c, i] != null)
                        {
                            World.CurrentDungeon.getMapLevel(1)[c, i].onEvent(EventType.IceEvent);
                        }
                    }
                }
            }
            
            if (InputHandler.KeyPressed(Keys.O))
            {
                for (int i = 0; i <= World.CurrentDungeon.getMapLevel(1).GetLength(0) - 1; i++)
                {
                    for (int c = 0; c <= World.CurrentDungeon.getMapLevel(1).GetLength(1) - 1; c++)
                    {
                        if (World.CurrentDungeon.getMapLevel(1)[c, i] != null)
                        {
                            World.CurrentDungeon.getMapLevel(1)[c, i].onEvent(EventType.FireEvent);
                        }
                    }
                }
            }














        }

        public void Damage(IncrementalValue pair, int modifier)
        {
            float ratio = ((float)_shield.MaximumValue / _shield.CurrentValue);
            _shield.Damage(IncrementalValue.Quarter, 1);

            if (ratio < 1.5f)
                _health.Damage(IncrementalValue.Quarter, modifier);
            else if (ratio < 2f)
                _health.Damage(IncrementalValue.Half, modifier);
            else if (ratio < 2.5f)
                _health.Damage(IncrementalValue.ThreeQuarters, modifier);
            else if (ratio < 3.0f)
                _health.Damage(IncrementalValue.Full, modifier);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RectangleExtensions.DrawRectangleBorder(VisualBoundingRectangle, spriteBatch, 4, Color.White);

            CurrentSprite.Draw(gameTime, spriteBatch, PositionOffset, World.Camera);
            spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Player Health: " + _health.CurrentValue, new Vector2(500, 10), Color.White);
            spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Player Shield: " + _shield.CurrentValue, new Vector2(500, 50), Color.White);
            if(_inventory.CurrentTempaQuip != null) spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Currently equipped: " + _inventory.CurrentTempaQuip.Name, new Vector2(300, 90), Color.White);

            int coordX = (int)Math.Ceiling( _position.X / Tile.Width);
            int coordY = (int)Math.Ceiling(_position.Y / Tile.Height);

            spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Coords: " + + coordX + ":" + coordY, new Vector2(0, 0), Color.White);
        }
    }
}
