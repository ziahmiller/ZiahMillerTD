using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// The plot an object (tower) is placed.
    /// </summary>
    public class FieldPlot : MonoBehaviour, ISelectObject
    {
        /// <summary>
        /// ISelectObject being held.
        /// </summary>
        private ISelectObject objectBeingHeld;

        /// <summary>
        /// The area of the cell the player is able to select.
        /// </summary>
        [SerializeField]
        private GameObject selectableCallArea;

        /// <summary>
        /// Displays if it is a path marker.
        /// </summary>
        [SerializeField]
        private GameObject pathMarker;

        /// <summary>
        /// The container that holds tower or objects.
        /// </summary>
        [SerializeField]
        private Transform container;

        /// <summary>
        /// Type of plot the cell is.
        /// </summary>
        private Plot.TypeOfPlot typeOfPlot;

        /// <summary>
        /// Gets pathMaker position
        /// </summary>
        public Vector3 GetPathMarkerPosition()
        {
            return pathMarker.transform.position;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// When the player selects this plot
        /// </summary>
        public void ObjectSelected()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sets up the visual for the plot.
        /// </summary>
        /// <param name="newTypeOfPlot"></param>
        public void SetUpPlot(Plot.TypeOfPlot newTypeOfPlot)
        {
            typeOfPlot = newTypeOfPlot;
         

            if (typeOfPlot == Plot.TypeOfPlot.path)
            {
                selectableCallArea.SetActive(false);
            }
            else
            {
                pathMarker.SetActive(false);
            }
        }

        /// <summary>
        /// Returns the tower container.
        /// </summary>
        /// <returns></returns>
        public Transform GetTowerContainer()
        {
            return container;
        }
    }
}
