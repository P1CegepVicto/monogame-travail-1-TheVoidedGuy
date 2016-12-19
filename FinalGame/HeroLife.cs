using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class HeroLife
    {
        public Texture2D sprite;
        public Rectangle rectSource = new Rectangle(1000, 0, 25, 25);
        public Rectangle rectHp = new Rectangle(1015, 0, 45, 25);
        public byte[] vie = { 3, 3 };
        public Rectangle[] spriteAfficher = new Rectangle[6];
        public enum etats { vrai, faux, HP };
        public etats[] objetState = new etats[6];

        public Rectangle tabVrai = new Rectangle(30, 0, 25, 25);
        public Rectangle tabFaux = new Rectangle(55, 0, 25, 25);
        public Rectangle tabHP = new Rectangle(0, 0, 30, 25);
    }
}
