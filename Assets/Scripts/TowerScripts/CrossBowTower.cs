using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public class CrossBowTower : Tower
    {
        // Start is called before the first frame update
        void Awake()
        {
            towerInfo = new TowerInfo(TowerInfo.TowerType.Bow, "CrossBow Tower", "Shoots Bolts at a target at a fast pace.", 1.0f, 1.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
