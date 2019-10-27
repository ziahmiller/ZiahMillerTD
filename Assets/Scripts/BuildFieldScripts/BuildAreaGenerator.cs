using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Handles building out the field based on input
    /// </summary>
    public class BuildAreaGenerator : MonoBehaviour
    {
        /// <summary>
        /// The object the build area will place when generating a area.
        /// </summary>
        [SerializeField]
        private FieldPlot plotCellPrefab;

        /// <summary>
        /// Entrance for the enemies.
        /// </summary>
        [SerializeField]
        private FieldPlot entrancePrefab;

        /// <summary>
        /// Exit for enemies.
        /// </summary>
        [SerializeField]
        private FieldPlot exitPrefab;
        

        /// <summary>
        /// Spawns all the cells needed for the build area.
        /// </summary>
        public void SpawnAllBuildAreas(LevelDetails levelDetails)
        {

            ClearAllChildren();

            for (var i = 0; i < levelDetails.rowOfPlots.Count; i++) {

                List<Plot> plotSection = levelDetails.rowOfPlots[i].plotsInRow;

                GameObject newParent = Instantiate(new GameObject());
                newParent.transform.SetParent(transform);
                newParent.transform.localPosition = new Vector3(0, 0, 0);
                newParent.name = "Row" + (i + 1);

                foreach (Plot plot in plotSection)
                {
                    int pathCount = levelDetails.pathwayPlots.Count;
                    
                    if ( plot.xPos == levelDetails.pathwayPlots[0].xPos && plot.zPos == levelDetails.pathwayPlots[0].zPos)
                    {
                        plot.typeOfPlot = Plot.TypeOfPlot.entrance;
                        SpanwBuildAreaCell(plot, entrancePrefab, newParent.transform);
                    }
                    else if (plot.xPos == levelDetails.pathwayPlots[pathCount - 1].xPos && plot.zPos == levelDetails.pathwayPlots[pathCount -1].zPos)
                    {
                        plot.typeOfPlot = Plot.TypeOfPlot.exit;
                        SpanwBuildAreaCell(plot, exitPrefab, newParent.transform);
                    }
                    else {
                        SpanwBuildAreaCell(plot, plotCellPrefab, newParent.transform);
                    }
                }

            }
        }

        /// <summary>
        /// Destroys all objects that are children of this transform.
        /// </summary>
        private void ClearAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }


        /// <summary>
        /// Spawns a single build area cell.
        /// </summary>
        /// <param name="xPos">The x local position</param>
        /// <param name="zPos">The z local position</param>
        private void SpanwBuildAreaCell(Plot plot, FieldPlot prefab, Transform parent)
        {
            FieldPlot newCell = Instantiate(prefab);
            //Need to set the parent to this build generator
            newCell.transform.SetParent(parent);
            //Then set its local position based off parent. Y is zero for now. 
            newCell.transform.localPosition = new Vector3(plot.xPos, 0, plot.zPos);

            newCell.SetUpPlot(plot.typeOfPlot);

        }

        public List<Vector3> GetPathAsVector3(List<Plot> pathPlots)
        {
            List<Vector3> newPath = new List<Vector3>();

            foreach (Plot plot in pathPlots)
            {
                Debug.Log("Plot " + plot.xPos+ " : " + plot.zPos);
                Vector3 newPos = transform.GetChild(plot.xPos).transform.GetChild(plot.zPos).GetComponent<FieldPlot>().GetPathMarkerPosition();
              
                newPath.Add(newPos);
            }

            return newPath;
        }
    }
}
