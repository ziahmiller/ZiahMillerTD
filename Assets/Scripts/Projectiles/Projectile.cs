using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public abstract class Projectile : MonoBehaviour
    {
        /// <summary>
        /// The damage the projectile does
        /// </summary>
        private float damage;

        /// <summary>
        /// Speed the projectile moves.
        /// </summary>
        private float speed;

        /// <summary>
        /// Rigidbody for projectile.
        /// </summary>
        [SerializeField]
        private Rigidbody rigidbody;

        /// <summary>
        /// Projectile Constructor 
        /// </summary>
        public Projectile(float newDamage, float newSpeed)
        {
            damage = newDamage;
            speed = newSpeed;
        }

        public virtual void DealDamage()
        {

        }
    }
}
