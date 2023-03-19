using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Plantlife.Helpers;
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
        public Vector2 DropPos;
        public bool isHarvestable = false;

        private int xOffset;
        private int yOffset;

        public Plant(SpriteAnimation TextureAnim)
        {
            this.TextureAnim = TextureAnim;
            TextureAnim.setFrame(0);
            CreateRandom();
        }

        public void Draw(SpriteBatch _spriteBatch, float Depth, Vector2 Position)
        {
            TextureAnim.Position = Position - new Vector2(xOffset, (TextureAnim.Texture.Height / 2f) + yOffset);
            DropPos = new Vector2(TextureAnim.Position.X + 4, TextureAnim.Position.Y + 16);
            Origin = new Vector2(Position.X - 16, Position.Y + 32);
            TextureAnim.Draw(_spriteBatch, Depth);
        }


        private void CreateRandom()
        {
            Random rand = new Random();
            xOffset = rand.Next(-1, 1);
            yOffset = rand.Next(-1, 1);
        }

        public virtual void DropItem()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            if (CurrFrame < MaxFrames)
                TextureAnim.setFrame(CurrFrame++);
            else
            {
                TextureAnim.setFrame(MaxFrames);
                isHarvestable = true;
            }
        }
    }
}
