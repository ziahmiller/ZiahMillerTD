using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Builds the path that the enemies will travel.
    /// </summary>
    public class BuildPathway 
    {
        /// <summary>
        /// The path the enemies will follow.
        /// </summary>
        private List<Plot> path = new List<Plot>(0);
        
        public void CreateTempPath()
        {
            path.Add(new Plot(1, 0, Plot.TypeOfPlot.entrance));
            path.Add(new Plot(1, 1, Plot.TypeOfPlot.path));
            path.Add(new Plot(1, 2, Plot.TypeOfPlot.path));
            path.Add(new Plot(1, 3, Plot.TypeOfPlot.path));
            path.Add(new Plot(1, 4, Plot.TypeOfPlot.path));
            path.Add(new Plot(1, 5, Plot.TypeOfPlot.path));
            path.Add(new Plot(1, 6, Plot.TypeOfPlot.path));


            path.Add(new Plot(1, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(2, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(3, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(4, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(5, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(6, 6, Plot.TypeOfPlot.path));
            path.Add(new Plot(7, 6, Plot.TypeOfPlot.exit));

        }

        /// <summary>
        /// Clears the path.
        /// </summary>
        public void ClearPath(Transform area)
        {
            foreach (Transform child in area)
            {
                foreach (Plot point in path)
                {
                    if (child.localPosition.x == point.xPos && child.localPosition.z == point.zPos) {
                        Object.Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}
