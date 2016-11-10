using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Monogame
{
    class GameObject
    {
        public bool estVivant;
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
