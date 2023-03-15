using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoiseGenProject.Helpers;
using Plantlife.Helpers;
using Plantlife.Plants;
using System;
using System.Diagnostics;

namespace Plantlife
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera camera;
        private Helper help = new();

        private SoundEffect buttonplop;
        private bool isFilledSlot;
        private float isFilledTimer;

        private Vector2 PlayerPos = new(50, 50);
        private Vector2 mouseHoverPos;
        private KeyboardState kStateOld = Keyboard.GetState();
        private MouseState mStateOld = Mouse.GetState();

        private float TimePassed;
        private int CurrentDay = 1;
        private int lastDay = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.camera = new(_graphics.GraphicsDevice)
            {
                Zoom = 4f
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new(GraphicsDevice);
            GameData.LoadTextures(Content);
            GameData.LoadAnimTextures();
            WorldGrid.LoadWorldContent();

            buttonplop = Content.Load<SoundEffect>("Audio/buttonplop");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive) 
            {
                MouseState mState = Mouse.GetState();
                KeyboardState kState = Keyboard.GetState();
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(TimePassed >= 500)
                {
                    TimePassed = 0;
                    CurrentDay++;
                }
                else
                {
                    TimePassed += dt * 100;
                }

                if (lastDay != CurrentDay)
                {
                    foreach (Plant plant in GameData.Map)
                    {
                        if (plant != null)
                        {
                            plant.Update();
                        }
                    }

                    lastDay = CurrentDay;
                }

                if(isFilledSlot)
                {
                    if(isFilledTimer >= 20)
                    {
                        isFilledTimer = 0;
                        isFilledSlot = false;
                    }
                    else
                    {
                        isFilledTimer += dt * 100;
                    }
                }


                mouseHoverPos = help.GetMousePos(mState, camera);

                if (mState.LeftButton == ButtonState.Pressed && mStateOld.LeftButton == ButtonState.Released)
                {
                    // Get the mouse position
                    Vector2 mousePos = help.GetMousePos(mState, camera);
                    int x = (int)mousePos.X / GameData.TileSize;
                    int y = (int)mousePos.Y / GameData.TileSize;

                    // Since Plants Can Be 32 Tall, and We only want the First "Square 16x16" of the Plant, we Just use 16x16 Here
                    if (x >= 0 && y >= 0 && x < GameData.MapSize && y < GameData.MapSize)
                    {
                        Plant plant = GameData.Map[x, y];
                        Rectangle blockRect = new(x * GameData.TileSize, y * GameData.TileSize, GameData.TileSize, GameData.TileSize);

                        if (plant != null && blockRect.Contains((int)mousePos.X, (int)mousePos.Y))
                        {
                            buttonplop.Play(0.5f, 0f, 0f);
                            isFilledSlot = true;
                        }
                        else if (plant == null && blockRect.Contains((int)mousePos.X, (int)mousePos.Y))
                        {
                            buttonplop.Play(0.5f, 0.5f, 0f);

                            Random random = new Random();
                            int chance = random.Next(0, 100);

                            if(chance < 50)
                            {
                                GameData.Map[x, y] = new Strawberry();
                                Plant newplant = GameData.Map[x, y];
                                GameData.Plants.Add(newplant);
                            }
                            else
                            {
                                GameData.Map[x, y] = new Sunflower();
                                Plant newplant = GameData.Map[x, y];
                                GameData.Plants.Add(newplant);
                            }

                            //Debug.WriteLine("This Can Be Farmed");
                        }
                    }
                }

                if(kState.IsKeyDown(Keys.W)) 
                {
                    PlayerPos.Y -= 225 * dt;
                }
                if (kState.IsKeyDown(Keys.S))
                {
                    PlayerPos.Y += 225 * dt;
                }
                if (kState.IsKeyDown(Keys.A))
                {
                    PlayerPos.X -= 225 * dt;
                }
                if (kState.IsKeyDown(Keys.D))
                {
                    PlayerPos.X += 225 * dt;
                }

                mStateOld = mState;
                kStateOld = kState;

                this.camera.Position = PlayerPos;
                this.camera.Update(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            _spriteBatch.Begin(this.camera, SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

                WorldGrid.DrawWorld(_spriteBatch, help, _graphics, mouseHoverPos, isFilledSlot);

            _spriteBatch.End();


            #region STATIC DISPLAY

            _spriteBatch.Begin();

            //_spriteBatch.DrawString(GameData.GameFont, "Mouse Pos: " + mouseHoverPos.X.ToString() + " " + mouseHoverPos.Y.ToString(), new Vector2(5, 20), Color.White);
            _spriteBatch.DrawString(GameData.GameFont, "Day: " + CurrentDay.ToString(), new Vector2(5, 10), Color.White);
            //_spriteBatch.DrawString(GameData.GameFont, "LastDay: " + lastDay.ToString(), new Vector2(5, 40), Color.White);

            _spriteBatch.End();

            #endregion

            base.Draw(gameTime);
        }
    }
}