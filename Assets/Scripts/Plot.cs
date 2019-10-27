using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    [System.Serializable]
    public class Plot 
    {
        public enum TypeOfPlot
        {
            tower = 0,
            entrance = 1,
            path = 2,
            exit = 3,
            empty = 4
        }

        /// <summary>
        /// X position
        /// </summary>
        public int xPos;

        /// <summary>
        /// Z position
        /// </summary>
        public int zPos;

        /// <summary>
        /// Nodes placement within the structure of the path.
        /// </summary>
        public TypeOfPlot typeOfPlot;

        public Plot(int newX, int newZ, TypeOfPlot newPlacement)
        {
            xPos = newX;
            zPos = newZ;
            typeOfPlot = newPlacement;
        }
    }
}
