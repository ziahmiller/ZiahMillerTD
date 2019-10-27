using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    [System.Serializable]
    public class Campaign
    {
        /// <summary>
        /// Las Level played by the player.
        /// </summary>
        public int lastLevelPlayed;

        /// <summary>
        /// Title of the level details in this campaign.
        /// </summary>
        public string titleOfLevelDetails;

        /// <summary>
        /// All the level details for this campaign. 
        /// </summary>
        public List<LevelDetails> allLevelDetails = new List<LevelDetails>();

    }
}
