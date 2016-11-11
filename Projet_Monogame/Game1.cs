using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet_Monogame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject hero;
        GameObject ennemi;
        #region Normal Projectile
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        #endregion
        #region Sneak Projectile
        GameObject sneakprojectile;
        GameObject sneakprojectile2;
        #endregion
        #region Round Projectile
        GameObject roundprojectile;
        GameObject smallroundprojectileW;
        GameObject smallroundprojectileA;
        GameObject smallroundprojectileS;
        GameObject smallroundprojectileD;
        #endregion
        int time;
        byte enemytime = 0;
        int roundtime = -1;
        int direction;
        float vI;
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
            hero.sprite = Content.Load<Texture2D>("Héro.png");
            ennemi = new GameObject();
            ennemi.position.X = 750;
            ennemi.position.Y = 10;
            ennemi.sprite = Content.Load<Texture2D>("Ennemi.png");
            #region Projectile 1
            projectile = new GameObject();
            projectile.position.X = ennemi.position.X + 145;
            projectile.position.Y = ennemi.position.Y + 160;
            projectile.vitesse.X = 3;
            projectile.vitesse.Y = 3;
            projectile.sprite = Content.Load<Texture2D>("Projectile.png");
            #endregion
            #region Projectile 2
            projectile2 = new GameObject();
            projectile2.vitesse.X = 0;
            projectile2.vitesse.Y = 3;
            projectile2.sprite = Content.Load<Texture2D>("Projectile.png");
            #endregion
            #region Projectile 3
            projectile3 = new GameObject();
            projectile3.vitesse.X = -1;
            projectile3.vitesse.Y = 3;
            projectile3.sprite = Content.Load<Texture2D>("Projectile.png");
            #endregion
            #region Sneaky Projectile
            sneakprojectile = new GameObject();
            sneakprojectile.vitesse.X = 0;
            sneakprojectile.vitesse.Y = 2;
            sneakprojectile.sprite = Content.Load<Texture2D>("Sneaky_Projectile.png");
            #endregion
            #region Sneaky Projectile 2
            sneakprojectile2 = new GameObject();
            sneakprojectile2.vitesse.X = 0;
            sneakprojectile2.vitesse.Y = 2;
            sneakprojectile2.sprite = Content.Load<Texture2D>("Sneaky_Projectile.png");
            #endregion
            #region Round Projectile
            roundprojectile = new GameObject();
            roundprojectile.sprite = Content.Load<Texture2D>("Round_Projectile.png");

            #region Small Round Projectile UP
            smallroundprojectileW = new GameObject();
            smallroundprojectileW.vitesse.X = 0;
            smallroundprojectileW.vitesse.Y = -3;
            smallroundprojectileW.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile LEFT
            smallroundprojectileA = new GameObject();
            smallroundprojectileA.vitesse.X = -3;
            smallroundprojectileA.vitesse.Y = 0;
            smallroundprojectileA.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile DOWN
            smallroundprojectileS = new GameObject();
            smallroundprojectileS.vitesse.X = 0;
            smallroundprojectileS.vitesse.Y = 3;
            smallroundprojectileS.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile RIGHT
            smallroundprojectileD = new GameObject();
            smallroundprojectileD.vitesse.X = 3;
            smallroundprojectileD.vitesse.Y = 0;
            smallroundprojectileD.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion

            #endregion
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
            {
                hero.estVivant = true;
                time = 0;
                projectile.position.X = ennemi.position.X + 145;
                projectile.position.Y = ennemi.position.Y + 160;
            }
            if (hero.estVivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) && hero.position.X - 5 > 0)
                    hero.position.X -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.D) && hero.position.X + 5 < graphics.GraphicsDevice.DisplayMode.Width - 100)
                    hero.position.X += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.W) && hero.position.Y - 5 > 200)
                    hero.position.Y -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.S) && hero.position.Y + 5 < graphics.GraphicsDevice.DisplayMode.Height - 100)
                    hero.position.Y += 5;

                UpdateHero();
                UpdateEnnemi();
                UpdateProjectile();
            }
            time += 1;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void UpdateHero()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) && hero.position.X - 5 > 0)
                hero.position.X -= 3;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && hero.position.X + 5 < graphics.GraphicsDevice.DisplayMode.Width - 100)
                hero.position.X += 3;
            if (Keyboard.GetState().IsKeyDown(Keys.W) && hero.position.Y - 5 > 200)
                hero.position.Y -= 3;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && hero.position.Y + 5 < graphics.GraphicsDevice.DisplayMode.Height - 100)
                hero.position.Y += 3;


        }
        public void UpdateEnnemi()
        {
            if (enemytime == 0)
            {
                enemytime = 121;
                direction = random.Next(1, 101);
                if (direction <= 50)
                    ennemi.vitesse.X = 4;
                else
                    ennemi.vitesse.X = -4;
            }
            if (ennemi.position.X <= graphics.GraphicsDevice.DisplayMode.Width - 150 && ennemi.position.X >= -150)
                ennemi.position.X += ennemi.vitesse.X;
            else if (ennemi.position.X > graphics.GraphicsDevice.DisplayMode.Width - 150)
                ennemi.position.X = -150;
            else
                ennemi.position.X = graphics.GraphicsDevice.DisplayMode.Width - 150;
            enemytime -= 1;
        }
        public void UpdateProjectile()
        {
            #region Projectile 1
            if (projectile.position.Y < graphics.GraphicsDevice.DisplayMode.Height)
            {
                projectile.position.X += projectile.vitesse.X;
                projectile.position.Y += projectile.vitesse.Y;
            }
            else if (projectile.position.Y >= graphics.GraphicsDevice.DisplayMode.Height || time == 0)
            {
                projectile.position.X = ennemi.position.X + 150;
                projectile.position.Y = ennemi.position.Y + 160;
                projectile.vitesse.X += 1;
                if (projectile.vitesse.X == 10)
                {
                    projectile.vitesse.Y = 9;
                    projectile.vitesse.X = -20;
                }
            }
            if (projectile.position.X > graphics.GraphicsDevice.DisplayMode.Width)
            {
                projectile.position.X = -50;
                projectile.position.Y += projectile.vitesse.Y;
            }
            if (projectile.position.X < -50)
            {
                projectile.position.X = graphics.GraphicsDevice.DisplayMode.Width;
                projectile.position.Y += projectile.vitesse.Y;
            }
            if (hero.GetRect().Intersects(projectile.GetRect()))
            {
                hero.estVivant = false;
                time = 0;
            }
            #endregion
            #region Projectile 2
            if (time == 180)
            {
                projectile2.position.X = ennemi.position.X + 145;
                projectile2.position.Y = ennemi.position.Y + 160;
            }
            if (time > 180)
            {
                if (projectile2.position.Y < graphics.GraphicsDevice.DisplayMode.Height)
                {
                    projectile2.position.X += projectile2.vitesse.X;
                    projectile2.position.Y += projectile2.vitesse.Y;
                }
                else
                {
                    projectile2.position.X = ennemi.position.X + 150;
                    projectile2.position.Y = ennemi.position.Y + 160;
                    projectile2.vitesse.Y += 1;
                    if (projectile2.vitesse.Y == 10)
                    {
                        projectile2.vitesse.X += 5;
                        projectile2.vitesse.Y = 1;
                    }

                }
                if (projectile2.position.X > graphics.GraphicsDevice.DisplayMode.Width)
                {
                    projectile2.position.X = -50;
                    projectile2.position.Y += projectile2.vitesse.Y;

                }
                if (projectile2.position.X < -50)
                {
                    projectile2.position.X = graphics.GraphicsDevice.DisplayMode.Width;
                    projectile2.position.Y += projectile2.vitesse.X;
                }
                if (hero.GetRect().Intersects(projectile2.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Projectile 3
            if (time == 580 || projectile3.position.Y >= graphics.GraphicsDevice.DisplayMode.Height)
            {
                projectile3.position.X = ennemi.position.X + 145;
                projectile3.position.Y = ennemi.position.Y + 160;
                if ((hero.position.X - (ennemi.position.X + 150)) / (hero.position.Y - (ennemi.position.Y + 150)) > 0)
                    projectile3.vitesse.X = (float)Math.Sqrt(36 * (hero.position.X - (ennemi.position.X + 150)) / (hero.position.Y - (ennemi.position.Y + 150)));
                else
                    projectile3.vitesse.X = (float)-Math.Sqrt(-36 * (hero.position.X - (ennemi.position.X + 150)) / (hero.position.Y - (ennemi.position.Y + 150)));
                if ((hero.position.X - (ennemi.position.X + 150)) / (hero.position.Y - (ennemi.position.Y + 150)) > 0)

                    projectile3.vitesse.Y = (float)Math.Sqrt(36 - Math.Pow(projectile3.vitesse.Y, 2));
                else
                    projectile3.vitesse.Y = (float)Math.Sqrt(36 - Math.Pow(projectile3.vitesse.Y, 2));
            }
            if (time > 580)
            {
                projectile3.position.X += projectile3.vitesse.X;
                projectile3.position.Y += projectile3.vitesse.Y;

                if (projectile3.position.X > graphics.GraphicsDevice.DisplayMode.Width)
                {
                    projectile3.position.X = -50;
                    projectile3.position.Y += projectile3.vitesse.Y;

                }
                if (projectile3.position.X < -50)
                {
                    projectile3.position.X = graphics.GraphicsDevice.DisplayMode.Width;
                    projectile3.position.Y += projectile3.vitesse.X;
                }
                if (hero.GetRect().Intersects(projectile3.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Sneaky Projectile 1
            if (time == 450)
            {
                sneakprojectile.position.X = ennemi.position.X + 145;
                sneakprojectile.position.Y = ennemi.position.Y + 200;
            }
            if (time > 450)
            {
                if (sneakprojectile.position.Y >= graphics.GraphicsDevice.DisplayMode.Height || sneakprojectile.position.Y <= 200 || sneakprojectile.position.X >= graphics.GraphicsDevice.DisplayMode.Width || sneakprojectile.position.X <= 0)
                {
                    sneakprojectile.position.X = ennemi.position.X + 145;
                    sneakprojectile.position.Y = ennemi.position.Y + 200;
                    sneakprojectile.vitesse.X = 0;
                    sneakprojectile.vitesse.Y = 0;
                }

                sneakprojectile.position.X += (sneakprojectile.vitesse.X / 40);
                sneakprojectile.position.Y += (sneakprojectile.vitesse.Y / 40);

                #region Momentum
                if (sneakprojectile.position.Y < hero.position.Y + 50)
                    sneakprojectile.vitesse.Y += 1;
                if (sneakprojectile.position.Y > hero.position.Y + 50)
                    sneakprojectile.vitesse.Y -= 1;
                if (sneakprojectile.position.X < hero.position.X + 50)
                    sneakprojectile.vitesse.X += 1;
                if (sneakprojectile.position.X > hero.position.X + 50)
                    sneakprojectile.vitesse.X -= 1;
                #endregion

                if (hero.GetRect().Intersects(sneakprojectile.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Sneaky Projectile 2
            if (time == 600)
            {
                sneakprojectile2.position.X = ennemi.position.X + 145;
                sneakprojectile2.position.Y = ennemi.position.Y + 200;
            }
            if (time > 600)
            {
                if (sneakprojectile2.position.Y >= graphics.GraphicsDevice.DisplayMode.Height || sneakprojectile2.position.Y <= 200 || sneakprojectile2.position.X >= graphics.GraphicsDevice.DisplayMode.Width || sneakprojectile2.position.X <= 0)
                {
                    sneakprojectile2.position.X = ennemi.position.X + 145;
                    sneakprojectile2.position.Y = ennemi.position.Y + 200;
                    sneakprojectile2.vitesse.X = 0;
                    sneakprojectile2.vitesse.Y = 0;
                }

                sneakprojectile2.position.X += (sneakprojectile2.vitesse.X / 40);
                sneakprojectile2.position.Y += (sneakprojectile2.vitesse.Y / 40);

                #region Momentum
                if (sneakprojectile2.position.Y < hero.position.Y + 50)
                    sneakprojectile2.vitesse.Y += 1;
                if (sneakprojectile2.position.Y > hero.position.Y + 50)
                    sneakprojectile2.vitesse.Y -= 1;
                if (sneakprojectile2.position.X < hero.position.X + 50)
                    sneakprojectile2.vitesse.X += 1;
                if (sneakprojectile2.position.X > hero.position.X + 50)
                    sneakprojectile2.vitesse.X -= 1;
                #endregion

                if (hero.GetRect().Intersects(sneakprojectile2.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Round Projectile
            if (time == 1000 || roundtime == 0)
            {
                roundprojectile.position.X = ennemi.position.X + 145;
                roundprojectile.position.Y = ennemi.position.Y + 160;
                roundprojectile.vitesse.X = (hero.position.X - (ennemi.position.X + 145)) / 60;
                roundprojectile.vitesse.Y = (hero.position.Y - (ennemi.position.Y + 145)) / 60;
                roundtime = 0;
            }
            if (time > 1000)
            {
                if (roundtime < 60)
                {
                    roundprojectile.position.X += roundprojectile.vitesse.X;
                    roundprojectile.position.Y += roundprojectile.vitesse.Y;
                }
                else if (roundtime < 120)
                {
                    roundprojectile.vitesse.X = 0;
                    roundprojectile.vitesse.Y = 0;
                }
                else if (roundtime == 120)
                {
                    smallroundprojectileA.position.X = roundprojectile.position.X;
                    smallroundprojectileA.position.Y = roundprojectile.position.Y;
                    smallroundprojectileW.position.X = roundprojectile.position.X;
                    smallroundprojectileW.position.Y = roundprojectile.position.Y;
                    smallroundprojectileD.position.X = roundprojectile.position.X;
                    smallroundprojectileD.position.Y = roundprojectile.position.Y;
                    smallroundprojectileS.position.X = roundprojectile.position.X;
                    smallroundprojectileS.position.Y = roundprojectile.position.Y;
                    roundprojectile.position.X = -50;
                    roundprojectile.position.Y = -50;
                }
                else
                {
                    smallroundprojectileA.position.X += smallroundprojectileA.vitesse.X;
                    smallroundprojectileA.position.Y += smallroundprojectileA.vitesse.Y;
                    smallroundprojectileW.position.X += smallroundprojectileW.vitesse.X;
                    smallroundprojectileW.position.Y += smallroundprojectileW.vitesse.Y;
                    smallroundprojectileS.position.X += smallroundprojectileS.vitesse.X;
                    smallroundprojectileS.position.Y += smallroundprojectileS.vitesse.Y;
                    smallroundprojectileD.position.X += smallroundprojectileD.vitesse.X;
                    smallroundprojectileD.position.Y += smallroundprojectileD.vitesse.Y;
                }
                if (roundtime == 480)
                    roundtime = -1;
                roundtime += 1;
                if (hero.GetRect().Intersects(roundprojectile.GetRect()) || hero.GetRect().Intersects(smallroundprojectileW.GetRect()) || hero.GetRect().Intersects(smallroundprojectileA.GetRect()) || hero.GetRect().Intersects(smallroundprojectileS.GetRect()) || hero.GetRect().Intersects(smallroundprojectileD.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
        }
        #endregion

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            if (hero.estVivant == true)
            {
                GraphicsDevice.Clear(Color.SkyBlue);
                spriteBatch.Begin();

                spriteBatch.Draw(hero.sprite, hero.position, Color.White);
                spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);
                spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
                if (time >= 180)
                    spriteBatch.Draw(projectile2.sprite, projectile2.position, Color.White);
                if (time >= 450)
                    spriteBatch.Draw(sneakprojectile.sprite, sneakprojectile.position, Color.White);
                if (time >= 580)
                    spriteBatch.Draw(projectile3.sprite, projectile3.position, Color.White);
                if (time >= 600)
                    spriteBatch.Draw(sneakprojectile2.sprite, sneakprojectile2.position, Color.White);
                if (time >= 1000)
                {
                    if (roundtime < 120)
                        spriteBatch.Draw(roundprojectile.sprite, roundprojectile.position, Color.White);
                    else
                    {
                        spriteBatch.Draw(smallroundprojectileW.sprite, smallroundprojectileW.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileA.sprite, smallroundprojectileA.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileS.sprite, smallroundprojectileS.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileD.sprite, smallroundprojectileD.position, Color.White);
                    }
                }

                spriteBatch.End();
            }
            else
                GraphicsDevice.Clear(Color.Red);


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
