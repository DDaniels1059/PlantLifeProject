using Comora;
using Microsoft.Xna.Framework;
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

        private Vector2 PlayerPos = new(50, 50);
        private Vector2 mouseHoverPos;
        private KeyboardState kStateOld = Keyboard.GetState();
        private MouseState mStateOld = Mouse.GetState();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.camera = new(_graphics.GraphicsDevice);
            this.camera.Zoom = 4f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new(GraphicsDevice);
            GameData.LoadTextures(Content);
            GameData.LoadAnimTextures();



            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    GameData.Map[x, y] = null;
                }
            }


            GameData.Map[0, 1] = new Sunflower();
            GameData.Map[2, 1] = new Sunflower();
            GameData.Map[4, 3] = new Sunflower();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(this.IsActive) 
            {
                MouseState mState = Mouse.GetState();
                KeyboardState kState = Keyboard.GetState();
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                mouseHoverPos = help.GetMousePos(mState, camera);

                if (mState.LeftButton == ButtonState.Pressed && mStateOld.LeftButton == ButtonState.Released)
                {
                    // Get the mouse position
                    Vector2 mousePos = help.GetMousePos(mState, camera);
                    int x = (int)mousePos.X / 16;
                    int y = (int)mousePos.Y / 16;


                    if (x >= 0 && y >= 0 && x < 10 && y < 10)
                    {
                        Plant plant = GameData.Map[x, y];

                        Rectangle blockRect = new(x * 16, y * 16, 16, 16);

                        if (plant != null && blockRect.Contains((int)mousePos.X, (int)mousePos.Y))
                        {
                            if(plant.CurrFrame < 5)
                            {
                                plant.CurrFrame += 1;
                            }
                            else
                            {
                                plant.CurrFrame = 0;
                            }

                            plant.TextureAnim.setFrame(plant.CurrFrame);                           
                        }
                        else if (plant == null && blockRect.Contains((int)mousePos.X, (int)mousePos.Y))
                        {
                            GameData.Map[x, y] = new Sunflower();
                            Debug.WriteLine("This Can Be Farmed");
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(this.camera, SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

            //Draw Map
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    // Calculate the position of the block on the screen
                    Plant plant = GameData.Map[x, y];
                    if (plant != null)
                    {
                        Vector2 blockPosition = new(x * 16, y * 16);
                        _spriteBatch.Draw(GameData.Textures["Plants/Ground"], blockPosition, null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0000000f);
                        plant.Draw(_spriteBatch, help.GetDepth(plant.Origin, _graphics), blockPosition);

                    }
                    else
                    {
                        Vector2 blockPosition = new(x * 16, y * 16);
                        _spriteBatch.Draw(GameData.Textures["Plants/Ground"], blockPosition, null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0000000f);
                    }
                }
            }

            //Draw Hover Texture
            Point mouseTile = new((int)(mouseHoverPos.X / 16), (int)(mouseHoverPos.Y / 16));
            if (mouseTile.X >= 0 && mouseTile.X < 10 && mouseTile.Y >= 0 && mouseTile.Y < 10 && mouseHoverPos.X >= 0 && mouseHoverPos.Y >= 0)
            {
                //Plant plant = GameData.Map[mouseTile.X, mouseTile.Y];

                Rectangle plantRect = new(mouseTile.X * 16, mouseTile.Y * 16, 16, 16);
                _spriteBatch.Draw(GameData.Textures["Misc/Hover"], plantRect, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.5f);
            }

            _spriteBatch.End();


            #region STATIC DISPLAY

            _spriteBatch.Begin();

            _spriteBatch.DrawString(GameData.GameFont, "Mouse Pos: " + mouseHoverPos.X.ToString() + " " + mouseHoverPos.Y.ToString(), new Vector2(5, 20), Color.White);
            _spriteBatch.End();

            #endregion

            base.Draw(gameTime);
        }
    }
}