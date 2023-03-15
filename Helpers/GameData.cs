using Microsoft.Xna.Framework.Content;
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
    internal class GameData
    {
        public static int MapSize = 5;
        public static int TileSize = 16;
        public static int TileWidth = 16;
        public static int TileHeight = 32;
        public static Plant[,] Map = new Plant[MapSize, MapSize];
        public static SpriteFont GameFont;

        public static List<Plant> Plants = new List<Plant>();

        public static Dictionary<string, Texture2D> Textures;
        public static void LoadTextures(ContentManager content)
        {
            Textures = new Dictionary<string, Texture2D>();
            Textures["Misc/Hover"] = content.Load<Texture2D>("Misc/hover");
            Textures["Plants/Sunflower"] = content.Load<Texture2D>("Plants/sunflower");
            Textures["Plants/Strawberry"] = content.Load<Texture2D>("Plants/strawberry");
            Textures["Plants/Ground"] = content.Load<Texture2D>("Plants/ground");

            GameFont = content.Load<SpriteFont>("Misc/gameFont");

        }

        public static Dictionary<string, SpriteAnimation> AnimTextures;
        public static void LoadAnimTextures()
        {
            AnimTextures = new Dictionary<string, SpriteAnimation>();
            AnimTextures["Plants/Sunflower"] = new SpriteAnimation(Textures["Plants/Sunflower"], 6, 0);
            AnimTextures["Plants/Strawberry"] = new SpriteAnimation(Textures["Plants/Strawberry"], 5, 0);

        }
    }
}
