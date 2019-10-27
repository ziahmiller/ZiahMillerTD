using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    public class TowerBuilderController : MonoBehaviour
    {
        [SerializeField]
        private GameObject towerMenu;

        [SerializeField]
        private Transform cardContrainer;

        private FieldPlot selectedFieldPLot;

        public TowerInfoCard towerInfoCardPrefab;

        [SerializeField]
        private List<Tower> allBuildableTowers = new List<Tower>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void TurnOnTowerMenu(FieldPlot plot)
        {
            if (cardContrainer.childCount < 1) {
                foreach (Tower tower in allBuildableTowers)
                {
                    TowerInfoCard newCard = Instantiate(towerInfoCardPrefab);
                    newCard.transform.SetParent(cardContrainer);
                    newCard.transform.localScale = new Vector3(1,1,1);
                    newCard.AssignTowerToCard(this, tower);
                }
            }
            selectedFieldPLot = plot;
            towerMenu.SetActive(true);
        }


        public void BuildTower(GameObject tower)
        {
            GameObject newTower = Instantiate(tower);
            newTower.transform.SetParent(selectedFieldPLot.GetTowerContainer());
            Debug.Log("tower " + tower.transform.localPosition.x + " : " + tower.transform.localPosition.z);
            newTower.transform.localPosition = new Vector3(0, 0, 0);
            newTower.transform.localScale = new Vector3(1,1,1);
            towerMenu.SetActive(false);
        }
    }
}
