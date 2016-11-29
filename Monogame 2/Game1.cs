using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject hero;
        GameObject[] ennemi;
        GameObject[] projectile;
        GameObject detection;
        GameObject background;
        GameObject background2;
        int nbreEnnemi = 5;
        int time;
        int luck;
        int kill = 0;
        Random random = new Random();
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
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            //fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hero = new GameObject();
            hero.estVivant = true;
            hero.position.X = 898;
            hero.position.Y = 900;
            hero.sprite = Content.Load<Texture2D>("Mine_3.png");
            detection = new GameObject();
            detection.position.X = hero.position.X - 113;
            detection.position.Y = hero.position.Y - 109;
            detection.sprite = Content.Load<Texture2D>("Detection_Range.png");
            background = new GameObject();
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("Background.png");
            background2 = new GameObject();
            background2.position.X = background.position.X + 1920;
            background2.position.Y = 0;
            background2.sprite = Content.Load<Texture2D>("Background.png");
            ennemi = new GameObject[30];
            for (int t = 0; t < 30; t++)
            {
                ennemi[t] = new GameObject();
                ennemi[t].estVivant = false;
                ennemi[t].vitesse.X = random.Next(-1, 2);
                ennemi[t].vitesse.Y = random.Next(-1, 2);
                ennemi[t].Time = 0;
                luck = random.Next(1, 4);
                if (luck == 1)
                    ennemi[t].sprite = Content.Load<Texture2D>("Mine_1.png");
                else if (luck == 2)
                    ennemi[t].sprite = Content.Load<Texture2D>("Mine_2.png");
                else if (luck == 3)
                    ennemi[t].sprite = Content.Load<Texture2D>("Mine_3.png");
            }
            projectile = new GameObject[5];
            for (int t = 0; t < 5; t++)
            {
                projectile[t] = new GameObject();
                projectile[t].sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
                projectile[t].estVivant = false;
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (hero.estVivant == false && time == 120)
                Exit();
            if (hero.estVivant == true)
            {
                UpdateHero();
                UpdateEnnemi();
                UpdateProjectile();
                UpdateBackground();
            }

            time += 1;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void UpdateHero()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) && hero.position.X - 5 > 0)
            {
                hero.position.X -= 3;
                detection.position.X -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && hero.position.X + 5 < graphics.GraphicsDevice.DisplayMode.Width - 74)
            {
                hero.position.X += 3;
                detection.position.X += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && hero.position.Y - 5 > 0)
            {
                hero.position.Y -= 3;
                detection.position.Y -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && hero.position.Y + 5 < graphics.GraphicsDevice.DisplayMode.Height - 85)
            {
                hero.position.Y += 3;
                detection.position.Y += 3;
            }
        }
        public void UpdateEnnemi()
        {
            if (kill < 25)
                nbreEnnemi = kill + 5;
            for (int t = 0; t < nbreEnnemi; t++)
            {
                if (detection.GetRect().Intersects(ennemi[t].GetRect()))
                    ennemi[t].visible = true;
                else
                    ennemi[t].visible = false;

                if (hero.GetRect().Intersects(ennemi[t].GetRect()) && ennemi[t].estVivant == true)
                {
                    hero.estVivant = false;
                    time = 0;
                }
                if (ennemi[t].estVivant == false && ennemi[t].Time == 0)
                {
                    ennemi[t].estVivant = true;
                    ennemi[t].visible = false;
                    ennemi[t].position.Y = random.Next(0, graphics.GraphicsDevice.DisplayMode.Height - 82);
                    if (ennemi[t].position.Y < detection.position.Y && ennemi[t].position.Y > detection.position.Y + 300)
                        ennemi[t].position.X = random.Next(0, graphics.GraphicsDevice.DisplayMode.Width - 74);
                    else
                    {
                        ennemi[t].position.X = random.Next(0, graphics.GraphicsDevice.DisplayMode.Width - 374);
                        if (ennemi[t].position.X > detection.position.X)
                            ennemi[t].position.X += 374;
                    }
                }
                ennemi[t].position.X += ennemi[t].vitesse.X;
                if (ennemi[t].position.X < -74)
                    ennemi[t].position.X = graphics.GraphicsDevice.DisplayMode.Width;
                else if (ennemi[t].position.X > graphics.GraphicsDevice.DisplayMode.Width)
                    ennemi[t].position.X = -74;
                ennemi[t].position.Y += ennemi[t].vitesse.Y;
                if (ennemi[t].position.Y < -82)
                    ennemi[t].position.Y = graphics.GraphicsDevice.DisplayMode.Height;
                else if (ennemi[t].position.Y > graphics.GraphicsDevice.DisplayMode.Height)
                    ennemi[t].position.Y = -82;

                for (int i = 0; i < 5; i++)
                {
                    if (projectile[i].GetRect().Intersects(ennemi[t].GetRect()) && projectile[i].estVivant == true && ennemi[t].visible == true)
                    {
                        kill++;
                        projectile[i].estVivant = false;
                        ennemi[t].estVivant = false;
                        ennemi[t].Time = 60;
                    }
                }
                if (ennemi[t].Time > 0)
                    ennemi[t].Time -= 1;
            }
        }
        public void UpdateProjectile()
        {
            if (hero.Time == 0)
            {
                byte t = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    while (projectile[t].estVivant == true)
                        t++;

                    projectile[t].estVivant = true;
                    projectile[t].position.X = hero.position.X;
                    projectile[t].position.Y = hero.position.Y + 24;
                    projectile[t].vitesse.Y = 0;
                    projectile[t].vitesse.X = -5;
                    projectile[t].vitesse.Y = 0;
                    projectile[t].Time = 300;
                    hero.Time = 60; 
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    while (projectile[t].estVivant == true)
                        t++;

                    projectile[t].estVivant = true;
                    projectile[t].position.X = hero.position.X + 40;
                    projectile[t].position.Y = hero.position.Y + 24;
                    projectile[t].vitesse.X = 5;
                    projectile[t].vitesse.Y = 0;
                    projectile[t].Time = 300;
                    hero.Time = 60;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    while (projectile[t].estVivant == true)
                        t++;

                    projectile[t].estVivant = true;
                    projectile[t].position.Y = hero.position.Y;
                    projectile[t].position.X = hero.position.X + 20;
                    projectile[t].vitesse.Y = -5;
                    projectile[t].vitesse.X = 0;
                    projectile[t].Time = 300;
                    hero.Time = 60;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    while (projectile[t].estVivant == true)
                        t++;

                    projectile[t].estVivant = true;
                    projectile[t].position.Y = hero.position.Y + 48;
                    projectile[t].position.X = hero.position.X + 20;
                    projectile[t].vitesse.Y = 5;
                    projectile[t].vitesse.X = 0;
                    projectile[t].Time = 300;
                    hero.Time = 60;
                }
            }
            for (byte t = 0; t < 5; t++)
            {
                if (projectile[t].estVivant)
                {
                    projectile[t].position.X += projectile[t].vitesse.X;
                    projectile[t].position.Y += projectile[t].vitesse.Y;
                    if (projectile[t].position.X < 0 || projectile[t].position.X > graphics.GraphicsDevice.DisplayMode.Width || projectile[t].position.Y < 0 || projectile[t].position.Y > graphics.GraphicsDevice.DisplayMode.Height)
                        projectile[t].estVivant = false;
                }
            }
            if (hero.Time > 0)
                hero.Time--;
        }
        public void UpdateBackground()
        {
            background.position.X -= 3;
            background2.position.X -= 3;

            if (background.position.X < -1919)
                background.position.X = background2.position.X + 1920;
            if (background2.position.X < -1919)
                background2.position.X = background.position.X + 1920;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (hero.estVivant == true)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();

                spriteBatch.Draw(background.sprite, background.position);
                spriteBatch.Draw(background2.sprite, background2.position, effects: SpriteEffects.FlipHorizontally);

                spriteBatch.Draw(hero.sprite, hero.position, Color.Cyan);

                for (int t = 0; t < nbreEnnemi; t++)
                    if (ennemi[t].visible == true && ennemi[t].estVivant == true)
                        spriteBatch.Draw(ennemi[t].sprite, ennemi[t].position, Color.Red);

                for (int t = 0; t < 5; t++)
                    if (projectile[t].estVivant == true)
                        spriteBatch.Draw(projectile[t].sprite, projectile[t].position, Color.Cyan);

                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Red);
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
