using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZiahTowerDefense
{
    public class TowerInfoCard : MonoBehaviour
    {

        public Text title;
        public Text discription;
        public Button selectButton;

        private Tower towerPrefab;
        private TowerBuilderController towerBuilderController;
        // Start is called before the first frame update
        void Start()
        {
            selectButton.onClick.AddListener(BuildTower);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void AssignTowerToCard(TowerBuilderController controller, Tower tower)
        {
            towerBuilderController = controller;
            towerPrefab = tower;

            title.text = "Temp";///tower.towerInfo.Title;
            discription.text = "Some random description";// tower.towerInfo.Discription;
        }

        public void BuildTower()
        {
            Debug.Log("BuildTower()");
            towerBuilderController.BuildTower(towerPrefab.gameObject);
        }
    }
}
