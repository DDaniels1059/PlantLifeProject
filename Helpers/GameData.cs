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

        public static Plant[,] Map = new Plant[10,10];
        public static SpriteFont GameFont;


        public static Dictionary<string, Texture2D> Textures;
        public static void LoadTextures(ContentManager content)
        {
            Textures = new Dictionary<string, Texture2D>();
            Textures["Misc/Hover"] = content.Load<Texture2D>("Misc/hover");
            Textures["Plants/Sunflower"] = content.Load<Texture2D>("Plants/sunflower");
            Textures["Plants/Ground"] = content.Load<Texture2D>("Plants/ground");

            GameFont = content.Load<SpriteFont>("Misc/gameFont");

        }

        public static Dictionary<string, SpriteAnimation> AnimTextures;
        public static void LoadAnimTextures()
        {
            AnimTextures = new Dictionary<string, SpriteAnimation>();
            AnimTextures["Plants/Sunflower"] = new SpriteAnimation(Textures["Plants/Sunflower"], 6, 0);
        }
    }
}
