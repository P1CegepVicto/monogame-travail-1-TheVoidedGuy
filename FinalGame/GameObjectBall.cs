using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectBall
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Rectangle position = new Rectangle(0,0,40,40);
        public bool estVivant;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int currentState = 0; //État de départ
        public int nbState = 3;
        public Rectangle[] currentSprite =
        {
            new Rectangle(0, 0, 40, 40),
            new Rectangle(41, 0, 39, 40),
            new Rectangle(81, 0, 40, 40)
        };
    }
}
