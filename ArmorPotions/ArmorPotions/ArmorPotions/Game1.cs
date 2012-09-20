using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ArmorPotionFramework.Game;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.SpriteClasses;
using System.IO;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Input;
using ArmorPotions.Items;
using ArmorPotions.Factories;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Projectiles;
using ArmorPotions.Items.TempaQuipItems;
using ArmorPotionFramework.TileEngine;
using ArmorPotion.MapStuff;

namespace ArmorPotions
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : ArmorPotionFramework.Game.ArmorPotionsGame
    {
        World world;
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            ScreenRectangle = new Rectangle(0, 0, 1024, 768);

            //graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.Components.Add(new InputHandler(this));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);            
            world = new World(this);

            ItemFactory itemFactory = new ItemFactory(world, @"Items");
            Map dungeonOne = MapLoader.Load("Content/Maps/DungeonOne_top.txt", "Content/Maps/DungeonOne_bottom.txt", world);
            world.CurrentDungeon = dungeonOne;
            EnemyFactory factory = new EnemyFactory(world, @"Enemy");
            world.EnemyFactory = factory;

            Enemy enemy = world.EnemyFactory.Create("LightBug");
            enemy.Position = new Vector2(300, 300);
            world.Enemies.Add(enemy);

            Item item = itemFactory.Create("Super Awesome Potion");
            item.Position = new Vector2(70, 70);
            world.item = item;

            Animation animation = new Animation(1, 32, 32, 0, 0);
            Animation animation2 = new Animation(1, 256, 256, 0, 0);

            AnimatedSprite sprite = new AnimatedSprite(Content.Load<Texture2D>(@"Items\Weapons\Fireball"), new Dictionary<AnimationKey, Animation> { { AnimationKey.Right, animation } });
            AnimatedSprite light = new AnimatedSprite(Content.Load<Texture2D>(@"Enemy\LightBugAttack"), new Dictionary<AnimationKey, Animation> { { AnimationKey.Right, animation2 } });

            world.Player.Inventory.TempaQuips.Add(new Sword(Content.Load<Texture2D>(@"Gui\SwordIcon"), "Sword"));

            world.Player.Inventory.TempaQuips.Add((Gun)itemFactory.Create("Bobs Gun"));
            world.Player.Inventory.SelectRelativeTempaQuip(world.Player, 0);

            world.Player.Inventory.TempaQuips.Add(new Zapper(Content.Load<Texture2D>(@"Gui\GogglesIcon"), "BobsZapper", light));

            world.Player.Inventory.TempaQuips.Add(new SomeConeWeapon(Content.Load<Texture2D>(@"Gui\GravityBootsIcon"), "BobsCone", sprite.Clone()));

            world.Player.Inventory.TempaQuips.Add((EBall)itemFactory.Create("E-Ball Fire"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (InputHandler.KeyDown(Keys.Escape))
                this.Exit();

            world.Update(gameTime);

            base.Update(gameTime);

            if (world.Player.Health.CurrentValue == 0)
                this.LoadContent();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            world.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
