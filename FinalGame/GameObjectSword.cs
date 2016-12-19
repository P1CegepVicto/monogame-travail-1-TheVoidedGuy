using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectSword
    {
        public Texture2D sprite;
        public bool estVivant;
        public Vector2 vitesse;
        public byte time;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public enum etats { droite, gauche, haut, bas };
        public etats objetState;

        public Rectangle swordD = new Rectangle(0, 0, 42, 8);
        public Rectangle swordG = new Rectangle(0, 9, 42, 8);
        public Rectangle swordB = new Rectangle(0, 18, 8, 42);
        public Rectangle swordH = new Rectangle(9, 18, 8, 42);
        public Rectangle swordProjectileD = new Rectangle(18, 52, 24, 8);
        public Rectangle swordProjectileG = new Rectangle(18, 43, 24, 8);
        public Rectangle swordProjectileH = new Rectangle(34, 18, 8, 24);
        public Rectangle swordProjectileB = new Rectangle(18, 18, 8, 24);
    }
}
