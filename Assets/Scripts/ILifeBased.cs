using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public interface ILifeBased 
    {
        /// <summary>
        /// Max life for this interface.
        /// </summary>
        float MaxLife { get; set; }

        /// <summary>
        /// Current life for interface.
        /// </summary>
        float CurrentLife { get; set; }

        /// <summary>
        /// Changes the health. Can be used to heal or deal damage.
        /// </summary>
        /// <param name="amount">Amount to change by</param>
        void ChangeHealth(float amount);
    }
}
