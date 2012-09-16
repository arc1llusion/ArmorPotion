﻿using System;
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

        private bool isAction;
        private float sightRadius;

        private delegate void Action(GameTime gameTime, Enemy enemy);

        private Action<GameTime, Enemy> _currentAction;

        public Enemy(World world)
            : base(world)
        {
            isAction = false;
            sightRadius = 100;
            _actionComponents = new Dictionary<String, IAIComponent>();
        }

        public Enemy(World world, EnemyData data)
            : base(world)
        {
            isAction = false;
            sightRadius = 100;

            this.Texture = data.Texture;
            this.Velocity = data.Velocity;

            this.AnimatedSprites = data.DeepCopySprites;
            this._idleComponent = data.IdleComponent;
            this._decisionComponent = data.DecisionComponent;
            this._actionComponents = data.ActionComponents;

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
                return (GameMath.Distance(Position, World.Player.Position) < sightRadius);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!isAction)
            {
                isAction = true;

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
            CurrentSprite.Draw(gameTime, spriteBatch, Position, World.Camera);
        }

        public void ActionComplete()
        {
            isAction = false;
            _currentAction = null;
        }
    }
}
