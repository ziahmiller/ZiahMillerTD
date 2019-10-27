using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Interface for classes to use if selected by the player.
    /// </summary>
    interface ISelectObject
    {
        /// <summary>
        /// Called when player selects the object.
        /// </summary>
        void ObjectSelected();
    }
}
