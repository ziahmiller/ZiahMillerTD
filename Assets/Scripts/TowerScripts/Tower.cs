using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public class Tower : Creature
    {
        [SerializeField]
        private Transform turret;

        [SerializeField]
        private Transform weapon;
        
        public TowerInfo towerInfo;
    }
}
