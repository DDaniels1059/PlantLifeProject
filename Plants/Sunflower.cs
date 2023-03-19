using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            MaxFrames = 5;
        }

        public override void DropItem()
        {

        }
    }
}
