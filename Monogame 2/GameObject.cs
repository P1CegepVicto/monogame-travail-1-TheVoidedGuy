using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_2
{
    class GameObject
    {
        public bool estVivant;
        public bool visible;
        public int Time;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 vitesse;
        
        public Rectangle rectColision = new Rectangle();
        public Rectangle GetRect()
        {
            rectColision.X = (int)this.position.X;
            rectColision.Y = (int)this.position.Y;
            rectColision.Width = this.sprite.Width;
            rectColision.Height = this.sprite.Height;

            return rectColision;
        }
    }
}
