using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Game;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.EntityClasses.Components;
using Microsoft.Xna.Framework.Content;

using ArmorPotionFramework.EntityClasses.Data;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotionFramework.EntityClasses
{
    public class Enemy : Entity
    {
        private Dictionary<String, IAIComponent> _actionComponents;
        private IAIComponent _decisionComponent;
        private IAIComponent _idleComponent;

        private bool _isAction;
        private float _sightRadius;

        private delegate void Action(GameTime gameTime, Enemy enemy);

        private Action<GameTime, Enemy> _currentAction;

        public Enemy(World world)
            : base(world)
        {
            _isAction = false;
            _sightRadius = 100;
            _actionComponents = new Dictionary<String, IAIComponent>();
        }

        public Enemy(World world, EnemyData data)
            : base(world)
        {
            _isAction = false;
            _sightRadius = data.SightRadius;

            this.Texture = data.Texture;
            this.Velocity = data.Velocity;

            this.AnimatedSprites = data.DeepCopySprites;
            this._idleComponent = data.IdleComponent;
            this._decisionComponent = data.DecisionComponent;
            this._actionComponents = data.ActionComponents;

            this.LeftCollisionOffset = data.LeftCollisionOffset;
            this.RightCollisionOffset = data.RightCollisionOffset;
            this.TopCollisionOffset = data.TopCollisionOffset;
            this.BottomCollisionOffset = data.BottomCollisionOffset;

            this.AnimatedSprites.First().Value.IsAnimating = true;
        }

        public IAIComponent IdleComponent
        {
            get
            {
                return _idleComponent;
            }
            set
            {
                this._idleComponent = value;
            }
        }

        public IAIComponent DecisionComponent
        {
            get
            {
                return _decisionComponent;
            }
            set
            {
                this._decisionComponent = value;
            }
        }

        public Action<GameTime, Enemy> ActiveComponent
        {
            get
            {
                return _currentAction;
            }

            set
            {
                this._currentAction = value;
            }
        }

        public Dictionary<String, IAIComponent> ActionComponents
        {
            get
            {
                return _actionComponents;
            }
        }

        public bool HasPlayerInSight
        {
            get
            {
                return (GameMath.Distance(Position, World.Player.Position) < _sightRadius);
            }
        }

        public float SightRadius
        {
            get { return this._sightRadius; }
            set { this._sightRadius = value; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this._health.CurrentValue <= 0)
                this._isAlive = false;

            if (!_isAction)
            {
                _isAction = true;

                if (!HasPlayerInSight || _actionComponents.Count == 0)
                {
                    _currentAction = _idleComponent.Update;
                }
                else
                {
                    _currentAction = _decisionComponent.Update;
                }
            }

            _currentAction(gameTime, this);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RectangleExtensions.DrawRectangleBorder(VisualBoundingRectangle, spriteBatch, 4, Color.White);
            CurrentSprite.Draw(gameTime, spriteBatch, PositionOffset, World.Camera);
        }

        public void ActionComplete()
        {
            _isAction = false;
            _currentAction = null;
        }
    }
}
