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
        private readonly InventoryManager _inventory;
        private Dictionary<String, AnimatedSprite> _heartAnimations;
        private List<AnimatedSprite> _hearts;

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

            _health = new AttributePair(400);
            _shield = new AttributePair(200);
            
            Rectangle windowBounds = World.Game.Window.ClientBounds;
            _inventory = new InventoryManager(new Vector2(10, windowBounds.Height - 70));
            _velocity = new Vector2(3, 3);

            this.XCollisionOffset = 30;
            this.TopCollisionOffset = (int)(CurrentSprite.Height / 2);

            _heartAnimations = new Dictionary<String, AnimatedSprite>();
            _hearts = new List<AnimatedSprite>();

            Animation fullHeartAnimation = new Animation(16, 44, 44, 0, 0);
            Animation threeQuarter = new Animation(16, 44, 44, 0, 44);
            Animation half = new Animation(16, 44, 44, 0, 88);
            Animation quarter = new Animation(16, 44, 44, 0, 132);
            Animation none = new Animation(16, 44, 44, 0, 176);
            Texture2D heartTexture = World.Game.Content.Load<Texture2D>(@"Gui/Hearts");

            fullHeartAnimation.FramesPerSecond = 8;

            Dictionary<AnimationKey, Animation> heartAnimation = new Dictionary<AnimationKey, Animation>();
            heartAnimation.Add(AnimationKey.Right, fullHeartAnimation);
            
            AnimatedSprite fullSprite = new AnimatedSprite(heartTexture, heartAnimation);
            fullSprite.IsAnimating = true;

            _heartAnimations.Add("Full", fullSprite);

            heartAnimation = new Dictionary<AnimationKey, Animation>();
            heartAnimation.Add(AnimationKey.Right, threeQuarter);

            _heartAnimations.Add("ThreeQuarter", new AnimatedSprite(heartTexture, heartAnimation));

            heartAnimation = new Dictionary<AnimationKey, Animation>();
            heartAnimation.Add(AnimationKey.Right, half);

            _heartAnimations.Add("HalfHeart", new AnimatedSprite(heartTexture, heartAnimation));

            heartAnimation = new Dictionary<AnimationKey, Animation>();
            heartAnimation.Add(AnimationKey.Right, quarter);

            _heartAnimations.Add("QuarterHeart", new AnimatedSprite(heartTexture, heartAnimation));

            heartAnimation = new Dictionary<AnimationKey, Animation>();
            heartAnimation.Add(AnimationKey.Right, none);

            _heartAnimations.Add("NoHeart", new AnimatedSprite(heartTexture, heartAnimation));

            PopulateHearts();
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

            _hearts.ForEach(heart => heart.Update(gameTime));

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
                            Tile tempTile = World.CurrentDungeon.getMapLevel(1)[c, i];
                            if (tempTile.TileID == 11 || tempTile.TileID == 12 || tempTile.TileID == 13)
                            {
                                tempTile.onEvent(EventType.IceEvent);
                            }
                            else
                            {
                            }
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
                            Tile tempTile = World.CurrentDungeon.getMapLevel(1)[c, i];
                            if (tempTile.TileID == 11 || tempTile.TileID == 12 || tempTile.TileID == 13)
                            {
                                tempTile.onEvent(EventType.FireEvent);
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }


            if (InputHandler.KeyPressed(Keys.P))
            {
                for (int i = 0; i <= World.CurrentDungeon.getMapLevel(1).GetLength(0) - 1; i++)
                {
                    for (int c = 0; c <= World.CurrentDungeon.getMapLevel(1).GetLength(1) - 1; c++)
                    {
                        if (World.CurrentDungeon.getMapLevel(1)[c, i] != null)
                        {
                            Tile tempTile = World.CurrentDungeon.getMapLevel(1)[c, i];
                            if (tempTile.TileID == 11 || tempTile.TileID == 12 || tempTile.TileID == 13)
                            {
                                tempTile.onEvent(EventType.LightningEvent);
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
            if (InputHandler.KeyPressed(Keys.U))
            {
                for (int i = 0; i <= World.CurrentDungeon.getMapLevel(1).GetLength(0) - 1; i++)
                {
                    for (int c = 0; c <= World.CurrentDungeon.getMapLevel(1).GetLength(1) - 1; c++)
                    {
                        if (World.CurrentDungeon.getMapLevel(1)[c, i] != null)
                        {
                            Tile tempTile = World.CurrentDungeon.getMapLevel(1)[c, i];
                            if (tempTile.TileID == 11 || tempTile.TileID == 12 || tempTile.TileID == 13)
                            {
                            }
                            else
                            {
                                tempTile.onEvent(EventType.DoorTrigger);
                            }
                        }
                    }
                }
            }
        }

        public void Damage(int value)
        {
            int shieldDamage = value / 4;
            float healthDamage = value;

            double shieldToHealthRatio = (float)_shield.CurrentValue / (float)_shield.MaximumValue;

            if (shieldToHealthRatio >= .50d)
                healthDamage /= 4;
            else if (shieldToHealthRatio >= .25d)
                healthDamage /= 2;
            else if (shieldToHealthRatio > 0d)
                healthDamage *= (2f/3f);

            _shield.Damage((ushort)shieldDamage);
            _health.Damage((ushort)healthDamage);

            PopulateHearts();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentSprite.Draw(gameTime, spriteBatch, PositionOffset, World.Camera);

            spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Player Shield: " + _shield.CurrentValue, new Vector2(500, 50), Color.White);

            int heartX = 10;
            int heartY = 10;

            for (int i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].Draw(gameTime, spriteBatch, new Vector2(heartX, heartY), World.Camera);
                heartX += _hearts[i].Width + 6;
            }

            if(_inventory.CurrentTempaQuip != null) spriteBatch.DrawString(World.Game.Content.Load<SpriteFont>(@"Fonts\ControlFont"), "Currently equipped: " + _inventory.CurrentTempaQuip.Name, new Vector2(300, 90), Color.White);

            int coordX = (int)Math.Ceiling( _position.X / Tile.Width);
            int coordY = (int)Math.Ceiling(_position.Y / Tile.Height);

            _inventory.Draw(gameTime, spriteBatch, World.Game.Window.ClientBounds.Bottom);
        }

        private void PopulateHearts()
        {
            _hearts.Clear();

            int currentHealth = _health.CurrentValue;
            while ((currentHealth / 100) >= 1)
            {
                _hearts.Add(_heartAnimations["Full"].Clone());
                currentHealth -= 100;
            }

            if (currentHealth > 75)
                _hearts.Add(_heartAnimations["Full"].Clone());
            else if (currentHealth > 50)
                _hearts.Add(_heartAnimations["ThreeQuarter"].Clone());
            else if (currentHealth > 25)
                _hearts.Add(_heartAnimations["HalfHeart"].Clone());
            else if (currentHealth > 0)
                _hearts.Add(_heartAnimations["QuarterHeart"].Clone());

            int emptyHearts = (_health.MaximumValue - _health.CurrentValue) / 100;

            for (int i = 0; i < emptyHearts; i++)
                _hearts.Add(_heartAnimations["NoHeart"].Clone());
        }
    }
}
