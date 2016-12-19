using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalGame
{
    class GameObjectTile
    {
        public Texture2D texture;
        public Rectangle rectSource = new Rectangle(0, 0, 50, 50);
        public int[,] map = {
                    { 27,39,30,27,28,29,28,29,28,29,28,29,28,29,28,29,30,27,39,30},
                    { 42,31,36,33,31,31,31,31,31,31,31,31,31,31,31,31,36,33,31,45},
                    { 33,31,45,42,31,38,31,31,31,31,31,31,31,31,31,31,45,42,31,36},
                    { 42,31,36,33,31,47,31,31,31,31,31,31,31,31,31,31,36,33,31,45},
                    { 33,31,45,42,31,56,31,31,31,31,31,31,31,31,31,31,45,42,31,36},
                    { 42,31,36,33,31,47,31,31,31,31,31,31,31,31,31,31,36,33,31,45},
                    { 33,31,45,42,31,56,31,31,31,31,31,31,31,31,31,31,45,42,31,36},
                    { 42,31,43,44,31,64,61,62,61,62,61,62,61,62,61,62,43,44,31,45},
                    { 33,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,55,55,31,36},
                    { 51,52,53,52,53,52,53,52,53,52,53,52,53,52,53,52,53,52,53,54}
                };
        public const int LIGNE = 20; // Y
        public const int COLONNE = 10; // X


        //X = 0 / 51 / 101 / 152 / 202 / 253 / 303 / 354 / 404
        //Y = 152 / 202 / 253 / 303 / 354

        #region Outworld Tiles
        public Rectangle tile01 = new Rectangle(0, 0, 50, 50);
        public Rectangle tile02 = new Rectangle(51, 0, 49, 50);
        public Rectangle tile03 = new Rectangle(101, 0, 50, 50);
        public Rectangle tile04 = new Rectangle(152, 0, 49, 50);
        public Rectangle tile05 = new Rectangle(0, 51, 50, 49);
        public Rectangle tile06 = new Rectangle(51, 51, 49, 49);
        public Rectangle tile07 = new Rectangle(101, 51, 50, 49);
        public Rectangle tile08 = new Rectangle(152, 51, 49, 49);
        public Rectangle tile09 = new Rectangle(202, 51, 50, 49);
        public Rectangle tile10 = new Rectangle(0, 101, 50, 50);
        public Rectangle tile11 = new Rectangle(51, 101, 49, 50);
        public Rectangle tile12 = new Rectangle(101, 101, 50, 50);
        public Rectangle tile13 = new Rectangle(152, 101, 49, 50);
        public Rectangle tile14 = new Rectangle(202, 101, 50, 50);
        #endregion
        #region Outworld Dungeon
        public Rectangle tile15 = new Rectangle(253, 0, 49, 50);
        public Rectangle tile16 = new Rectangle(303, 0, 50, 50);
        public Rectangle tile17 = new Rectangle(354, 0, 49, 50);
        public Rectangle tile18 = new Rectangle(404, 0, 50, 50);
        public Rectangle tile19 = new Rectangle(253, 51, 49, 49);
        public Rectangle tile20 = new Rectangle(303, 51, 50, 49);
        public Rectangle tile21 = new Rectangle(354, 51, 49, 49);
        public Rectangle tile22 = new Rectangle(404, 51, 50, 49);
        public Rectangle tile23 = new Rectangle(253, 101, 49, 50);
        public Rectangle tile24 = new Rectangle(303, 101, 50, 50);
        public Rectangle tile25 = new Rectangle(354, 101, 49, 50);
        public Rectangle tile26 = new Rectangle(404, 101, 50, 50);
        #endregion
        #region Dungeon Tiles
        public Rectangle tile27 = new Rectangle(0, 152, 50, 49);
        public Rectangle tile28 = new Rectangle(51, 152, 49, 49);
        public Rectangle tile29 = new Rectangle(101, 152, 50, 49);
        public Rectangle tile30 = new Rectangle(152, 152, 49, 49);
        public Rectangle tile31 = new Rectangle(202, 152, 50, 49);
        public Rectangle tile32 = new Rectangle(253, 152, 49, 49);

        public Rectangle tile33 = new Rectangle(0, 202, 50, 50);
        public Rectangle tile34 = new Rectangle(51, 202, 49, 50);
        public Rectangle tile35 = new Rectangle(101, 202, 50, 50);
        public Rectangle tile36 = new Rectangle(152, 202, 49, 50);
        public Rectangle tile37 = new Rectangle(202, 202, 50, 50);
        public Rectangle tile38 = new Rectangle(253, 202, 49, 50);
        public Rectangle tile39 = new Rectangle(303, 202, 50, 50);
        public Rectangle tile40 = new Rectangle(354, 202, 49, 50);
        public Rectangle tile41 = new Rectangle(404, 202, 50, 50);

        public Rectangle tile42 = new Rectangle(0, 253, 50, 49);
        public Rectangle tile43 = new Rectangle(51, 253, 49, 49);
        public Rectangle tile44 = new Rectangle(101, 253, 50, 49);
        public Rectangle tile45 = new Rectangle(152, 253, 49, 49);
        public Rectangle tile46 = new Rectangle(202, 253, 50, 49);
        public Rectangle tile47 = new Rectangle(253, 253, 49, 49);
        public Rectangle tile48 = new Rectangle(303, 253, 50, 49);
        public Rectangle tile49 = new Rectangle(354, 253, 49, 49);
        public Rectangle tile50 = new Rectangle(404, 253, 50, 49);

        public Rectangle tile51 = new Rectangle(0, 303, 50, 50);
        public Rectangle tile52 = new Rectangle(51, 303, 49, 50);
        public Rectangle tile53 = new Rectangle(101, 303, 50, 50);
        public Rectangle tile54 = new Rectangle(152, 303, 49, 50);
        public Rectangle tile55 = new Rectangle(202, 303, 50, 50);
        public Rectangle tile56 = new Rectangle(253, 303, 49, 50);
        public Rectangle tile57 = new Rectangle(303, 303, 50, 50);
        public Rectangle tile58 = new Rectangle(354, 303, 49, 50);
        public Rectangle tile59 = new Rectangle(404, 303, 50, 50);

        public Rectangle tile60 = new Rectangle(0, 354, 50, 49);
        public Rectangle tile61 = new Rectangle(51, 354, 49, 49);
        public Rectangle tile62 = new Rectangle(101, 354, 50, 49);
        public Rectangle tile63 = new Rectangle(152, 354, 49, 49);
        public Rectangle tile64 = new Rectangle(202, 354, 50, 49);
        public Rectangle tile65 = new Rectangle(253, 354, 49, 49);
        public Rectangle tile66 = new Rectangle(303, 354, 50, 49);
        public Rectangle tile67 = new Rectangle(354, 354, 49, 49);
        public Rectangle tile68 = new Rectangle(404, 354, 50, 49);
        #endregion

        public Rectangle tile69 = new Rectangle(303, 152, 50, 49);
    }
}
