using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    [System.Serializable]
    public class LevelDetails
    {
        /// <summary>
        /// The title of the level.
        /// </summary>
        public string levelTitle;

        /// <summary>
        /// The amount of enemies going to be spawned.
        /// </summary>
        public int enimiesAmount;

        /// <summary>
        /// HowFast the enemies are spawned.
        /// </summary>
        public float spawnRate;

        /// <summary>
        /// Width of the level.
        /// </summary>
        public int xWidth = 8;

        /// <summary>
        /// Length of the level.
        /// </summary>
        public int zLength;

        /// <summary>
        /// The pathway for the level.
        /// </summary>
        public List<RowOfPlots> rowOfPlots = new List<RowOfPlots>();

        /// <summary>
        /// The pathway the enemies will follow. 
        /// </summary>
        public List<Plot> pathwayPlots = new List<Plot>();

        /// <summary>
        /// If the player beat the level.
        /// </summary>
        public bool levelBeat = false;
        

    }
}
