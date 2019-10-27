using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Handles the area the player can build objects.
    /// </summary>
    public class BuildAreaController : MonoBehaviour
    {
        /// <summary>
        /// Build all the selectable build areas.
        /// </summary>
        [SerializeField]
        private BuildAreaGenerator buildAreaGenerator;
        
        
        public void SpawnPlots(LevelDetails levelDetails)
        {
            buildAreaGenerator.SpawnAllBuildAreas(levelDetails);
        }

        public List<Vector3> GetPathAsVector3(List<Plot> plotPath) {
            return buildAreaGenerator.GetPathAsVector3(plotPath);
        }
        
    }
}
