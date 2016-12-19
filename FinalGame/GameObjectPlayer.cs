using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectPlayer
    {
        public Texture2D sprite;
        public Vector2 direction;
        public Rectangle position;
        public int time = 0;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran
        public enum etats { attenteDroite, attenteGauche, attenteHaut, attenteBas, runDroite, runGauche, runHaut, runBas, attackLeft, attackRight, attackDown, attackHaut};
        public etats objetState;
        
        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int runStateV = 0; //État de départ
        public int nbEtatRunV = 4; //Combien il y a de rectangles pour l’état “courrir vertical”
        public Rectangle[] tabRunHaut =
        {
            new Rectangle(74, 32, 18, 31),
            new Rectangle(110, 0, 18, 31),
            new Rectangle(93, 32, 18, 31),
            new Rectangle(110, 0, 18, 31)
        };
        public Rectangle[] tabRunBas =
        {
            new Rectangle(36, 32, 18, 31),
            new Rectangle(72, 0, 18, 31),
            new Rectangle(55, 32, 18, 31),
            new Rectangle(72, 0, 18, 31)
        };

        public int runStateH = 0; //État de départ
        public int nbEtatRunH = 2; //Combien d’état pour l’état “courrir horizontal”
        public Rectangle[] tabRunDroite =
        {
            new Rectangle(0, 32, 17, 31),
            new Rectangle(0, 0, 17, 31)
        };
        public Rectangle[] tabRunGauche =
        {
            new Rectangle(18, 32, 17, 31),
            new Rectangle(36, 0, 17, 31)
        };

        public int waitState = 0; //État de départ
        public int nbEtatWait = 2; //Combien d’état pour l’état “attendre”
        public Rectangle[] waitDroite =
        {
            new Rectangle(0, 0, 17, 31),
            new Rectangle(18, 0, 17, 31)
        };
        public Rectangle[] waitGauche =
        {
            new Rectangle(36, 0, 17, 31),
            new Rectangle(54, 0, 17, 31)
        };
        public Rectangle[] waitHaut =
        {
            new Rectangle(110, 0, 18, 31),
            new Rectangle(129, 0, 18, 31)
        };
        public Rectangle[] waitBas =
        {
            new Rectangle(72, 0, 18, 31),
            new Rectangle(91, 0, 18, 31)
        };
        public Rectangle attackDroite = new Rectangle(0, 64, 17, 31);
        public Rectangle attackGauche = new Rectangle(18, 64, 17, 31);
        public Rectangle attackBas = new Rectangle(36, 64, 18, 31);
        public Rectangle attackHaut = new Rectangle(55, 64, 18, 31);
    }
}
