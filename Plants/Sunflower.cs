using Microsoft.Xna.Framework.Graphics;
using NoiseGenProject.Helpers;
using Plantlife.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantlife.Plants
{
    internal class Sunflower : Plant
    {
        public Sunflower() : base(new SpriteAnimation(GameData.Textures["Plants/Sunflower"], 6, 0))
        {

        }
    }
}
