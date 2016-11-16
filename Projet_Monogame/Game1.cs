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
        GameObject background;
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
        #region Round Projectile 1
        GameObject roundprojectile;
        GameObject smallroundprojectileW;
        GameObject smallroundprojectileA;
        GameObject smallroundprojectileS;
        GameObject smallroundprojectileD;
        #endregion
        #region Round Projectile 2
        GameObject roundprojectile2;
        GameObject smallroundprojectileW2;
        GameObject smallroundprojectileA2;
        GameObject smallroundprojectileS2;
        GameObject smallroundprojectileD2;
        #endregion
        #endregion
        int time;
        byte enemytime = 0;
        int roundtime = -1;
        int roundtime2 = -1;
        int direction;
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
            background = new GameObject();
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("Background.png");
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
            projectile2.position.X = 0;
            projectile2.position.Y = 0;
            projectile2.vitesse.X = 0;
            projectile2.vitesse.Y = 3;
            projectile2.sprite = Content.Load<Texture2D>("Projectile.png");
            #endregion
            #region Projectile 3
            projectile3 = new GameObject();
            projectile3.position.X = 0;
            projectile3.position.Y = 0;
            projectile3.vitesse.X = random.Next(-3,3);
            projectile3.vitesse.Y = random.Next(2, 5);
            projectile3.sprite = Content.Load<Texture2D>("Projectile.png");
            #endregion
            #region Sneaky Projectile
            sneakprojectile = new GameObject();
            sneakprojectile.position.X = 0;
            sneakprojectile.position.Y = 0;
            sneakprojectile.vitesse.X = 0;
            sneakprojectile.vitesse.Y = 2;
            sneakprojectile.sprite = Content.Load<Texture2D>("Sneaky_Projectile.png");
            #endregion
            #region Sneaky Projectile 2
            sneakprojectile2 = new GameObject();
            sneakprojectile2.position.X = 0;
            sneakprojectile2.position.Y = 0;
            sneakprojectile2.vitesse.X = 0;
            sneakprojectile2.vitesse.Y = 2;
            sneakprojectile2.sprite = Content.Load<Texture2D>("Sneaky_Projectile.png");
            #endregion
            #region Round Projectile
            roundprojectile = new GameObject();
            roundprojectile.position.X = 0;
            roundprojectile.position.Y = 0;
            roundprojectile.sprite = Content.Load<Texture2D>("Round_Projectile.png");

            #region Small Round Projectile UP
            smallroundprojectileW = new GameObject();
            smallroundprojectileW.vitesse.X = 0;
            smallroundprojectileW.vitesse.Y = -5;
            smallroundprojectileW.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile LEFT
            smallroundprojectileA = new GameObject();
            smallroundprojectileA.vitesse.X = -5;
            smallroundprojectileA.vitesse.Y = 0;
            smallroundprojectileA.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile DOWN
            smallroundprojectileS = new GameObject();
            smallroundprojectileS.vitesse.X = 0;
            smallroundprojectileS.vitesse.Y = 5;
            smallroundprojectileS.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile RIGHT
            smallroundprojectileD = new GameObject();
            smallroundprojectileD.vitesse.X = 5;
            smallroundprojectileD.vitesse.Y = 0;
            smallroundprojectileD.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion

            #endregion
            #region Round Projectile 2
            roundprojectile2 = new GameObject();
            roundprojectile2.position.X = 0;
            roundprojectile2.position.Y = 0;
            roundprojectile2.sprite = Content.Load<Texture2D>("Round_Projectile.png");

            #region Small Round Projectile UP 2
            smallroundprojectileW2 = new GameObject();
            smallroundprojectileW2.vitesse.X = -5;
            smallroundprojectileW2.vitesse.Y = -5;
            smallroundprojectileW2.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile LEFT 2
            smallroundprojectileA2 = new GameObject();
            smallroundprojectileA2.vitesse.X = -5;
            smallroundprojectileA2.vitesse.Y = 5;
            smallroundprojectileA2.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile DOWN 2
            smallroundprojectileS2 = new GameObject();
            smallroundprojectileS2.vitesse.X = 5;
            smallroundprojectileS2.vitesse.Y = 5;
            smallroundprojectileS2.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
            #endregion
            #region Small Round Projectile RIGHT 2
            smallroundprojectileD2 = new GameObject();
            smallroundprojectileD2.vitesse.X = 5;
            smallroundprojectileD2.vitesse.Y = -5;
            smallroundprojectileD2.sprite = Content.Load<Texture2D>("Small_Round_Projectile.png");
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
                Exit();
            if (time > 6120)
                Exit();
            if (hero.estVivant == true && time < 6000)
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
            if (projectile.position.Y >= graphics.GraphicsDevice.DisplayMode.Height)
            {
                projectile.position.X = ennemi.position.X + 145;
                projectile.position.Y = ennemi.position.Y + 200;
                projectile.vitesse.X = random.Next(-7, 8);
                projectile.vitesse.Y = random.Next(3, 6);
                if (time > 4500)
                {
                    projectile.vitesse.X *= 2;
                    projectile.vitesse.Y *= 2;
                }
            }
            if (projectile.position.X >= graphics.GraphicsDevice.DisplayMode.Width || projectile.position.X <= 0)
                projectile.vitesse.X = -projectile.vitesse.X;

            projectile.position.X += projectile.vitesse.X;
            projectile.position.Y += projectile.vitesse.Y;
            if (hero.GetRect().Intersects(projectile.GetRect()))
            {
                hero.estVivant = false;
                time = 0;
            }
            #endregion
            #region Projectile 2
            if (time >= 180)
            {
                if (time == 180)
                {
                    projectile2.position.X = ennemi.position.X + 145;
                    projectile2.position.Y = ennemi.position.Y + 200;
                }
                if (projectile2.position.Y >= graphics.GraphicsDevice.DisplayMode.Height)
                {
                    projectile2.position.X = ennemi.position.X + 145;
                    projectile2.position.Y = ennemi.position.Y + 200;
                    projectile2.vitesse.X = random.Next(-7, 8);
                    projectile2.vitesse.Y = random.Next(3, 6);
                    if (time > 4500)
                    {
                        projectile2.vitesse.X *= 2;
                        projectile2.vitesse.Y *= 2;
                    }
                }
                if (projectile2.position.X >= graphics.GraphicsDevice.DisplayMode.Width || projectile2.position.X <= 0)
                    projectile2.vitesse.X = -projectile2.vitesse.X;

                projectile2.position.X += projectile2.vitesse.X;
                projectile2.position.Y += projectile2.vitesse.Y;
                if (hero.GetRect().Intersects(projectile2.GetRect()))
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Projectile 3
            if (time >= 500)
            {
                if (time == 500)
                {
                    projectile3.position.X = ennemi.position.X + 145;
                    projectile3.position.Y = ennemi.position.Y + 200;
                }
                if (projectile3.position.Y >= graphics.GraphicsDevice.DisplayMode.Height)
                {
                    projectile3.position.X = ennemi.position.X + 145;
                    projectile3.position.Y = ennemi.position.Y + 200;
                    projectile3.vitesse.X = random.Next(-7, 8);
                    projectile3.vitesse.Y = random.Next(3, 6);
                    if (time > 4500)
                    {
                        projectile3.vitesse.X *= 2;
                        projectile3.vitesse.Y *= 2;
                    }
                }
                if (projectile3.position.X >= graphics.GraphicsDevice.DisplayMode.Width || projectile3.position.X <= 0)
                    projectile3.vitesse.X = -projectile3.vitesse.X;

                projectile3.position.X += projectile3.vitesse.X;
                projectile3.position.Y += projectile3.vitesse.Y;
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

                if (sneakprojectile.GetRect().Intersects(sneakprojectile2.GetRect()))
                    sneakprojectile.vitesse.X += 100;

                sneakprojectile.position.X += (sneakprojectile.vitesse.X / 40);
                sneakprojectile.position.Y += (sneakprojectile.vitesse.Y / 40);

                #region Momentum
                if (time < 4500)
                {
                    if (sneakprojectile.position.Y < hero.position.Y + 50)
                        sneakprojectile.vitesse.Y += 1;
                    if (sneakprojectile.position.Y > hero.position.Y + 50)
                        sneakprojectile.vitesse.Y -= 1;
                    if (sneakprojectile.position.X < hero.position.X + 50)
                        sneakprojectile.vitesse.X += 1;
                    if (sneakprojectile.position.X > hero.position.X + 50)
                        sneakprojectile.vitesse.X -= 1;
                }
                else
                {
                    if (sneakprojectile.position.Y < hero.position.Y + 50)
                    {
                        if (sneakprojectile.vitesse.Y > 0)
                            sneakprojectile.vitesse.Y += 2;
                        else
                            sneakprojectile.vitesse.Y += 5;
                    }
                    if (sneakprojectile.position.Y > hero.position.Y + 50)
                    {
                        if (sneakprojectile.vitesse.Y < 0)
                            sneakprojectile.vitesse.Y -= 2;
                        else
                            sneakprojectile.vitesse.Y -= 5;
                    }
                    if (sneakprojectile.position.X < hero.position.X + 50)
                    {
                        if (sneakprojectile.vitesse.X < 0)
                            sneakprojectile.vitesse.X += 5;
                        else
                            sneakprojectile.vitesse.X += 2;
                    }
                    if (sneakprojectile.position.X > hero.position.X + 50)
                    {
                        if (sneakprojectile.vitesse.X > 0)
                            sneakprojectile.vitesse.X -= 5;
                        else
                            sneakprojectile.vitesse.X -= 2;
                    }
                }
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
                if (time < 4500)
                {
                    if (sneakprojectile2.position.Y < hero.position.Y + 50)
                        sneakprojectile2.vitesse.Y += 1;
                    if (sneakprojectile2.position.Y > hero.position.Y + 50)
                        sneakprojectile2.vitesse.Y -= 1;
                    if (sneakprojectile2.position.X < hero.position.X + 50)
                        sneakprojectile2.vitesse.X += 1;
                    if (sneakprojectile2.position.X > hero.position.X + 50)
                        sneakprojectile2.vitesse.X -= 1;
                }
                else
                {
                    if (sneakprojectile2.position.Y < hero.position.Y + 50)
                    {
                        if (sneakprojectile2.vitesse.Y > 0)
                            sneakprojectile2.vitesse.Y += 2;
                        else
                            sneakprojectile2.vitesse.Y += 5;
                    }
                    if (sneakprojectile2.position.Y > hero.position.Y + 50)
                    {
                        if (sneakprojectile2.vitesse.Y < 0)
                            sneakprojectile2.vitesse.Y -= 2;
                        else
                            sneakprojectile2.vitesse.Y -= 5;
                    }
                    if (sneakprojectile2.position.X < hero.position.X + 50)
                    {
                        if (sneakprojectile2.vitesse.X < 0)
                            sneakprojectile2.vitesse.X += 5;
                        else
                            sneakprojectile2.vitesse.X += 2;
                    }
                    if (sneakprojectile2.position.X > hero.position.X + 50)
                    {
                        if (sneakprojectile2.vitesse.X > 0)
                            sneakprojectile2.vitesse.X -= 5;
                        else
                            sneakprojectile2.vitesse.X -= 2;
                    }
                }
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
                roundprojectile.vitesse.X = (hero.position.X - (ennemi.position.X + 145)) / 45;
                roundprojectile.vitesse.Y = (hero.position.Y - (ennemi.position.Y + 145)) / 45;
                roundtime = 0;
            }
            if (time > 1000)
            {
                if (roundtime < 45)
                    roundprojectile.position.Y += roundprojectile.vitesse.Y;
                else if (roundtime < 90)
                    roundprojectile.position.X += roundprojectile.vitesse.X;
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
                if (roundtime == 360)
                    roundtime = -1;
                roundtime += 1;
                if (hero.GetRect().Intersects(roundprojectile.GetRect()) && roundtime < 120)
                {
                    hero.estVivant = false;
                    time = 0;
                }
                else if ((hero.GetRect().Intersects(smallroundprojectileW.GetRect()) || hero.GetRect().Intersects(smallroundprojectileA.GetRect()) || hero.GetRect().Intersects(smallroundprojectileS.GetRect()) || hero.GetRect().Intersects(smallroundprojectileD.GetRect())) && roundtime > 120)
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
            #region Round Projectile 2
            if (time == 1500 || roundtime2 == 0)
            {
                roundprojectile2.position.X = ennemi.position.X + 145;
                roundprojectile2.position.Y = ennemi.position.Y + 160;
                roundprojectile2.vitesse.X = (hero.position.X - (ennemi.position.X + 145)) / 45;
                roundprojectile2.vitesse.Y = (hero.position.Y - (ennemi.position.Y + 145)) / 45;
                roundtime = 0;
            }
            if (time > 1500)
            {
                if (roundtime2 < 45)
                    roundprojectile2.position.X += roundprojectile.vitesse.X;
                else if (roundtime2 < 90)
                    roundprojectile2.position.Y += roundprojectile.vitesse.Y;
                else if (roundtime2 < 120)
                {
                    roundprojectile2.vitesse.X = 0;
                    roundprojectile2.vitesse.Y = 0;
                }
                else if (roundtime2 == 120)
                {
                    smallroundprojectileA2.position.X = roundprojectile2.position.X;
                    smallroundprojectileA2.position.Y = roundprojectile2.position.Y;
                    smallroundprojectileW2.position.X = roundprojectile2.position.X;
                    smallroundprojectileW2.position.Y = roundprojectile2.position.Y;
                    smallroundprojectileD2.position.X = roundprojectile2.position.X;
                    smallroundprojectileD2.position.Y = roundprojectile2.position.Y;
                    smallroundprojectileS2.position.X = roundprojectile2.position.X;
                    smallroundprojectileS2.position.Y = roundprojectile2.position.Y;
                    roundprojectile2.position.X = -50;
                    roundprojectile2.position.Y = -50;
                }
                else
                {
                    smallroundprojectileA2.position.X += smallroundprojectileA2.vitesse.X;
                    smallroundprojectileA2.position.Y += smallroundprojectileA2.vitesse.Y;
                    smallroundprojectileW2.position.X += smallroundprojectileW2.vitesse.X;
                    smallroundprojectileW2.position.Y += smallroundprojectileW2.vitesse.Y;
                    smallroundprojectileS2.position.X += smallroundprojectileS2.vitesse.X;
                    smallroundprojectileS2.position.Y += smallroundprojectileS2.vitesse.Y;
                    smallroundprojectileD2.position.X += smallroundprojectileD2.vitesse.X;
                    smallroundprojectileD2.position.Y += smallroundprojectileD2.vitesse.Y;
                }
                if (roundtime2 == 360)
                    roundtime2 = -1;
                roundtime2 += 1;
                if (hero.GetRect().Intersects(roundprojectile2.GetRect()) && roundtime2 < 120)
                {
                    hero.estVivant = false;
                    time = 0;
                }
                else if ((hero.GetRect().Intersects(smallroundprojectileW2.GetRect()) || hero.GetRect().Intersects(smallroundprojectileA2.GetRect()) || hero.GetRect().Intersects(smallroundprojectileS2.GetRect()) || hero.GetRect().Intersects(smallroundprojectileD2.GetRect())) && roundtime2 > 120)
                {
                    hero.estVivant = false;
                    time = 0;
                }
            }
            #endregion
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (hero.estVivant == true)
            {
                spriteBatch.Begin();

                if (time < 1500)
                    spriteBatch.Draw(background.sprite, background.position, Color.Turquoise);
                else if (time < 3000)
                    spriteBatch.Draw(background.sprite, background.position, Color.White);
                else if (time < 4500)
                    spriteBatch.Draw(background.sprite, background.position, Color.DarkGray);
                else if (time < 6000)
                    spriteBatch.Draw(background.sprite, background.position, Color.Gray);

                spriteBatch.Draw(hero.sprite, hero.position, Color.White);
                spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);
                spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
                if (time > 180)
                    spriteBatch.Draw(projectile2.sprite, projectile2.position, Color.White);
                if (time > 450)
                {
                    if (time < 4500)
                        spriteBatch.Draw(sneakprojectile.sprite, sneakprojectile.position, Color.White);
                    else
                        spriteBatch.Draw(sneakprojectile.sprite, sneakprojectile.position, Color.Red);
                } 
                if (time > 500)
                    spriteBatch.Draw(projectile3.sprite, projectile3.position, Color.White);
                if (time > 600)
                {
                    if (time < 4500)
                        spriteBatch.Draw(sneakprojectile2.sprite, sneakprojectile2.position, Color.White);
                    else
                        spriteBatch.Draw(sneakprojectile2.sprite, sneakprojectile2.position, Color.Red);
                }
                if (time > 1000)
                {
                    if (roundtime <= 120)
                        spriteBatch.Draw(roundprojectile.sprite, roundprojectile.position, Color.White);
                    else
                    {
                        spriteBatch.Draw(smallroundprojectileW.sprite, smallroundprojectileW.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileA.sprite, smallroundprojectileA.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileS.sprite, smallroundprojectileS.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileD.sprite, smallroundprojectileD.position, Color.White);
                    }
                }
                if (time > 1500)
                {
                    if (roundtime2 <= 120)
                        spriteBatch.Draw(roundprojectile2.sprite, roundprojectile2.position, Color.White);
                    else
                    {
                        spriteBatch.Draw(smallroundprojectileW2.sprite, smallroundprojectileW2.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileA2.sprite, smallroundprojectileA2.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileS2.sprite, smallroundprojectileS2.position, Color.White);
                        spriteBatch.Draw(smallroundprojectileD2.sprite, smallroundprojectileD2.position, Color.White);
                    }
                }
                spriteBatch.End();
            }
            else
                GraphicsDevice.Clear(Color.Red);

            if (time > 6000)
                GraphicsDevice.Clear(Color.Green);


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
