using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoiseGenProject.Helpers;
using Plantlife.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantlife.Helpers
{
    internal class WorldGrid
    {
        // Load The Original Game World
        // TODO: Possibly random generation of some sort

        public static void LoadWorldContent()
        {
            for (int y = 0; y < GameData.MapSize; y++)
            {
                for (int x = 0; x < GameData.MapSize; x++)
                {
                    GameData.Map[x, y] = null;
                }
            }

            //GameData.Map[0, 1] = new Sunflower();
        }

        public static void DrawWorld(SpriteBatch _spriteBatch, Helper help, GraphicsDeviceManager _graphics, Vector2 MouseHoverPos, bool isFilledSlot)
        {
            for (int y = 0; y < GameData.MapSize; y++)
            {
                for (int x = 0; x < GameData.MapSize; x++)
                {
                    // Calculate the position of the block on the screen
                    // Since Plants Can Be 32 Tall, and We only want the First "Square 16x16" of the Plant, we Just use 16x16 Here
                    Plant plant = GameData.Map[x, y];
                    if (plant != null)
                    {
                        Vector2 blockPosition = new(x * GameData.TileSize, y * GameData.TileSize);
                        _spriteBatch.Draw(GameData.Textures["Plants/Ground"], blockPosition, null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0000000f);
                        plant.Draw(_spriteBatch, help.GetDepth(plant.Origin, _graphics), blockPosition);

                    }
                    else
                    {
                        Vector2 blockPosition = new(x * GameData.TileSize, y * GameData.TileSize);
                        _spriteBatch.Draw(GameData.Textures["Plants/Ground"], blockPosition, null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0000000f);
                    }
                }
            }

            //Draw Hover Texture
            Point mouseTile = new((int)(MouseHoverPos.X / GameData.TileSize), (int)(MouseHoverPos.Y / GameData.TileSize));
            if (mouseTile.X >= 0 && mouseTile.X < GameData.MapSize && mouseTile.Y >= 0 && mouseTile.Y < GameData.MapSize && MouseHoverPos.X >= 0 && MouseHoverPos.Y >= 0)
            {
                //Plant plant = GameData.Map[mouseTile.X, mouseTile.Y];
                Rectangle plantRect = new(mouseTile.X * GameData.TileSize, mouseTile.Y * GameData.TileSize, GameData.TileSize, GameData.TileSize);

                if (isFilledSlot)
                {
                    _spriteBatch.Draw(GameData.Textures["Misc/Hover"], plantRect, null, Color.Red, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0000001f);
                }
                else
                {
                    _spriteBatch.Draw(GameData.Textures["Misc/Hover"], plantRect, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0000001f);
                }
            }
        }
    }
}
