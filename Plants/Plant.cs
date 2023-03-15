using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoiseGenProject.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantlife.Plants
{
    internal class Plant
    {
        public SpriteAnimation TextureAnim;
        public int CurrFrame = 0;
        public int MaxFrames;
        public Vector2 Origin;
        public bool isHarvestable = false;

        public Plant(SpriteAnimation TextureAnim)
        {
            this.TextureAnim = TextureAnim;
            TextureAnim.setFrame(0);
        }

        public void Draw(SpriteBatch _spriteBatch, float Depth, Vector2 Position)
        {
            TextureAnim.Position = Position - new Vector2(0, TextureAnim.Texture.Height / 2f);
            Origin = new Vector2(Position.X - 16, Position.Y + 32);
            TextureAnim.Draw(_spriteBatch, Depth);
        }

        public void Update()
        {
            // (condition) ? (expressionIfTrue) : (expressionIfFalse);

            //if (CurrFrame < MaxFrames)
            //    TextureAnim.setFrame(CurrFrame++);
            //else
            //{
            //    TextureAnim.setFrame(MaxFrames);
            //    isHarvestable = true;
            //}

            TextureAnim.setFrame((CurrFrame < MaxFrames) ? CurrFrame++ : (isHarvestable ? MaxFrames : CurrFrame));
        }
    }
}
