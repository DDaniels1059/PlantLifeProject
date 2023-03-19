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
    internal class Strawberry : Plant
    {
        public Strawberry() : base(new SpriteAnimation(GameData.Textures["Plants/Strawberry"], 5, 0))
        {
            MaxFrames = 4;
        }

        public override void DropItem()
        {

        }
    }
}
