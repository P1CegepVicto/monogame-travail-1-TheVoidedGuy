using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectSlime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Rectangle position;
        public bool estVivant;
        public byte vie;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran
        public int Time;
        public int time = 0;
        public enum etats {droite,gauche,die};
        public etats objetState;

        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int currentState = 0; //État de départ
        public int nbState = 2;
        public Rectangle[] tabDroite =
        {
            new Rectangle(0, 0, 40, 50),
            new Rectangle(41, 2, 40, 48),
        };
        public Rectangle[] tabGauche =
        {
            new Rectangle(0, 51, 40, 50),
            new Rectangle(41, 53, 40, 48),
        };
        public Rectangle[] tabDie =
        {
            new Rectangle(0, 102, 40, 30),
            new Rectangle(41, 111, 32, 21),
        };
    }
}
