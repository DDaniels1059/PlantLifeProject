using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoiseGenProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantlife.Plants
{
    internal class Plant
    {
        public SpriteAnimation TextureAnim;
        public int CurrFrame = 0;
        public Vector2 Origin;

        public Plant(SpriteAnimation TextureAnim)
        {
            this.TextureAnim = TextureAnim;
            TextureAnim.setFrame(0);
        }

        public virtual void Draw(SpriteBatch _spriteBatch, float Depth, Vector2 Position)
        {
            TextureAnim.Position = Position - new Vector2(0, TextureAnim.Texture.Height / 2f);
            Origin = new Vector2(Position.X - 16, Position.Y+ 32);
            TextureAnim.Draw(_spriteBatch, Depth);
        }
    }
}
