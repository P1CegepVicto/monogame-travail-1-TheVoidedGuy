using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectBoss
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Rectangle position = new Rectangle(0, 0, 38, 50);
        public Rectangle projPosition = new Rectangle(0, 0, 20, 20);
        public bool estVivant;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int currentState = 0; //État de départ
        public int nbState = 4;
        public Rectangle[] dragonSprite =
        {
            new Rectangle(0, 0, 38, 50),
            new Rectangle(39, 0, 38, 50),
            new Rectangle(78, 0, 38, 50),
            new Rectangle(78, 0, 38, 50)
        };
        public Rectangle[] projSprite =
        {
            new Rectangle(0, 51, 20, 20),
            new Rectangle(21, 51, 20, 20),
            new Rectangle(42, 51, 20, 20)
        };
    }
}
