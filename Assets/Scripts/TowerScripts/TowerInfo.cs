using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public class TowerInfo
    {
        public enum TowerType {
            Bow,
            Cannon,
            Hammer,
            Rocket
        }
        
        private TowerType towerType;

        private string title;

        private string discription;

        private float damage;

        private float fireSpeed;

        public TowerType TowerType1 { get => towerType; set => towerType = value; }
        public string Title { get => title; set => title = value; }
        public string Discription { get => discription; set => discription = value; }
        public float Damage { get => damage; set => damage = value; }
        public float FireSpeed { get => fireSpeed; set => fireSpeed = value; }

        public TowerInfo(TowerType newType, string newTitle, string newDiscription, float newDamage, float newFire)
        {
            TowerType1 = newType;
            Title = newTitle;
            Discription = newDiscription;
            Damage = newDamage;
            FireSpeed = newFire;
        }
    }

}
