using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.Game;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Characteristics;
using ArmorPotionFramework.Inventory;
using ArmorPotionFramework.Input;
using Microsoft.Xna.Framework.Input;
using ArmorPotionFramework.TileEngine;
using ArmorPotionFramework.Utility;


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

            if (InputHandler.KeyDown(Keys.W))
            {
                deltaY -= _velocity.Y;
                CurrentSprite.CurrentAnimation = AnimationKey.Up;
                CurrentSprite.IsAnimating = true;
            }
            else if (InputHandler.KeyDown(Keys.S))
            {
                deltaY += _velocity.Y;
                CurrentSprite.CurrentAnimation = AnimationKey.Down;
                CurrentSprite.IsAnimating = true;
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                deltaX -= _velocity.X;
                CurrentSprite.CurrentAnimation = AnimationKey.Left;
                CurrentSprite.IsAnimating = true;
            }
            else if (InputHandler.KeyDown(Keys.D))
            {
                deltaX += _velocity.X;
                CurrentSprite.CurrentAnimation = AnimationKey.Right;
                CurrentSprite.IsAnimating = true;
            }

            if (InputHandler.KeyReleased(Keys.A) || InputHandler.KeyReleased(Keys.D) || InputHandler.KeyReleased(Keys.W) || InputHandler.KeyReleased(Keys.S))
            {
                CurrentSprite.IsAnimating = false;
                CurrentSprite.Reset();
            }

            if (InputHandler.KeyDown(Keys.Space))
            {
                //object o = _inventory.CurrentInstaQuip;
                //_inventory.ConsumeInstaQuip(this);
                //Damage(IncrementalValue.Full, 1);

                _inventory.ActivateTempaQuip(gameTime, this);
            }

            HandleCollisions(new Vector2(deltaX, deltaY));

            if (InputHandler.KeyPressed(Keys.Left))
                _inventory.SelectRelativeTempaQuip(this, -1);
            else if (InputHandler.KeyPressed(Keys.Right))
                _inventory.SelectRelativeTempaQuip(this, 1);
        }

        private void HandleCollisions(Vector2 velocity)
        {
            Rectangle bounds = BoundingRectangle;

            int leftTile = (int)Math.Floor(((float)bounds.Left + velocity.X) / Tile.Width);
            int rightTile = (int)Math.Ceiling((((float)bounds.Right + velocity.X) / Tile.Width)) - 1;
            int topTile = (int)Math.Floor(((float)bounds.Top + velocity.Y) / Tile.Height);
            int bottomTile = (int)Math.Ceiling((((float)bounds.Bottom  + velocity.Y) / Tile.Height)) - 1;

            Map map = World.CurrentDungeon;

            bool collided = false;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    TileInfo? tile = map.GetTile(1, x, y);
                    if (tile.HasValue)
                    {
                        Rectangle tileRect = tile.Value.Bounds;
                        Rectangle velRectangle = new Rectangle(
                                                            bounds.X + (int)velocity.X,
                                                            bounds.Y + (int)velocity.Y,
                                                            bounds.Width,
                                                            bounds.Height);

                                         
                        collided = velRectangle.Intersects(tileRect);
                    }
                }
            }

            if (!collided)
                _position += velocity;
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
        }
    }
}
