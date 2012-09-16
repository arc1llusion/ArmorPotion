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

namespace ArmorPotions
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : ArmorPotionFramework.Game.ArmorPotionsGame
    {
        World world;
        GraphicsDeviceManager graphics;
        LinearProjectile lProj;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            EnemyFactory factory = new EnemyFactory(world, @"Images\Enemy");
            ItemFactory itemFactory = new ItemFactory(world, @"Items");

            //world.Enemies.Add(factory.Create("MaleFighter"));
            world.Enemies.Add(factory.Create("LightBug"));
            //world.Enemies.Add(factory.Create("MaleFighter"));

            Item item = itemFactory.Create("Super Awesome Potion");
            item.Position = new Vector2(70, 70);

            world.item = item;
            //lProj = new LinearProjectile(world, 5, world.Player.Position, new Vector2(300, 300));

            Animation animation = new Animation(1, 256, 256, 0, 0);
            AnimatedSprite sprite = new AnimatedSprite(Content.Load<Texture2D>(@"Images\Enemy\LightBugAttack"), new Dictionary<AnimationKey, Animation> { { AnimationKey.Down, animation } });

            world.Player.Inventory.TempaQuips.Add(new Gun(null, "BobsGun", sprite));
            world.Player.Inventory.SelectRelativeTempaQuip(world.Player, 0);

            world.Player.Inventory.TempaQuips.Add(new Zapper(null, "BobsZappter", sprite.Clone()));

            //lProj.AnimatedSprites.Add("Normal", sprite);
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
            //lProj.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            world.Draw(gameTime, SpriteBatch);
            //lProj.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
