using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GameObjectPlayer ruby;
        GameObjectTile fond;
        GameObjectSlime[] slime;
        GameObjectBall[] ball;
        GameObjectBoss boss;
        GameObjectBoss[] projectileBoss; 
        GameObjectSword projectile;
        GameObjectSword sword;
        GameObject key;
        HeroLife vie;
        string state = "Load";
        byte time = 0;
        byte carte = 0;
        bool passage = false;
        Random random = new Random();
        int randomStore;
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
            this.graphics.PreferredBackBufferWidth = 1188;
            this.graphics.PreferredBackBufferHeight = 500;
            this.graphics.ApplyChanges();
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
            #region Ruby
            ruby = new GameObjectPlayer();
            ruby.direction = Vector2.Zero;
            ruby.objetState = GameObjectPlayer.etats.attenteHaut;
            ruby.position = new Rectangle(491, 385, 25, 43);   //Position initiale de Ruby
            ruby.sprite = Content.Load<Texture2D>("Ruby.png");
            ruby.spriteAfficher = ruby.waitHaut[ruby.waitState];
            vie = new HeroLife();
            vie.spriteAfficher[0] = vie.tabHP;
            for (byte i = 1; i < 6; i++)
                vie.spriteAfficher[i] = vie.tabVrai;
            for (byte i = 1; i < 6; i++)
                vie.objetState[i] = HeroLife.etats.vrai;
            vie.sprite = Content.Load<Texture2D>("Life.png");
            #endregion
            #region Slime
            slime = new GameObjectSlime[10];
            for (byte i = 0; i < 10; i++)
            {
                slime[i] = new GameObjectSlime();
                slime[i].estVivant = false;
                slime[i].position = new Rectangle(0, 0, 40, 50);
                slime[i].Time = 0;
                slime[i].objetState = GameObjectSlime.etats.droite;
                slime[i].sprite = Content.Load<Texture2D>("Slime.png");
                slime[i].spriteAfficher = slime[i].tabDie[slime[i].currentState];
            }
            #endregion
            #region Ball
            ball = new GameObjectBall[4];
            for (byte i = 0; i < 4; i++)
            {
                ball[i] = new GameObjectBall();
                ball[i].estVivant = false;
                ball[i].sprite = Content.Load<Texture2D>("Ball.png");
                ball[i].spriteAfficher = ball[i].currentSprite[ball[i].currentState];
            }
            #endregion
            #region Boss
            projectileBoss = new GameObjectBoss[24];
            for (byte i = 0; i < 20; i++)
            {
                projectileBoss[i] = new GameObjectBoss();
                projectileBoss[i].estVivant = false;
                projectileBoss[i].sprite = Content.Load<Texture2D>("Boss.png");
                projectileBoss[i].spriteAfficher = projectileBoss[i].projSprite[0];
            }
            for (byte i = 20; i < 24; i++)
            {
                projectileBoss[i] = new GameObjectBoss();
                projectileBoss[i].estVivant = false;
                projectileBoss[i].sprite = Content.Load<Texture2D>("Boss.png");
                projectileBoss[i].spriteAfficher = projectileBoss[i].projSprite[0];
            }
            #endregion
            #region Sword
            sword = new GameObjectSword();
            sword.sprite = Content.Load<Texture2D>("Sword.png");
            projectile = new GameObjectSword();
            projectile.estVivant = false;
            projectile.sprite = Content.Load<Texture2D>("Sword.png");
            #endregion
            key = new GameObject();
            key.sprite = Content.Load<Texture2D>("Key.png");
            fond = new GameObjectTile();
            fond.rectSource = new Rectangle(0, 0, 50, 50);
            fond.texture = Content.Load<Texture2D>("TileLayer.png");
            //fond.sprite = Content.Load<Texture2d>(“img”.png);


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
            if (state == "Game")
            {
                previousKeys = keys;
                keys = Keyboard.GetState();

                UpdateHero();
                UpdateEnnemi();
                UpdateProjectile();
                UpdateBackground();

                if (vie.vie[0] <= 0)
                    state = "Game Over";
            }
            if (state == "Load")
                UpdateBackground();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void UpdateHero()
        {
            if (state == "Game")
            {
                if (sword.time == 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        ruby.position.Y -= 2;
                    else if (Keyboard.GetState().IsKeyDown(Keys.A))
                        ruby.position.X -= 2;
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                        ruby.position.Y += 2;
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                        ruby.position.X += 2;

                    #region Sprite State
                    if (keys.IsKeyDown(Keys.W))
                        ruby.objetState = GameObjectPlayer.etats.runHaut;
                    else if (keys.IsKeyDown(Keys.A))
                        ruby.objetState = GameObjectPlayer.etats.runGauche;
                    else if (keys.IsKeyDown(Keys.S))
                        ruby.objetState = GameObjectPlayer.etats.runBas;
                    else if (keys.IsKeyDown(Keys.D))
                        ruby.objetState = GameObjectPlayer.etats.runDroite;
                    else if (previousKeys.IsKeyDown(Keys.W))
                        ruby.objetState = GameObjectPlayer.etats.attenteHaut;
                    else if (previousKeys.IsKeyDown(Keys.A))
                        ruby.objetState = GameObjectPlayer.etats.attenteGauche;
                    else if (previousKeys.IsKeyDown(Keys.S))
                        ruby.objetState = GameObjectPlayer.etats.attenteBas;
                    else if (previousKeys.IsKeyDown(Keys.D))
                        ruby.objetState = GameObjectPlayer.etats.attenteDroite;

                    //Compteur permettant de gérer le changement d'images
                    ruby.cpt++;
                    if (ruby.cpt == 12) //Vitesse défilement
                    {
                        //Gestion de la course verticale
                        if (ruby.objetState == GameObjectPlayer.etats.runHaut || ruby.objetState == GameObjectPlayer.etats.runBas)
                        {
                            ruby.runStateV++;

                            if (ruby.runStateV == ruby.nbEtatRunV)
                                ruby.runStateV = 0;
                        }

                        //Gestion de la course horizontale
                        if (ruby.objetState == GameObjectPlayer.etats.runDroite || ruby.objetState == GameObjectPlayer.etats.runGauche)
                        {
                            ruby.runStateH++;

                            if (ruby.runStateH == ruby.nbEtatRunH)
                                ruby.runStateH = 0;
                        }

                        //Gestion de la course horizontale
                        if (ruby.objetState == GameObjectPlayer.etats.attenteDroite || ruby.objetState == GameObjectPlayer.etats.attenteGauche || ruby.objetState == GameObjectPlayer.etats.attenteHaut || ruby.objetState == GameObjectPlayer.etats.attenteBas)
                        {
                            ruby.waitState++;

                            if (ruby.waitState == ruby.nbEtatWait)
                                ruby.waitState = 0;
                        }
                        ruby.cpt = 0;
                    }

                    //Changement de l'affichage
                    if (ruby.objetState == GameObjectPlayer.etats.attenteHaut)
                        ruby.spriteAfficher = ruby.waitHaut[ruby.waitState];
                    if (ruby.objetState == GameObjectPlayer.etats.attenteBas)
                        ruby.spriteAfficher = ruby.waitBas[ruby.waitState];
                    if (ruby.objetState == GameObjectPlayer.etats.attenteDroite)
                        ruby.spriteAfficher = ruby.waitDroite[ruby.waitState];
                    if (ruby.objetState == GameObjectPlayer.etats.attenteGauche)
                        ruby.spriteAfficher = ruby.waitGauche[ruby.waitState];
                    if (ruby.objetState == GameObjectPlayer.etats.runHaut)
                        ruby.spriteAfficher = ruby.tabRunHaut[ruby.runStateV];
                    if (ruby.objetState == GameObjectPlayer.etats.runBas)
                        ruby.spriteAfficher = ruby.tabRunBas[ruby.runStateV];
                    if (ruby.objetState == GameObjectPlayer.etats.runDroite)
                        ruby.spriteAfficher = ruby.tabRunDroite[ruby.runStateH];
                    if (ruby.objetState == GameObjectPlayer.etats.runGauche)
                        ruby.spriteAfficher = ruby.tabRunGauche[ruby.runStateH];
                    #endregion
                    #region Life Gauge (DO NOT TOUCH)
                    for (byte i = 1; i < vie.vie[1] + 1; i++)
                    {
                        if (vie.vie[0] >= i)
                            vie.objetState[i] = HeroLife.etats.vrai;
                        else
                            vie.objetState[i] = HeroLife.etats.faux;
                        if (vie.objetState[i] == HeroLife.etats.vrai)
                            vie.spriteAfficher[i] = vie.tabVrai;
                        else
                            vie.spriteAfficher[i] = vie.tabFaux;
                    }
                    #endregion
                    #region Colision Folder
                    if (ruby.position.Y + 44 > 500)
                        ruby.position.Y -= 2;
                    if (ruby.position.X + 27 > 1000)
                        ruby.position.X -= 2;
                    if (ruby.position.Y - 1 < 0)
                        ruby.position.Y += 2;
                    if (ruby.position.X - 1 < 0)
                        ruby.position.X += 2;
                    if ((fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 55 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 55 || (fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 55 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 55)) && ruby.time <= 0)
                    {
                        vie.vie[0]--;
                        ruby.time = 60;
                    }
                    if (fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] > 14 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 21 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 22 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 31 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 37 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 39 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 46 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 48 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 57 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] != 66)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.W))
                            ruby.position.Y += 2;
                        else if (Keyboard.GetState().IsKeyDown(Keys.A))
                            ruby.position.X += 2;
                    }
                    if (fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] > 14 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 21 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 22 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 31 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 37 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 39 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 46 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 48 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 57 && fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] != 66)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.W))
                            ruby.position.Y += 2;
                        else if (Keyboard.GetState().IsKeyDown(Keys.D))
                            ruby.position.X -= 2;
                    }
                    if (fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] > 14 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 21 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 22 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 31 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 37 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)ruby.position.X / 50] != 39 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 46 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 48 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 57 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] != 66)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                            ruby.position.X += 2;
                        if (Keyboard.GetState().IsKeyDown(Keys.S))
                            ruby.position.Y -= 2;
                    }
                    if (fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] > 14 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 21 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 22 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 31 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 37 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 39 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 46 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 48 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 57 && fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] != 66)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.S))
                            ruby.position.Y -= 2;
                        else if (Keyboard.GetState().IsKeyDown(Keys.D))
                            ruby.position.X -= 2;
                    }
                    #region Map Change
                    #region Up Left
                    if (fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 21 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 22 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 39 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 48 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 57 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)ruby.position.X / 50] == 66)
                    {
                        if (carte == 0)
                        {
                            carte = 1;
                            ruby.position.Y = 385;
                        }
                        else if (carte == 1)
                        {
                            carte = 2;
                            ruby.position.Y = 405;
                        }
                        else if (carte == 2)
                        {
                            if (ruby.position.Y > 405)
                            {
                                carte = 1;
                                ruby.position.Y = 40;
                            }
                            else if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            else if (ruby.position.X < 75)
                            {
                                carte = 3;
                                ruby.position.X = 915;
                            }
                        }
                        else if (carte == 3)
                        {
                            if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            if (ruby.position.Y > 405)
                            {
                                carte = 4;
                                ruby.position.Y = 40;
                            }
                        }
                        else if (carte == 4)
                        {
                            if (ruby.position.Y < 75)
                            {
                                carte = 3;
                                ruby.position.Y = 405;
                            }
                        }
                        state = "Load";
                        time = 30;
                    }
                    #endregion
                    #region Up Right
                    else if (fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 21 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 22 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 39 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 48 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 57 || fond.map[(int)(ruby.position.Y + 15) / 50, (int)(ruby.position.X + 25) / 50] == 66)
                    {
                        if (carte == 0)
                        {
                            carte = 1;
                            ruby.position.Y = 385;
                        }
                        else if (carte == 1)
                        {
                            carte = 2;
                            ruby.position.Y = 405;
                        }
                        else if (carte == 2)
                        {
                            if (ruby.position.Y > 405)
                            {
                                carte = 1;
                                ruby.position.Y = 40;
                            }
                            else if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            else if (ruby.position.X < 75)
                            {
                                carte = 3;
                                ruby.position.X = 915;
                            }
                        }
                        else if (carte == 3)
                        {
                            if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            if (ruby.position.Y > 405)
                            {
                                carte = 4;
                                ruby.position.Y = 40;
                            }
                        }
                        else if (carte == 4)
                        {
                            if (ruby.position.Y < 75)
                            {
                                carte = 3;
                                ruby.position.Y = 915;
                            }
                        }
                        state = "Load";
                        time = 30;
                    }
                    #endregion
                    #region Down Left
                    else if (fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 21 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 22 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)ruby.position.X / 50] == 39 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 48 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 57 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X) / 50] == 66)
                    {
                        if (carte == 0)
                        {
                            carte = 1;
                            ruby.position.Y = 385;
                        }
                        else if (carte == 1)
                        {
                            carte = 2;
                            ruby.position.Y = 405;
                        }
                        else if (carte == 2)
                        {
                            if (ruby.position.Y > 405)
                            {
                                carte = 1;
                                ruby.position.Y = 40;
                            }
                            else if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            else if (ruby.position.X < 75)
                            {
                                carte = 3;
                                ruby.position.X = 915;
                            }
                        }
                        else if (carte == 3)
                        {
                            if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            if (ruby.position.Y > 405)
                            {
                                carte = 4;
                                ruby.position.Y = 40;
                            }
                        }
                        else if (carte == 4)
                        {
                            if (ruby.position.Y < 75)
                            {
                                carte = 3;
                                ruby.position.Y = 915;
                            }
                        }
                        state = "Load";
                        time = 30;
                    }
                    #endregion
                    #region Down Right
                    else if (fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 21 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 22 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 39 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 48 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 57 || fond.map[(int)(ruby.position.Y + 43) / 50, (int)(ruby.position.X + 25) / 50] == 66)
                    {
                        if (carte == 0)
                        {
                            carte = 1;
                            ruby.position.Y = 385;
                        }
                        else if (carte == 1)
                        {
                            carte = 2;
                            ruby.position.Y = 405;
                        }
                        else if (carte == 2)
                        {
                            if (ruby.position.Y > 405)
                            {
                                carte = 1;
                                ruby.position.Y = 40;
                            }
                            else if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            else if (ruby.position.X < 75)
                            {
                                carte = 3;
                                ruby.position.X = 915;
                            }
                        }
                        else if (carte == 3)
                        {
                            if (ruby.position.X > 924)
                            {
                                carte = 2;
                                ruby.position.X = 60;
                            }
                            if (ruby.position.Y > 405)
                            {
                                carte = 4;
                                ruby.position.Y = 40;
                            }
                        }
                        else if (carte == 4)
                        {
                            if (ruby.position.Y < 75)
                            {
                                carte = 3;
                                ruby.position.Y = 915;
                            }
                        }
                        state = "Load";
                        time = 30;
                    }
                    #endregion
                    #endregion
                    #endregion

                    if (ruby.time > 0)
                        ruby.time--;
                }
            }
        }
        public void UpdateEnnemi()
        {
            #region Slime [10]
            for (byte i = 0; i < 10; i++)
            {
                if (slime[i].estVivant)
                {
                    if (slime[i].Time <= 0)
                    {
                        slime[i].Time = 100;
                        randomStore = random.Next(0, 4);
                        if (randomStore == 0) // W
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 46)
                            {
                                if (fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 21 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 22 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 46)
                                {
                                    slime[i].vitesse.Y = 0;
                                    slime[i].Time = 0;
                                }
                                else
                                    slime[i].vitesse.Y = 1;
                            }
                            else
                                slime[i].vitesse.Y = -1;

                            slime[i].vitesse.X = 0;
                        }
                        else if (randomStore == 1) // A
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 46)
                            {
                                if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 46)
                                {
                                    slime[i].vitesse.X = 0;
                                    slime[i].Time = 0;
                                }
                                else
                                {
                                    slime[i].vitesse.X = 1;
                                    slime[i].objetState = GameObjectSlime.etats.droite;
                                }
                            }
                            else
                            {
                                slime[i].vitesse.X = -1;
                                slime[i].objetState = GameObjectSlime.etats.gauche;
                            }

                            slime[i].vitesse.Y = 0;
                        }
                        if (randomStore == 2) // S
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 21 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 22 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 46)
                            {
                                if (fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 46)
                                {
                                    slime[i].vitesse.Y = 0;
                                    slime[i].Time = 0;
                                }
                                else
                                    slime[i].vitesse.Y = -1;
                            }
                            else
                                slime[i].vitesse.Y = 1;

                            slime[i].vitesse.X = 0;
                        }
                        else if (randomStore == 3) // D
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 46)
                            {
                                if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 46)
                                {
                                    slime[i].vitesse.X = 0;
                                    slime[i].Time = 0;
                                }
                                else
                                {
                                    slime[i].vitesse.X = -1;
                                    slime[i].objetState = GameObjectSlime.etats.gauche;
                                }
                            }
                            else
                            {
                                slime[i].vitesse.X = 1;
                                slime[i].objetState = GameObjectSlime.etats.droite;
                            }

                            slime[i].vitesse.Y = 0;
                        }
                    }
                    if (slime[i].Time > 50)
                    {
                        slime[i].position.X += (int)slime[i].vitesse.X;
                        slime[i].position.Y += (int)slime[i].vitesse.Y;
                    }
                    //Compteur permettant de gérer le changement d'images
                    slime[i].cpt++;
                    if (slime[i].cpt == 24) //Vitesse défilement
                    {
                        slime[i].currentState++;

                        if (slime[i].currentState == slime[i].nbState)
                            slime[i].currentState = 0;
                    }
                    if (slime[i].objetState == GameObjectSlime.etats.droite)
                        slime[i].spriteAfficher = slime[i].tabDroite[2-slime[i].vie];
                    else if (slime[i].objetState == GameObjectSlime.etats.gauche)
                        slime[i].spriteAfficher = slime[i].tabGauche[2-slime[i].vie];
                    if (slime[i].currentState == 0)
                        slime[i].position.Height = 50;
                    else
                        slime[i].position.Height = 48;
                    slime[i].Time--;

                    if (slime[i].position.Intersects(ruby.position) && ruby.time == 0)
                    {
                        vie.vie[0]--;
                        ruby.time = 60;
                    }
                    if (slime[i].position.Intersects(sword.position) && sword.estVivant && slime[i].time == 0)
                    {
                        slime[i].vie--;
                        slime[i].time = 60;
                        if (sword.objetState == GameObjectSword.etats.haut)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 46)
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                            else
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50) - 1) * 50 + 1;
                            slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                        }
                        else if (sword.objetState == GameObjectSword.etats.bas)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 21 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 22 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 46)
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                            else
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50) + 1) * 50 + 1;
                            slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                        }
                        else if (sword.objetState == GameObjectSword.etats.droite)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 46)
                                slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                            else
                                slime[i].position.X = ((int)(slime[i].position.X / 50) + 1) * 50 + 1;
                            slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                        }
                        else if (sword.objetState == GameObjectSword.etats.gauche)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 46)
                                slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                            else
                                slime[i].position.X = ((int)(slime[i].position.X / 50) - 1) * 50 + 1;
                            slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                        }
                        slime[i].Time = 0;
                        if (slime[i].vie == 0)
                            slime[i].estVivant = false;
                    }
                    if (slime[i].position.Intersects(projectile.position) && projectile.estVivant && slime[i].time == 0)
                    {
                        slime[i].vie--;
                        slime[i].time = 60;
                        if (projectile.objetState == GameObjectSword.etats.haut)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) - 1, (int)slime[i].position.X / 50] != 46)
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                            else
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50) - 1) * 50 + 1;
                            slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                        }
                        else if (projectile.objetState == GameObjectSword.etats.bas)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] > 14 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 21 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 22 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 31 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 37 && fond.map[(int)(slime[i].position.Y / 50) + 1, (int)slime[i].position.X / 50] != 46)
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                            else
                                slime[i].position.Y = ((int)(slime[i].position.Y / 50) + 1) * 50 + 1;
                            slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                        }
                        else if (projectile.objetState == GameObjectSword.etats.droite)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) + 1] != 46)
                                slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                            else
                                slime[i].position.X = ((int)(slime[i].position.X / 50) + 1) * 50 + 1;
                            slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                        }
                        else if (projectile.objetState == GameObjectSword.etats.gauche)
                        {
                            if (fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] > 14 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 31 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 37 && fond.map[(int)(slime[i].position.Y / 50), (int)(slime[i].position.X / 50) - 1] != 46)
                                slime[i].position.X = ((int)(slime[i].position.X / 50)) * 50 + 1;
                            else
                                slime[i].position.X = ((int)(slime[i].position.X / 50) - 1) * 50 + 1;
                            slime[i].position.Y = ((int)(slime[i].position.Y / 50)) * 50 + 1;
                        }
                        projectile.estVivant = false;
                        slime[i].Time = 0;
                        if (slime[i].vie == 0)
                        {
                            slime[i].estVivant = false;
                        }
                    }
                    if (!slime[i].estVivant && i == 0 && carte == 4)
                    {
                        key.estVivant = true;
                        key.position.X = slime[i].position.X + 11;
                        key.position.Y = slime[i].position.Y + 11;
                        key.position.Height = 27;
                        key.position.Width = 18;
                    }
                    if (slime[i].time > 0)
                        slime[i].time--;
                }
            }
            #endregion
            #region Ball [4]
            for (byte i = 0; i < 4; i++)
            {
                if (ball[i].estVivant)
                {
                    ball[i].position.X += (int)ball[i].vitesse.X;
                    ball[i].position.Y += (int)ball[i].vitesse.Y;
                    if (fond.map[(int)(ball[i].position.Y / 50), (int)(ball[i].position.X / 50)] == 40 || fond.map[(int)(ball[i].position.Y / 50), (int)(ball[i].position.X / 50)] == 49 || fond.map[(int)(ball[i].position.Y / 50), (int)(ball[i].position.X / 50)] == 58 || fond.map[(int)(ball[i].position.Y / 50), (int)(ball[i].position.X / 50)] == 67 || fond.map[(int)(ball[i].position.Y + 40) / 50, (int)(ball[i].position.X + 40) / 50] == 40 || fond.map[(int)(ball[i].position.Y + 40) / 50, (int)(ball[i].position.X + 40) / 50] == 49 || fond.map[(int)(ball[i].position.Y + 40) / 50, (int)(ball[i].position.X + 40) / 50] == 58 || fond.map[(int)(ball[i].position.Y + 40) / 50, (int)(ball[i].position.X + 40) / 50] == 67)
                    {
                        if (carte == 3)
                            ball[i].position.X = 909;
                    }
                    if (ball[i].position.Intersects(ruby.position) && ruby.time == 0)
                    {
                        vie.vie[0]--;
                        ruby.time = 60;
                    }
                    ball[i].cpt++;
                    if (ball[i].cpt == 8) //Vitesse défilement
                    {
                        ball[i].currentState++;

                        if (ball[i].currentState == ball[i].nbState)
                            ball[i].currentState = 0;

                        ball[i].cpt = 0;
                    }
                    ball[i].spriteAfficher = ball[i].currentSprite[ball[i].currentState];
                }
            }
            #endregion
            #region Key
            if (key.estVivant)
            {
                if (key.position.Intersects(ruby.position))
                {
                    if (carte == 4)
                        passage = true;

                    key.estVivant = false;
                }
            }
            #endregion
        }
        public void UpdateProjectile()
        {
            if (sword.time == 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    sword.estVivant = true;
                    sword.time = 31;
                    if (ruby.objetState == GameObjectPlayer.etats.attenteHaut || ruby.objetState == GameObjectPlayer.etats.runHaut)
                    {
                        ruby.spriteAfficher = ruby.attackHaut;
                        sword.objetState = GameObjectSword.etats.haut;
                        sword.spriteAfficher = sword.swordH;
                        sword.position.X = ruby.position.X + 8;
                        sword.position.Y = ruby.position.Y - 42;
                        sword.position.Width = 8;
                        sword.position.Height = 42;
                        if (vie.vie[0] == 1 && !projectile.estVivant)
                        {
                            projectile.objetState = GameObjectSword.etats.haut;
                            projectile.estVivant = true;
                            projectile.spriteAfficher = projectile.swordProjectileH;
                            projectile.position.X = ruby.position.X + 8;
                            projectile.position.Y = ruby.position.Y - 42;
                            projectile.vitesse.Y = -5;
                            projectile.vitesse.X = 0;
                            projectile.position.Width = 8;
                            projectile.position.Height = 24;
                        }
                    }
                    else if (ruby.objetState == GameObjectPlayer.etats.attenteBas || ruby.objetState == GameObjectPlayer.etats.runBas)
                    {
                        ruby.spriteAfficher = ruby.attackBas;
                        sword.spriteAfficher = sword.swordB;
                        sword.objetState = GameObjectSword.etats.bas;
                        sword.position.X = ruby.position.X + 8;
                        sword.position.Y = ruby.position.Y + 43;
                        sword.position.Width = 8;
                        sword.position.Height = 42;
                        if (vie.vie[0] == 1 && !projectile.estVivant)
                        {
                            projectile.objetState = GameObjectSword.etats.bas;
                            projectile.estVivant = true;
                            projectile.spriteAfficher = projectile.swordProjectileB;
                            projectile.position.X = ruby.position.X + 8;
                            projectile.position.Y = ruby.position.Y - 42;
                            projectile.vitesse.Y = 5;
                            projectile.vitesse.X = 0;
                            projectile.position.Width = 8;
                            projectile.position.Height = 24;
                        }
                    }
                    else if (ruby.objetState == GameObjectPlayer.etats.attenteDroite || ruby.objetState == GameObjectPlayer.etats.runDroite)
                    {
                        ruby.spriteAfficher = ruby.attackDroite;
                        sword.spriteAfficher = sword.swordD;
                        sword.objetState = GameObjectSword.etats.droite;
                        sword.position.X = ruby.position.X + 25;
                        sword.position.Y = ruby.position.Y + 21;
                        sword.position.Width = 42;
                        sword.position.Height = 8;
                        if (vie.vie[0] == 1 && !projectile.estVivant)
                        {
                            projectile.objetState = GameObjectSword.etats.droite;
                            projectile.estVivant = true;
                            projectile.spriteAfficher = projectile.swordProjectileD;
                            projectile.position.X = ruby.position.X + 25;
                            projectile.position.Y = ruby.position.Y + 21;
                            projectile.vitesse.Y = 0;
                            projectile.vitesse.X = 5;
                            projectile.position.Width = 24;
                            projectile.position.Height = 8;
                        }
                    }
                    else if (ruby.objetState == GameObjectPlayer.etats.attenteGauche || ruby.objetState == GameObjectPlayer.etats.runGauche)
                    {
                        ruby.spriteAfficher = ruby.attackGauche;
                        sword.objetState = GameObjectSword.etats.gauche;
                        sword.spriteAfficher = sword.swordG;
                        sword.position.X = ruby.position.X - 42;
                        sword.position.Y = ruby.position.Y + 21;
                        sword.position.Width = 42;
                        sword.position.Height = 8;
                        if (vie.vie[0] == 1 && !projectile.estVivant)
                        {
                            projectile.objetState = GameObjectSword.etats.gauche;
                            projectile.estVivant = true;
                            projectile.spriteAfficher = projectile.swordProjectileG;
                            projectile.position.X = ruby.position.X + 25;
                            projectile.position.Y = ruby.position.Y + 21;
                            projectile.vitesse.Y = 0;
                            projectile.vitesse.X = -5;
                            projectile.position.Width = 24;
                            projectile.position.Height = 8;
                        }
                    }
                }
            }
            else
            {
                sword.time--;
                if (sword.time == 0)
                    sword.estVivant = false;
            }
            if (projectile.estVivant)
            {
                projectile.position.X += (int)projectile.vitesse.X;
                projectile.position.Y += (int)projectile.vitesse.Y;
            }
            #region Colision Folder
            if (projectile.estVivant)
            {
                if (projectile.objetState == GameObjectSword.etats.haut)
                {
                    if (projectile.position.Y <= 0)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] > 14 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 31 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 37 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 46)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 8) / 50] > 14 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 8) / 50] != 31 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 8) / 50] != 37 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 8) / 50] != 46)
                        projectile.estVivant = false;
                }
                else if (projectile.objetState == GameObjectSword.etats.bas)
                {
                    if (projectile.position.Y + 42 > 500)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X / 50)] > 14 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X / 50)] != 31 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X / 50)] != 37 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X / 50)] != 46)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X + 8) / 50] > 14 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X + 8) / 50] != 31 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X + 8) / 50] != 37 && fond.map[(int)(projectile.position.Y + 24) / 50, (int)(projectile.position.X + 8) / 50] != 46)
                        projectile.estVivant = false;
                }
                else if (projectile.objetState == GameObjectSword.etats.droite)
                {
                    if (projectile.position.X >= 1000)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 24) / 50] > 14 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 24) / 50] != 31 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 24) / 50] != 37 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X + 24) / 50] != 46)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X + 24) / 50] > 14 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X + 24) / 50] != 31 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X + 24) / 50] != 37 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X + 24) / 50] != 46)
                        projectile.estVivant = false;
                }
                else if (projectile.objetState == GameObjectSword.etats.gauche)
                {
                    if (projectile.position.X <= 0)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] > 14 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 31 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 37 && fond.map[(int)(projectile.position.Y) / 50, (int)(projectile.position.X / 50)] != 46)
                        projectile.estVivant = false;
                    else if (fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X) / 50] > 14 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X) / 50] != 31 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X) / 50] != 37 && fond.map[(int)(projectile.position.Y + 8) / 50, (int)(projectile.position.X) / 50] != 46)
                        projectile.estVivant = false;
                }
            }
            #endregion
        }
        public void UpdateBackground()
        {
            if (state == "Load")
            {
                #region Outworld
                if (carte == 0)
                {
                    #region Ligne 1
                    fond.map[0, 0] = 4;
                    fond.map[0, 1] = 4;
                    fond.map[0, 2] = 4;
                    fond.map[0, 3] = 4;
                    fond.map[0, 4] = 17;
                    fond.map[0, 5] = 17;
                    fond.map[0, 6] = 23;
                    fond.map[0, 7] = 25;
                    fond.map[0, 8] = 17;
                    fond.map[0, 9] = 15;
                    fond.map[0, 10] = 16;
                    fond.map[0, 11] = 17;
                    fond.map[0, 12] = 26;
                    fond.map[0, 13] = 24;
                    fond.map[0, 14] = 17;
                    fond.map[0, 15] = 17;
                    fond.map[0, 16] = 4;
                    fond.map[0, 17] = 4;
                    fond.map[0, 18] = 4;
                    fond.map[0, 19] = 4;
                    #endregion
                    #region Ligne 2
                    fond.map[1, 0] = 4;
                    fond.map[1, 1] = 4;
                    fond.map[1, 2] = 4;
                    fond.map[1, 3] = 4;
                    fond.map[1, 4] = 23;
                    fond.map[1, 5] = 18;
                    fond.map[1, 6] = 25;
                    fond.map[1, 7] = 17;
                    fond.map[1, 8] = 23;
                    fond.map[1, 9] = 19;
                    fond.map[1, 10] = 20;
                    fond.map[1, 11] = 24;
                    fond.map[1, 12] = 17;
                    fond.map[1, 13] = 26;
                    fond.map[1, 14] = 18;
                    fond.map[1, 15] = 24;
                    fond.map[1, 16] = 4;
                    fond.map[1, 17] = 4;
                    fond.map[1, 18] = 4;
                    fond.map[1, 19] = 4;
                    #endregion
                    #region Ligne 3
                    fond.map[2, 0] = 4;
                    fond.map[2, 1] = 4;
                    fond.map[2, 2] = 4;
                    fond.map[2, 3] = 4;
                    fond.map[2, 4] = 4;
                    fond.map[2, 5] = 4;
                    fond.map[2, 6] = 4;
                    fond.map[2, 7] = 23;
                    fond.map[2, 8] = 18;
                    fond.map[2, 9] = 21;
                    fond.map[2, 10] = 22;
                    fond.map[2, 11] = 18;
                    fond.map[2, 12] = 24;
                    fond.map[2, 13] = 4;
                    fond.map[2, 14] = 4;
                    fond.map[2, 15] = 4;
                    fond.map[2, 16] = 4;
                    fond.map[2, 17] = 4;
                    fond.map[2, 18] = 4;
                    fond.map[2, 19] = 4;
                    #endregion
                    #region Ligne 4
                    fond.map[3, 0] = 4;
                    fond.map[3, 1] = 4;
                    fond.map[3, 2] = 4;
                    fond.map[3, 3] = 4;
                    fond.map[3, 4] = 4;
                    fond.map[3, 5] = 4;
                    fond.map[3, 6] = 4;
                    fond.map[3, 7] = 10;
                    fond.map[3, 8] = 9;
                    fond.map[3, 9] = 6;
                    fond.map[3, 10] = 6;
                    fond.map[3, 11] = 8;
                    fond.map[3, 12] = 12;
                    fond.map[3, 13] = 4;
                    fond.map[3, 14] = 4;
                    fond.map[3, 15] = 4;
                    fond.map[3, 16] = 4;
                    fond.map[3, 17] = 4;
                    fond.map[3, 18] = 4;
                    fond.map[3, 19] = 4;
                    #endregion
                    #region Ligne 5
                    fond.map[4, 0] = 4;
                    fond.map[4, 1] = 4;
                    fond.map[4, 2] = 4;
                    fond.map[4, 3] = 4;
                    fond.map[4, 4] = 4;
                    fond.map[4, 5] = 4;
                    fond.map[4, 6] = 4;
                    fond.map[4, 7] = 4;
                    fond.map[4, 8] = 5;
                    fond.map[4, 9] = 6;
                    fond.map[4, 10] = 6;
                    fond.map[4, 11] = 7;
                    fond.map[4, 12] = 4;
                    fond.map[4, 13] = 4;
                    fond.map[4, 14] = 4;
                    fond.map[4, 15] = 4;
                    fond.map[4, 16] = 4;
                    fond.map[4, 17] = 4;
                    fond.map[4, 18] = 4;
                    fond.map[4, 19] = 4;
                    #endregion
                    #region Ligne 6
                    fond.map[5, 0] = 4;
                    fond.map[5, 1] = 4;
                    fond.map[5, 2] = 4;
                    fond.map[5, 3] = 4;
                    fond.map[5, 4] = 4;
                    fond.map[5, 5] = 4;
                    fond.map[5, 6] = 4;
                    fond.map[5, 7] = 4;
                    fond.map[5, 8] = 5;
                    fond.map[5, 9] = 6;
                    fond.map[5, 10] = 6;
                    fond.map[5, 11] = 7;
                    fond.map[5, 12] = 4;
                    fond.map[5, 13] = 4;
                    fond.map[5, 14] = 4;
                    fond.map[5, 15] = 4;
                    fond.map[5, 16] = 1;
                    fond.map[5, 17] = 3;
                    fond.map[5, 18] = 4;
                    fond.map[5, 19] = 4;
                    #endregion
                    #region Ligne 7
                    fond.map[6, 0] = 4;
                    fond.map[6, 1] = 4;
                    fond.map[6, 2] = 4;
                    fond.map[6, 3] = 4;
                    fond.map[6, 4] = 4;
                    fond.map[6, 5] = 4;
                    fond.map[6, 6] = 4;
                    fond.map[6, 7] = 4;
                    fond.map[6, 8] = 5;
                    fond.map[6, 9] = 6;
                    fond.map[6, 10] = 6;
                    fond.map[6, 11] = 7;
                    fond.map[6, 12] = 4;
                    fond.map[6, 13] = 4;
                    fond.map[6, 14] = 4;
                    fond.map[6, 15] = 4;
                    fond.map[6, 16] = 5;
                    fond.map[6, 17] = 7;
                    fond.map[6, 18] = 4;
                    fond.map[6, 19] = 4;
                    #endregion
                    #region Ligne 8
                    fond.map[7, 0] = 4;
                    fond.map[7, 1] = 4;
                    fond.map[7, 2] = 4;
                    fond.map[7, 3] = 4;
                    fond.map[7, 4] = 4;
                    fond.map[7, 5] = 4;
                    fond.map[7, 6] = 4;
                    fond.map[7, 7] = 4;
                    fond.map[7, 8] = 5;
                    fond.map[7, 9] = 6;
                    fond.map[7, 10] = 6;
                    fond.map[7, 11] = 7;
                    fond.map[7, 12] = 4;
                    fond.map[7, 13] = 4;
                    fond.map[7, 14] = 4;
                    fond.map[7, 15] = 4;
                    fond.map[7, 16] = 10;
                    fond.map[7, 17] = 12;
                    fond.map[7, 18] = 4;
                    fond.map[7, 19] = 4;
                    #endregion
                    #region Ligne 9
                    fond.map[8, 0] = 4;
                    fond.map[8, 1] = 4;
                    fond.map[8, 2] = 4;
                    fond.map[8, 3] = 4;
                    fond.map[8, 4] = 4;
                    fond.map[8, 5] = 4;
                    fond.map[8, 6] = 4;
                    fond.map[8, 7] = 4;
                    fond.map[8, 8] = 5;
                    fond.map[8, 9] = 6;
                    fond.map[8, 10] = 6;
                    fond.map[8, 11] = 7;
                    fond.map[8, 12] = 4;
                    fond.map[8, 13] = 4;
                    fond.map[8, 14] = 4;
                    fond.map[8, 15] = 4;
                    fond.map[8, 16] = 4;
                    fond.map[8, 17] = 4;
                    fond.map[8, 18] = 4;
                    fond.map[8, 19] = 4;
                    #endregion
                    #region Ligne 10
                    fond.map[9, 0] = 4;
                    fond.map[9, 1] = 4;
                    fond.map[9, 2] = 4;
                    fond.map[9, 3] = 4;
                    fond.map[9, 4] = 4;
                    fond.map[9, 5] = 4;
                    fond.map[9, 6] = 4;
                    fond.map[9, 7] = 4;
                    fond.map[9, 8] = 5;
                    fond.map[9, 9] = 6;
                    fond.map[9, 10] = 6;
                    fond.map[9, 11] = 7;
                    fond.map[9, 12] = 4;
                    fond.map[9, 13] = 4;
                    fond.map[9, 14] = 4;
                    fond.map[9, 15] = 4;
                    fond.map[9, 16] = 4;
                    fond.map[9, 17] = 4;
                    fond.map[9, 18] = 4;
                    fond.map[9, 19] = 4;
                    #endregion
                }
                #endregion
                #region Dungeon Room 1
                if (carte == 1)
                {
                    #region Ligne 1
                    fond.map[0, 0] = 27;
                    fond.map[0, 1] = 29;
                    fond.map[0, 2] = 28;
                    fond.map[0, 3] = 29;
                    fond.map[0, 4] = 39;
                    fond.map[0, 5] = 28;
                    fond.map[0, 6] = 29;
                    fond.map[0, 7] = 28;
                    fond.map[0, 8] = 30;
                    fond.map[0, 9] = 69;
                    fond.map[0, 10] = 69;
                    fond.map[0, 11] = 27;
                    fond.map[0, 12] = 29;
                    fond.map[0, 13] = 28;
                    fond.map[0, 14] = 29;
                    fond.map[0, 15] = 39;
                    fond.map[0, 16] = 28;
                    fond.map[0, 17] = 29;
                    fond.map[0, 18] = 28;
                    fond.map[0, 19] = 30;
                    #endregion
                    #region Ligne 2
                    fond.map[1, 0] = 42;
                    fond.map[1, 1] = 31;
                    fond.map[1, 2] = 31;
                    fond.map[1, 3] = 31;
                    fond.map[1, 4] = 31;
                    fond.map[1, 5] = 31;
                    fond.map[1, 6] = 31;
                    fond.map[1, 7] = 31;
                    fond.map[1, 8] = 45;
                    fond.map[1, 9] = 69;
                    fond.map[1, 10] = 69;
                    fond.map[1, 11] = 42;
                    fond.map[1, 12] = 31;
                    fond.map[1, 13] = 31;
                    fond.map[1, 14] = 31;
                    fond.map[1, 15] = 31;
                    fond.map[1, 16] = 31;
                    fond.map[1, 17] = 31;
                    fond.map[1, 18] = 31;
                    fond.map[1, 19] = 45;
                    #endregion
                    #region Ligne 3
                    fond.map[2, 0] = 33;
                    fond.map[2, 1] = 31;
                    fond.map[2, 2] = 37;
                    fond.map[2, 3] = 37;
                    fond.map[2, 4] = 37;
                    fond.map[2, 5] = 37;
                    fond.map[2, 6] = 37;
                    fond.map[2, 7] = 31;
                    fond.map[2, 8] = 36;
                    fond.map[2, 9] = 69;
                    fond.map[2, 10] = 69;
                    fond.map[2, 11] = 33;
                    fond.map[2, 12] = 31;
                    fond.map[2, 13] = 37;
                    fond.map[2, 14] = 37;
                    fond.map[2, 15] = 37;
                    fond.map[2, 16] = 37;
                    fond.map[2, 17] = 37;
                    fond.map[2, 18] = 31;
                    fond.map[2, 19] = 36;
                    #endregion
                    #region Ligne 4
                    fond.map[3, 0] = 42;
                    fond.map[3, 1] = 31;
                    fond.map[3, 2] = 37;
                    fond.map[3, 3] = 37;
                    fond.map[3, 4] = 37;
                    fond.map[3, 5] = 37;
                    fond.map[3, 6] = 37;
                    fond.map[3, 7] = 31;
                    fond.map[3, 8] = 43;
                    fond.map[3, 9] = 29;
                    fond.map[3, 10] = 28;
                    fond.map[3, 11] = 44;
                    fond.map[3, 12] = 31;
                    fond.map[3, 13] = 37;
                    fond.map[3, 14] = 37;
                    fond.map[3, 15] = 37;
                    fond.map[3, 16] = 37;
                    fond.map[3, 17] = 37;
                    fond.map[3, 18] = 31;
                    fond.map[3, 19] = 45;
                    #endregion
                    #region Ligne 5
                    fond.map[4, 0] = 33;
                    fond.map[4, 1] = 31;
                    fond.map[4, 2] = 37;
                    fond.map[4, 3] = 37;
                    fond.map[4, 4] = 37;
                    fond.map[4, 5] = 37;
                    fond.map[4, 6] = 37;
                    fond.map[4, 7] = 31;
                    fond.map[4, 8] = 31;
                    fond.map[4, 9] = 31;
                    fond.map[4, 10] = 31;
                    fond.map[4, 11] = 31;
                    fond.map[4, 12] = 31;
                    fond.map[4, 13] = 37;
                    fond.map[4, 14] = 37;
                    fond.map[4, 15] = 37;
                    fond.map[4, 16] = 37;
                    fond.map[4, 17] = 37;
                    fond.map[4, 18] = 31;
                    fond.map[4, 19] = 36;
                    #endregion
                    #region Ligne 6
                    fond.map[5, 0] = 42;
                    fond.map[5, 1] = 31;
                    fond.map[5, 2] = 37;
                    fond.map[5, 3] = 37;
                    fond.map[5, 4] = 37;
                    fond.map[5, 5] = 37;
                    fond.map[5, 6] = 37;
                    fond.map[5, 7] = 37;
                    fond.map[5, 8] = 37;
                    fond.map[5, 9] = 37;
                    fond.map[5, 10] = 37;
                    fond.map[5, 11] = 37;
                    fond.map[5, 12] = 37;
                    fond.map[5, 13] = 37;
                    fond.map[5, 14] = 37;
                    fond.map[5, 15] = 37;
                    fond.map[5, 16] = 37;
                    fond.map[5, 17] = 37;
                    fond.map[5, 18] = 31;
                    fond.map[5, 19] = 45;
                    #endregion
                    #region Ligne 7
                    fond.map[6, 0] = 33;
                    fond.map[6, 1] = 31;
                    fond.map[6, 2] = 37;
                    fond.map[6, 3] = 37;
                    fond.map[6, 4] = 37;
                    fond.map[6, 5] = 37;
                    fond.map[6, 6] = 37;
                    fond.map[6, 7] = 37;
                    fond.map[6, 8] = 37;
                    fond.map[6, 9] = 37;
                    fond.map[6, 10] = 37;
                    fond.map[6, 11] = 37;
                    fond.map[6, 12] = 37;
                    fond.map[6, 13] = 37;
                    fond.map[6, 14] = 37;
                    fond.map[6, 15] = 37;
                    fond.map[6, 16] = 37;
                    fond.map[6, 17] = 37;
                    fond.map[6, 18] = 31;
                    fond.map[6, 19] = 36;
                    #endregion
                    #region Ligne 8
                    fond.map[7, 0] = 42;
                    fond.map[7, 1] = 31;
                    fond.map[7, 2] = 31;
                    fond.map[7, 3] = 31;
                    fond.map[7, 4] = 31;
                    fond.map[7, 5] = 31;
                    fond.map[7, 6] = 31;
                    fond.map[7, 7] = 37;
                    fond.map[7, 8] = 37;
                    fond.map[7, 9] = 37;
                    fond.map[7, 10] = 37;
                    fond.map[7, 11] = 37;
                    fond.map[7, 12] = 37;
                    fond.map[7, 13] = 31;
                    fond.map[7, 14] = 31;
                    fond.map[7, 15] = 31;
                    fond.map[7, 16] = 31;
                    fond.map[7, 17] = 31;
                    fond.map[7, 18] = 31;
                    fond.map[7, 19] = 45;
                    #endregion
                    #region Ligne 9
                    fond.map[8, 0] = 51;
                    fond.map[8, 1] = 53;
                    fond.map[8, 2] = 52;
                    fond.map[8, 3] = 53;
                    fond.map[8, 4] = 52;
                    fond.map[8, 5] = 35;
                    fond.map[8, 6] = 31;
                    fond.map[8, 7] = 31;
                    fond.map[8, 8] = 31;
                    fond.map[8, 9] = 31;
                    fond.map[8, 10] = 31;
                    fond.map[8, 11] = 31;
                    fond.map[8, 12] = 31;
                    fond.map[8, 13] = 31;
                    fond.map[8, 14] = 34;
                    fond.map[8, 15] = 53;
                    fond.map[8, 16] = 52;
                    fond.map[8, 17] = 53;
                    fond.map[8, 18] = 52;
                    fond.map[8, 19] = 54;
                    #endregion
                    #region Ligne 10
                    fond.map[9, 0] = 69;
                    fond.map[9, 1] = 69;
                    fond.map[9, 2] = 69;
                    fond.map[9, 3] = 69;
                    fond.map[9, 4] = 69;
                    fond.map[9, 5] = 51;
                    fond.map[9, 6] = 53;
                    fond.map[9, 7] = 52;
                    fond.map[9, 8] = 53;
                    fond.map[9, 9] = 52;
                    fond.map[9, 10] = 53;
                    fond.map[9, 11] = 52;
                    fond.map[9, 12] = 53;
                    fond.map[9, 13] = 52;
                    fond.map[9, 14] = 54;
                    fond.map[9, 15] = 69;
                    fond.map[9, 16] = 69;
                    fond.map[9, 17] = 69;
                    fond.map[9, 18] = 69;
                    fond.map[9, 19] = 69;
                    #endregion

                    slime[0].estVivant = true;
                    slime[0].position.X = 355;
                    slime[0].position.Y = 201;
                    slime[0].Time = 0;
                    slime[0].vie = 2;
                    for (byte i = 1; i < 10; i++)
                        slime[i].estVivant = false;
                    for (byte i = 0; i < 4; i++)
                        ball[i].estVivant = false;
                }
                #endregion
                #region Dungeon Room 2
                if (carte == 2)
                {
                    #region Ligne 1
                    fond.map[0, 0] = 27;
                    fond.map[0, 1] = 29;
                    fond.map[0, 2] = 28;
                    fond.map[0, 3] = 29;
                    fond.map[0, 4] = 28;
                    fond.map[0, 5] = 29;
                    fond.map[0, 6] = 28;
                    fond.map[0, 7] = 29;
                    fond.map[0, 8] = 30;
                    fond.map[0, 9] = 69;
                    fond.map[0, 10] = 69;
                    fond.map[0, 11] = 27;
                    fond.map[0, 12] = 29;
                    fond.map[0, 13] = 28;
                    fond.map[0, 14] = 29;
                    fond.map[0, 15] = 28;
                    fond.map[0, 16] = 29;
                    fond.map[0, 17] = 28;
                    fond.map[0, 18] = 29;
                    fond.map[0, 19] = 30;
                    #endregion
                    #region Ligne 2
                    fond.map[1, 0] = 42;
                    fond.map[1, 1] = 31;
                    fond.map[1, 2] = 31;
                    fond.map[1, 3] = 31;
                    fond.map[1, 4] = 31;
                    fond.map[1, 5] = 31;
                    fond.map[1, 6] = 31;
                    fond.map[1, 7] = 31;
                    fond.map[1, 8] = 45;
                    fond.map[1, 9] = 69;
                    fond.map[1, 10] = 69;
                    fond.map[1, 11] = 42;
                    fond.map[1, 12] = 31;
                    fond.map[1, 13] = 31;
                    fond.map[1, 14] = 31;
                    fond.map[1, 15] = 31;
                    fond.map[1, 16] = 31;
                    fond.map[1, 17] = 31;
                    fond.map[1, 18] = 31;
                    fond.map[1, 19] = 45;
                    #endregion
                    #region Ligne 3
                    fond.map[2, 0] = 33;
                    fond.map[2, 1] = 31;
                    fond.map[2, 2] = 64;
                    fond.map[2, 3] = 61;
                    fond.map[2, 4] = 31;
                    fond.map[2, 5] = 61;
                    fond.map[2, 6] = 64;
                    fond.map[2, 7] = 31;
                    fond.map[2, 8] = 36;
                    fond.map[2, 9] = 69;
                    fond.map[2, 10] = 69;
                    fond.map[2, 11] = 33;
                    fond.map[2, 12] = 31;
                    fond.map[2, 13] = 64;
                    fond.map[2, 14] = 61;
                    fond.map[2, 15] = 31;
                    fond.map[2, 16] = 61;
                    fond.map[2, 17] = 64;
                    fond.map[2, 18] = 31;
                    fond.map[2, 19] = 36;
                    #endregion
                    #region Ligne 4
                    fond.map[3, 0] = 48;
                    fond.map[3, 1] = 31;
                    fond.map[3, 2] = 47;
                    fond.map[3, 3] = 31;
                    fond.map[3, 4] = 31;
                    fond.map[3, 5] = 31;
                    fond.map[3, 6] = 47;
                    fond.map[3, 7] = 31;
                    fond.map[3, 8] = 45;
                    fond.map[3, 9] = 69;
                    fond.map[3, 10] = 69;
                    fond.map[3, 11] = 42;
                    fond.map[3, 12] = 31;
                    fond.map[3, 13] = 47;
                    fond.map[3, 14] = 31;
                    fond.map[3, 15] = 31;
                    fond.map[3, 16] = 31;
                    fond.map[3, 17] = 47;
                    fond.map[3, 18] = 31;
                    fond.map[3, 19] = 57;
                    #endregion
                    #region Ligne 5
                    fond.map[4, 0] = 33;
                    fond.map[4, 1] = 31;
                    fond.map[4, 2] = 56;
                    fond.map[4, 3] = 31;
                    fond.map[4, 4] = 55;
                    fond.map[4, 5] = 31;
                    fond.map[4, 6] = 56;
                    fond.map[4, 7] = 31;
                    fond.map[4, 8] = 36;
                    fond.map[4, 9] = 69;
                    fond.map[4, 10] = 69;
                    fond.map[4, 11] = 33;
                    fond.map[4, 12] = 31;
                    fond.map[4, 13] = 56;
                    fond.map[4, 14] = 31;
                    fond.map[4, 15] = 55;
                    fond.map[4, 16] = 31;
                    fond.map[4, 17] = 56;
                    fond.map[4, 18] = 31;
                    fond.map[4, 19] = 36;
                    #endregion
                    #region Ligne 6
                    fond.map[5, 0] = 42;
                    fond.map[5, 1] = 31;
                    fond.map[5, 2] = 47;
                    fond.map[5, 3] = 31;
                    fond.map[5, 4] = 55;
                    fond.map[5, 5] = 31;
                    fond.map[5, 6] = 47;
                    fond.map[5, 7] = 31;
                    fond.map[5, 8] = 45;
                    fond.map[5, 9] = 69;
                    fond.map[5, 10] = 69;
                    fond.map[5, 11] = 42;
                    fond.map[5, 12] = 31;
                    fond.map[5, 13] = 47;
                    fond.map[5, 14] = 31;
                    fond.map[5, 15] = 55;
                    fond.map[5, 16] = 31;
                    fond.map[5, 17] = 47;
                    fond.map[5, 18] = 31;
                    fond.map[5, 19] = 45;
                    #endregion
                    #region Ligne 7
                    fond.map[6, 0] = 33;
                    fond.map[6, 1] = 31;
                    fond.map[6, 2] = 56;
                    fond.map[6, 3] = 31;
                    fond.map[6, 4] = 31;
                    fond.map[6, 5] = 31;
                    fond.map[6, 6] = 56;
                    fond.map[6, 7] = 31;
                    fond.map[6, 8] = 36;
                    fond.map[6, 9] = 69;
                    fond.map[6, 10] = 69;
                    fond.map[6, 11] = 33;
                    fond.map[6, 12] = 31;
                    fond.map[6, 13] = 56;
                    fond.map[6, 14] = 31;
                    fond.map[6, 15] = 31;
                    fond.map[6, 16] = 31;
                    fond.map[6, 17] = 56;
                    fond.map[6, 18] = 31;
                    fond.map[6, 19] = 36;
                    #endregion
                    #region Ligne 8
                    fond.map[7, 0] = 42;
                    fond.map[7, 1] = 31;
                    fond.map[7, 2] = 64;
                    fond.map[7, 3] = 61;
                    fond.map[7, 4] = 62;
                    fond.map[7, 5] = 61;
                    fond.map[7, 6] = 64;
                    fond.map[7, 7] = 31;
                    fond.map[7, 8] = 45;
                    fond.map[7, 9] = 69;
                    fond.map[7, 10] = 69;
                    fond.map[7, 11] = 42;
                    fond.map[7, 12] = 55;
                    fond.map[7, 13] = 64;
                    fond.map[7, 14] = 61;
                    fond.map[7, 15] = 62;
                    fond.map[7, 16] = 61;
                    fond.map[7, 17] = 64;
                    fond.map[7, 18] = 55;
                    fond.map[7, 19] = 45;
                    #endregion
                    #region Ligne 9
                    fond.map[8, 0] = 33;
                    fond.map[8, 1] = 31;
                    fond.map[8, 2] = 31;
                    fond.map[8, 3] = 31;
                    fond.map[8, 4] = 31;
                    fond.map[8, 5] = 31;
                    fond.map[8, 6] = 31;
                    fond.map[8, 7] = 31;
                    fond.map[8, 8] = 36;
                    fond.map[8, 9] = 69;
                    fond.map[8, 10] = 69;
                    fond.map[8, 11] = 33;
                    fond.map[8, 12] = 31;
                    fond.map[8, 13] = 31;
                    fond.map[8, 14] = 31;
                    fond.map[8, 15] = 31;
                    fond.map[8, 16] = 31;
                    fond.map[8, 17] = 31;
                    fond.map[8, 18] = 31;
                    fond.map[8, 19] = 36;
                    #endregion
                    #region Ligne 10
                    fond.map[9, 0] = 51;
                    fond.map[9, 1] = 52;
                    fond.map[9, 2] = 53;
                    fond.map[9, 3] = 52;
                    fond.map[9, 4] = 66;
                    fond.map[9, 5] = 52;
                    fond.map[9, 6] = 53;
                    fond.map[9, 7] = 52;
                    fond.map[9, 8] = 54;
                    fond.map[9, 9] = 69;
                    fond.map[9, 10] = 69;
                    fond.map[9, 11] = 51;
                    fond.map[9, 12] = 52;
                    fond.map[9, 13] = 53;
                    fond.map[9, 14] = 52;
                    fond.map[9, 15] = 66;
                    fond.map[9, 16] = 52;
                    fond.map[9, 17] = 53;
                    fond.map[9, 18] = 52;
                    fond.map[9, 19] = 54;
                    #endregion

                    slime[0].estVivant = true;
                    slime[0].position.X = 905;
                    slime[0].position.Y = 401;
                    slime[0].vie = 2;
                    slime[0].Time = 0;
                    slime[1].estVivant = true;
                    slime[1].position.X = 105;
                    slime[1].position.Y = 401;
                    slime[1].Time = 0;
                    slime[1].vie = 2;
                    slime[2].estVivant = true;
                    slime[2].position.X = 205;
                    slime[2].position.Y = 51;
                    slime[2].Time = 0;
                    slime[2].vie = 2;
                    for (byte i = 3; i < 10; i++)
                        slime[i].estVivant = false;
                    for (byte i = 0; i < 4; i++)
                        ball[i].estVivant = false;
                }
                #endregion
                #region Dungeon Room 3
                if (carte == 3)
                {
                    #region Ligne 1
                    fond.map[0, 0] = 27;
                    fond.map[0, 1] = 29;
                    fond.map[0, 2] = 28;
                    fond.map[0, 3] = 29;
                    fond.map[0, 4] = 28;
                    fond.map[0, 5] = 29;
                    fond.map[0, 6] = 28;
                    fond.map[0, 7] = 29;
                    fond.map[0, 8] = 28;
                    fond.map[0, 9] = 29;
                    fond.map[0, 10] = 28;
                    fond.map[0, 11] = 29;
                    fond.map[0, 12] = 28;
                    fond.map[0, 13] = 29;
                    fond.map[0, 14] = 28;
                    fond.map[0, 15] = 29;
                    fond.map[0, 16] = 28;
                    fond.map[0, 17] = 29;
                    fond.map[0, 18] = 28;
                    fond.map[0, 19] = 30;
                    #endregion
                    #region Ligne 2
                    fond.map[1, 0] = 49;
                    fond.map[1, 1] = 31;
                    fond.map[1, 2] = 31;
                    fond.map[1, 3] = 31;
                    fond.map[1, 4] = 31;
                    fond.map[1, 5] = 31;
                    fond.map[1, 6] = 31;
                    fond.map[1, 7] = 31;
                    fond.map[1, 8] = 31;
                    fond.map[1, 9] = 31;
                    fond.map[1, 10] = 31;
                    fond.map[1, 11] = 31;
                    fond.map[1, 12] = 31;
                    fond.map[1, 13] = 31;
                    fond.map[1, 14] = 31;
                    fond.map[1, 15] = 31;
                    fond.map[1, 16] = 31;
                    fond.map[1, 17] = 31;
                    fond.map[1, 18] = 31;
                    fond.map[1, 19] = 58;
                    #endregion
                    #region Ligne 3
                    fond.map[2, 0] = 33;
                    fond.map[2, 1] = 31;
                    fond.map[2, 2] = 34;
                    fond.map[2, 3] = 52;
                    fond.map[2, 4] = 53;
                    fond.map[2, 5] = 52;
                    fond.map[2, 6] = 35;
                    fond.map[2, 7] = 31;
                    fond.map[2, 8] = 34;
                    fond.map[2, 9] = 52;
                    fond.map[2, 10] = 53;
                    fond.map[2, 11] = 35;
                    fond.map[2, 12] = 31;
                    fond.map[2, 13] = 34;
                    fond.map[2, 14] = 53;
                    fond.map[2, 15] = 52;
                    fond.map[2, 16] = 53;
                    fond.map[2, 17] = 35;
                    fond.map[2, 18] = 31;
                    fond.map[2, 19] = 36;
                    #endregion
                    #region Ligne 4
                    fond.map[3, 0] = 42;
                    fond.map[3, 1] = 31;
                    fond.map[3, 2] = 45;
                    fond.map[3, 3] = 69;
                    fond.map[3, 4] = 69;
                    fond.map[3, 5] = 69;
                    fond.map[3, 6] = 51;
                    fond.map[3, 7] = 52;
                    fond.map[3, 8] = 54;
                    fond.map[3, 9] = 69;
                    fond.map[3, 10] = 69;
                    fond.map[3, 11] = 51;
                    fond.map[3, 12] = 52;
                    fond.map[3, 13] = 54;
                    fond.map[3, 14] = 69;
                    fond.map[3, 15] = 69;
                    fond.map[3, 16] = 69;
                    fond.map[3, 17] = 42;
                    fond.map[3, 18] = 31;
                    fond.map[3, 19] = 57;
                    #endregion
                    #region Ligne 5
                    fond.map[4, 0] = 33;
                    fond.map[4, 1] = 31;
                    fond.map[4, 2] = 36;
                    fond.map[4, 3] = 69;
                    fond.map[4, 4] = 69;
                    fond.map[4, 5] = 69;
                    fond.map[4, 6] = 69;
                    fond.map[4, 7] = 69;
                    fond.map[4, 8] = 69;
                    fond.map[4, 9] = 69;
                    fond.map[4, 10] = 69;
                    fond.map[4, 11] = 69;
                    fond.map[4, 12] = 69;
                    fond.map[4, 13] = 69;
                    fond.map[4, 14] = 69;
                    fond.map[4, 15] = 69;
                    fond.map[4, 16] = 69;
                    fond.map[4, 17] = 33;
                    fond.map[4, 18] = 31;
                    fond.map[4, 19] = 36;
                    #endregion
                    #region Ligne 6
                    fond.map[5, 0] = 42;
                    fond.map[5, 1] = 31;
                    fond.map[5, 2] = 45;
                    fond.map[5, 3] = 69;
                    fond.map[5, 4] = 69;
                    fond.map[5, 5] = 69;
                    fond.map[5, 6] = 69;
                    fond.map[5, 7] = 69;
                    fond.map[5, 8] = 69;
                    fond.map[5, 9] = 69;
                    fond.map[5, 10] = 69;
                    fond.map[5, 11] = 69;
                    fond.map[5, 12] = 69;
                    fond.map[5, 13] = 69;
                    fond.map[5, 14] = 69;
                    fond.map[5, 15] = 69;
                    fond.map[5, 16] = 69;
                    fond.map[5, 17] = 42;
                    fond.map[5, 18] = 55;
                    fond.map[5, 19] = 45;
                    #endregion
                    #region Ligne 7
                    fond.map[6, 0] = 33;
                    fond.map[6, 1] = 31;
                    fond.map[6, 2] = 36;
                    fond.map[6, 3] = 69;
                    fond.map[6, 4] = 69;
                    fond.map[6, 5] = 69;
                    fond.map[6, 6] = 69;
                    fond.map[6, 7] = 69;
                    fond.map[6, 8] = 69;
                    fond.map[6, 9] = 69;
                    fond.map[6, 10] = 69;
                    fond.map[6, 11] = 69;
                    fond.map[6, 12] = 69;
                    fond.map[6, 13] = 69;
                    fond.map[6, 14] = 69;
                    fond.map[6, 15] = 69;
                    fond.map[6, 16] = 69;
                    fond.map[6, 17] = 33;
                    fond.map[6, 18] = 31;
                    fond.map[6, 19] = 36;
                    #endregion
                    #region Ligne 8
                    fond.map[7, 0] = 42;
                    fond.map[7, 1] = 31;
                    fond.map[7, 2] = 45;
                    fond.map[7, 3] = 69;
                    fond.map[7, 4] = 69;
                    fond.map[7, 5] = 69;
                    fond.map[7, 6] = 69;
                    fond.map[7, 7] = 69;
                    fond.map[7, 8] = 69;
                    fond.map[7, 9] = 69;
                    fond.map[7, 10] = 69;
                    fond.map[7, 11] = 69;
                    fond.map[7, 12] = 69;
                    fond.map[7, 13] = 69;
                    fond.map[7, 14] = 69;
                    fond.map[7, 15] = 69;
                    fond.map[7, 16] = 69;
                    fond.map[7, 17] = 42;
                    fond.map[7, 18] = 31;
                    fond.map[7, 19] = 45;
                    #endregion
                    #region Ligne 9
                    fond.map[8, 0] = 33;
                    fond.map[8, 1] = 31;
                    fond.map[8, 2] = 36;
                    fond.map[8, 3] = 69;
                    fond.map[8, 4] = 69;
                    fond.map[8, 5] = 69;
                    fond.map[8, 6] = 69;
                    fond.map[8, 7] = 69;
                    fond.map[8, 8] = 69;
                    fond.map[8, 9] = 69;
                    fond.map[8, 10] = 69;
                    fond.map[8, 11] = 69;
                    fond.map[8, 12] = 69;
                    fond.map[8, 13] = 69;
                    fond.map[8, 14] = 69;
                    fond.map[8, 15] = 69;
                    fond.map[8, 16] = 69;
                    fond.map[8, 17] = 33;
                    fond.map[8, 18] = 31;
                    fond.map[8, 19] = 36;
                    #endregion
                    #region Ligne 10
                    fond.map[9, 0] = 51;
                    fond.map[9, 1] = 66;
                    fond.map[9, 2] = 54;
                    fond.map[9, 3] = 69;
                    fond.map[9, 4] = 69;
                    fond.map[9, 5] = 69;
                    fond.map[9, 6] = 69;
                    fond.map[9, 7] = 69;
                    fond.map[9, 8] = 69;
                    fond.map[9, 9] = 69;
                    fond.map[9, 10] = 69;
                    fond.map[9, 11] = 69;
                    fond.map[9, 12] = 69;
                    fond.map[9, 13] = 69;
                    fond.map[9, 14] = 69;
                    fond.map[9, 15] = 69;
                    fond.map[9, 16] = 69;
                    fond.map[9, 17] = 51;
                    fond.map[9, 18] = 66;
                    fond.map[9, 19] = 54;
                    #endregion
                    slime[0].estVivant = true;
                    slime[0].position.X = 505;
                    slime[0].position.Y = 51;
                    slime[0].vie = 2;
                    slime[0].Time = 0;
                    for (byte i = 1; i < 10; i++)
                        slime[i].estVivant = false;
                    ball[0].estVivant = true;
                    ball[0].position.X = 405;
                    ball[0].position.Y = 51;
                    ball[0].vitesse.X = -2;
                    ball[0].vitesse.Y = 0;
                    ball[1].estVivant = true;
                    ball[1].position.X = 805;
                    ball[1].position.Y = 51;
                    ball[1].vitesse.X = -2;
                    ball[1].vitesse.Y = 0;
                    for (byte i = 2; i < 4; i++)
                        ball[i].estVivant = false;
                }
                #endregion
                #region Dungeon Room 4
                if (carte == 4)
                {
                    #region Ligne 1
                    fond.map[0, 0] = 27;
                    fond.map[0, 1] = 39;
                    fond.map[0, 2] = 30;
                    fond.map[0, 3] = 27;
                    fond.map[0, 4] = 28;
                    fond.map[0, 5] = 29;
                    fond.map[0, 6] = 28;
                    fond.map[0, 7] = 29;
                    fond.map[0, 8] = 28;
                    fond.map[0, 9] = 29;
                    fond.map[0, 10] = 28;
                    fond.map[0, 11] = 29;
                    fond.map[0, 12] = 28;
                    fond.map[0, 13] = 29;
                    fond.map[0, 14] = 28;
                    fond.map[0, 15] = 29;
                    fond.map[0, 16] = 30;
                    fond.map[0, 17] = 27;
                    fond.map[0, 18] = 39;
                    fond.map[0, 19] = 30;
                    #endregion
                    #region Ligne 2
                    fond.map[1, 0] = 42;
                    fond.map[1, 1] = 31;
                    fond.map[1, 2] = 36;
                    fond.map[1, 3] = 33;
                    fond.map[1, 4] = 31;
                    fond.map[1, 5] = 31;
                    fond.map[1, 6] = 31;
                    fond.map[1, 7] = 31;
                    fond.map[1, 8] = 31;
                    fond.map[1, 9] = 31;
                    fond.map[1, 10] = 31;
                    fond.map[1, 11] = 31;
                    fond.map[1, 12] = 31;
                    fond.map[1, 13] = 31;
                    fond.map[1, 14] = 31;
                    fond.map[1, 15] = 31;
                    fond.map[1, 16] = 36;
                    fond.map[1, 17] = 33;
                    fond.map[1, 18] = 31;
                    fond.map[1, 19] = 45;
                    #endregion
                    #region Ligne 3
                    fond.map[2, 0] = 33;
                    fond.map[2, 1] = 31;
                    fond.map[2, 2] = 45;
                    fond.map[2, 3] = 42;
                    fond.map[2, 4] = 31;
                    fond.map[2, 5] = 47;
                    fond.map[2, 6] = 31;
                    fond.map[2, 7] = 31;
                    fond.map[2, 8] = 31;
                    fond.map[2, 9] = 31;
                    fond.map[2, 10] = 31;
                    fond.map[2, 11] = 31;
                    fond.map[2, 12] = 31;
                    fond.map[2, 13] = 31;
                    fond.map[2, 14] = 31;
                    fond.map[2, 15] = 31;
                    fond.map[2, 16] = 45;
                    fond.map[2, 17] = 42;
                    fond.map[2, 18] = 31;
                    fond.map[2, 19] = 36;
                    #endregion
                    #region Ligne 4
                    fond.map[3, 0] = 42;
                    fond.map[3, 1] = 31;
                    fond.map[3, 2] = 36;
                    fond.map[3, 3] = 33;
                    fond.map[3, 4] = 31;
                    fond.map[3, 5] = 47;
                    fond.map[3, 6] = 31;
                    fond.map[3, 7] = 31;
                    fond.map[3, 8] = 31;
                    fond.map[3, 9] = 31;
                    fond.map[3, 10] = 31;
                    fond.map[3, 11] = 31;
                    fond.map[3, 12] = 31;
                    fond.map[3, 13] = 31;
                    fond.map[3, 14] = 31;
                    fond.map[3, 15] = 31;
                    fond.map[3, 16] = 36;
                    fond.map[3, 17] = 33;
                    fond.map[3, 18] = 31;
                    fond.map[3, 19] = 45;
                    #endregion
                    #region Ligne 5
                    fond.map[4, 0] = 33;
                    fond.map[4, 1] = 31;
                    fond.map[4, 2] = 45;
                    fond.map[4, 3] = 42;
                    fond.map[4, 4] = 31;
                    fond.map[4, 5] = 56;
                    fond.map[4, 6] = 31;
                    fond.map[4, 7] = 31;
                    fond.map[4, 8] = 31;
                    fond.map[4, 9] = 31;
                    fond.map[4, 10] = 31;
                    fond.map[4, 11] = 31;
                    fond.map[4, 12] = 31;
                    fond.map[4, 13] = 31;
                    fond.map[4, 14] = 31;
                    fond.map[4, 15] = 31;
                    fond.map[4, 16] = 36;
                    fond.map[4, 17] = 33;
                    fond.map[4, 18] = 31;
                    fond.map[4, 19] = 36;
                    #endregion
                    #region Ligne 6
                    fond.map[5, 0] = 42;
                    fond.map[5, 1] = 31;
                    fond.map[5, 2] = 36;
                    fond.map[5, 3] = 33;
                    fond.map[5, 4] = 31;
                    fond.map[5, 5] = 47;
                    fond.map[5, 6] = 31;
                    fond.map[5, 7] = 31;
                    fond.map[5, 8] = 31;
                    fond.map[5, 9] = 31;
                    fond.map[5, 10] = 31;
                    fond.map[5, 11] = 31;
                    fond.map[5, 12] = 31;
                    fond.map[5, 13] = 31;
                    fond.map[5, 14] = 31;
                    fond.map[5, 15] = 31;
                    fond.map[5, 16] = 36;
                    fond.map[5, 17] = 33;
                    fond.map[5, 18] = 31;
                    fond.map[5, 19] = 45;
                    #endregion
                    #region Ligne 7
                    fond.map[6, 0] = 33;
                    fond.map[6, 1] = 31;
                    fond.map[6, 2] = 45;
                    fond.map[6, 3] = 42;
                    fond.map[6, 4] = 31;
                    fond.map[6, 5] = 56;
                    fond.map[6, 6] = 31;
                    fond.map[6, 7] = 31;
                    fond.map[6, 8] = 31;
                    fond.map[6, 9] = 31;
                    fond.map[6, 10] = 31;
                    fond.map[6, 11] = 31;
                    fond.map[6, 12] = 31;
                    fond.map[6, 13] = 31;
                    fond.map[6, 14] = 31;
                    fond.map[6, 15] = 31;
                    fond.map[6, 16] = 45;
                    fond.map[6, 17] = 42;
                    fond.map[6, 18] = 31;
                    fond.map[6, 19] = 36;
                    #endregion
                    #region Ligne 8
                    fond.map[7, 0] = 42;
                    fond.map[7, 1] = 31;
                    fond.map[7, 2] = 43;
                    fond.map[7, 3] = 44;
                    fond.map[7, 4] = 31;
                    fond.map[7, 5] = 64;
                    fond.map[7, 6] = 61;
                    fond.map[7, 7] = 62;
                    fond.map[7, 8] = 61;
                    fond.map[7, 9] = 62;
                    fond.map[7, 10] = 61;
                    fond.map[7, 11] = 62;
                    fond.map[7, 12] = 61;
                    fond.map[7, 13] = 62;
                    fond.map[7, 14] = 61;
                    fond.map[7, 15] = 62;
                    fond.map[7, 16] = 43;
                    fond.map[7, 17] = 44;
                    fond.map[7, 18] = 31;
                    fond.map[7, 19] = 45;
                    #endregion
                    #region Ligne 9
                    fond.map[8, 0] = 33;
                    fond.map[8, 1] = 31;
                    fond.map[8, 2] = 31;
                    fond.map[8, 3] = 31;
                    fond.map[8, 4] = 31;
                    fond.map[8, 5] = 31;
                    fond.map[8, 6] = 31;
                    fond.map[8, 7] = 31;
                    fond.map[8, 8] = 31;
                    fond.map[8, 9] = 31;
                    fond.map[8, 10] = 31;
                    fond.map[8, 11] = 31;
                    fond.map[8, 12] = 31;
                    fond.map[8, 13] = 31;
                    fond.map[8, 14] = 31;
                    fond.map[8, 15] = 31;
                    fond.map[8, 16] = 55;
                    fond.map[8, 17] = 55;
                    fond.map[8, 18] = 31;
                    fond.map[8, 19] = 36;
                    #endregion
                    #region Ligne 10
                    fond.map[9, 0] = 51;
                    fond.map[9, 1] = 52;
                    fond.map[9, 2] = 53;
                    fond.map[9, 3] = 52;
                    fond.map[9, 4] = 53;
                    fond.map[9, 5] = 52;
                    fond.map[9, 6] = 53;
                    fond.map[9, 7] = 52;
                    fond.map[9, 8] = 53;
                    fond.map[9, 9] = 52;
                    fond.map[9, 10] = 53;
                    fond.map[9, 11] = 52;
                    fond.map[9, 12] = 53;
                    fond.map[9, 13] = 52;
                    fond.map[9, 14] = 53;
                    fond.map[9, 15] = 52;
                    fond.map[9, 16] = 53;
                    fond.map[9, 17] = 52;
                    fond.map[9, 18] = 53;
                    fond.map[9, 19] = 54;
                    #endregion
                    slime[0].estVivant = true;
                    slime[0].position.X = 505;
                    slime[0].position.Y = 51;
                    slime[0].vie = 2;
                    slime[0].Time = 0;
                    slime[1].estVivant = true;
                    slime[1].position.X = 405;
                    slime[1].position.Y = 151;
                    slime[1].vie = 2;
                    slime[1].Time = 0;
                    slime[2].estVivant = true;
                    slime[2].position.X = 705;
                    slime[2].position.Y = 251;
                    slime[2].vie = 2;
                    slime[2].Time = 0;
                    slime[3].estVivant = true;
                    slime[3].position.X = 305;
                    slime[3].position.Y = 301;
                    slime[3].vie = 2;
                    slime[3].Time = 0;
                    slime[4].estVivant = true;
                    slime[4].position.X = 705;
                    slime[4].position.Y = 51;
                    slime[4].vie = 2;
                    slime[4].Time = 0;
                    for (byte i = 0; i < 4; i++)
                        ball[i].estVivant = false;
                }
                #endregion
                time--;
                if (time == 0)
                    state = "Game";
            }
            else if (state == "Game")
            {
                if (passage)
                {
                    if (carte == 2)
                    {
                        fond.map[7, 12] = 46;
                        fond.map[7, 18] = 46;
                    }
                    else if (carte == 3)
                        fond.map[5, 18] = 46;
                    else if (carte == 4)
                    {
                        fond.map[8, 16] = 46;
                        fond.map[8, 17] = 46;
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (state == "Game" || state == "Load")
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                #region Terrain (Back)
                for (int i = 0; i < GameObjectTile.COLONNE; i++)
                {
                    fond.rectSource.Y = (i * 50);
                    for (int j = 0; j < GameObjectTile.LIGNE; j++)
                    {
                        fond.rectSource.X = (j * 50);
                        #region Outworld Tiles (1-14) & Dungeon Outside (15-26)
                        if (fond.map[i, j] == 1)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile01, Color.White);
                        if (fond.map[i, j] == 2)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile02, Color.White);
                        if (fond.map[i, j] == 3)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile03, Color.White);
                        if (fond.map[i, j] == 4)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile04, Color.White);
                        if (fond.map[i, j] == 5)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile05, Color.White);
                        if (fond.map[i, j] == 6)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile06, Color.White);
                        if (fond.map[i, j] == 7)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile07, Color.White);
                        if (fond.map[i, j] == 8)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile08, Color.White);
                        if (fond.map[i, j] == 9)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile09, Color.White);
                        if (fond.map[i, j] == 10)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile10, Color.White);
                        if (fond.map[i, j] == 11)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile11, Color.White);
                        if (fond.map[i, j] == 12)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile12, Color.White);
                        if (fond.map[i, j] == 13)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile13, Color.White);
                        if (fond.map[i, j] == 14)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile14, Color.White);
                        if (fond.map[i, j] == 15)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile15, Color.White);
                        if (fond.map[i, j] == 16)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile16, Color.White);
                        if (fond.map[i, j] == 17)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile17, Color.White);
                        if (fond.map[i, j] == 18)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile18, Color.White);
                        if (fond.map[i, j] == 19)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile19, Color.White);
                        if (fond.map[i, j] == 20)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile20, Color.White);
                        if (fond.map[i, j] == 21)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile21, Color.White);
                        if (fond.map[i, j] == 22)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile22, Color.White);
                        if (fond.map[i, j] == 23)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile23, Color.White);
                        if (fond.map[i, j] == 24)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile24, Color.White);
                        if (fond.map[i, j] == 25)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile25, Color.White);
                        if (fond.map[i, j] == 26)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile26, Color.White);
                        #endregion
                        #region Dungeon Tiles (27-67 except 41/50/59/68)
                        if (fond.map[i, j] == 27)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile27, Color.White);
                        if (fond.map[i, j] == 28)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile28, Color.White);
                        if (fond.map[i, j] == 29)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile29, Color.White);
                        if (fond.map[i, j] == 30)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile30, Color.White);
                        if (fond.map[i, j] == 31)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile31, Color.White);
                        if (fond.map[i, j] == 32)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile32, Color.White);
                        if (fond.map[i, j] == 33)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile33, Color.White);
                        if (fond.map[i, j] == 34)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile34, Color.White);
                        if (fond.map[i, j] == 35)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile35, Color.White);
                        if (fond.map[i, j] == 36)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile36, Color.White);
                        if (fond.map[i, j] == 37)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile37, Color.White);
                        if (fond.map[i, j] == 38)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile38, Color.White);
                        if (fond.map[i, j] == 39)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile39, Color.White);
                        if (fond.map[i, j] == 40)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile40, Color.White);
                        if (fond.map[i, j] == 42)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile42, Color.White);
                        if (fond.map[i, j] == 43)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile43, Color.White);
                        if (fond.map[i, j] == 44)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile44, Color.White);
                        if (fond.map[i, j] == 45)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile45, Color.White);
                        if (fond.map[i, j] == 46)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile46, Color.White);
                        if (fond.map[i, j] == 47)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile47, Color.White);
                        if (fond.map[i, j] == 48)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile48, Color.White);
                        if (fond.map[i, j] == 49)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile49, Color.White);
                        if (fond.map[i, j] == 51)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile51, Color.White);
                        if (fond.map[i, j] == 52)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile52, Color.White);
                        if (fond.map[i, j] == 53)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile53, Color.White);
                        if (fond.map[i, j] == 54)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile54, Color.White);
                        if (fond.map[i, j] == 55)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile55, Color.White);
                        if (fond.map[i, j] == 56)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile56, Color.White);
                        if (fond.map[i, j] == 57)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile57, Color.White);
                        if (fond.map[i, j] == 58)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile58, Color.White);
                        if (fond.map[i, j] == 60)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile60, Color.White);
                        if (fond.map[i, j] == 61)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile61, Color.White);
                        if (fond.map[i, j] == 62)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile62, Color.White);
                        if (fond.map[i, j] == 63)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile63, Color.White);
                        if (fond.map[i, j] == 64)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile64, Color.White);
                        if (fond.map[i, j] == 65)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile65, Color.White);
                        if (fond.map[i, j] == 66)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile66, Color.White);
                        if (fond.map[i, j] == 67)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile67, Color.White);
                        if (fond.map[i, j] == 69) // Void Tile
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile69, Color.White);
                        #endregion
                    }
                }
                #endregion
                if (key.estVivant)
                    spriteBatch.Draw(key.sprite, key.position, Color.White);
                #region Player
                if (ruby.time <= 0)
                    spriteBatch.Draw(ruby.sprite, ruby.position, ruby.spriteAfficher, Color.White);
                else
                    spriteBatch.Draw(ruby.sprite, ruby.position, ruby.spriteAfficher, Color.Red);
                if (projectile.estVivant)
                    spriteBatch.Draw(projectile.sprite, projectile.position, projectile.spriteAfficher, Color.White);
                if (sword.estVivant)
                    spriteBatch.Draw(sword.sprite, sword.position, sword.spriteAfficher, Color.White);
                #endregion
                #region Ennemi
                #region Slime
                for (byte i = 0; i < 10; i++)
                {
                    if (slime[i].estVivant == true)
                        spriteBatch.Draw(slime[i].sprite, slime[i].position, slime[i].spriteAfficher, Color.White);
                }
                #endregion
                #region Ball
                for (byte i = 0; i < 4; i++)
                {
                    if (ball[i].estVivant == true)
                        spriteBatch.Draw(ball[i].sprite, ball[i].position, ball[i].spriteAfficher, Color.White);
                }
                #endregion
                #endregion
                #region Life Gauge (DO NOT TOUCH)
                spriteBatch.Draw(vie.sprite, vie.rectHp, vie.spriteAfficher[0], Color.White);
                for (byte i = 1; i < vie.vie[1] + 1; i++)
                {
                    vie.rectSource.X = 1053 + (22 * i);
                    spriteBatch.Draw(vie.sprite, vie.rectSource, vie.spriteAfficher[i], Color.White);
                }
                #endregion
                #region Terrain (Front)
                for (int i = 0; i < GameObjectTile.COLONNE; i++)
                {
                    fond.rectSource.Y = (i * 50);
                    for (int j = 0; j < GameObjectTile.LIGNE; j++)
                    {
                        fond.rectSource.X = (j * 50);
                        #region Dungeon Tiles (41-50-59-68)
                        if (fond.map[i, j] == 40)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile41, Color.White);
                        if (fond.map[i, j] == 49)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile50, Color.White);
                        if (fond.map[i, j] == 58)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile59, Color.White);
                        if (fond.map[i, j] == 67)
                            spriteBatch.Draw(fond.texture, fond.rectSource, fond.tile68, Color.White);
                        #endregion
                    }
                }
                #endregion
                spriteBatch.End();
            }
            else
                GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
