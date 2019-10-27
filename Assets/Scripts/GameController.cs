using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Controls the setup of the game and how the player can interact with the game overall. 
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public GameObject testNextButton;

        /// <summary>
        /// the current campaign the player is doing.
        /// </summary>
        private Campaign currentCampaign;

        /// <summary>
        /// LevelDataBuilder to access the campaign and other level details.
        /// </summary>
        private LevelDataBuilder levelDataBuilder = new LevelDataBuilder();


        /// <summary>
        /// BuildAreaController prefab that is loaded.
        /// </summary>
        [SerializeField]
        private BuildAreaController buildAreaControllerPrefab;

        /// <summary>
        /// BuildAreaController
        /// </summary>
        [HideInInspector]
        private BuildAreaController buildAreaController;

        /// <summary>
        /// Controls the enemies being spawned prefab.
        /// </summary>
        [SerializeField]
        private EnemySpawnController enemySpawnControllerPrefab;

        /// <summary>
        /// Controls the enemies being spawned.
        /// </summary>
        private EnemySpawnController enemySpawnController;

        /// <summary>
        /// Controls the towers and how they are built.
        /// </summary>
        public TowerBuilderController towerBuilderController;

        // Start is called before the first frame update
        void Awake()
        {
            BuildPlayableArea();
            testNextButton.SetActive(false);
        }

        private void Start()
        {
            enemySpawnController.DoneSpawningCraetures.AddListener(TurnOnNextLevelButton);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                SelectObject();
            }
        }

        /// <summary>
        /// When clicking selects the object clicked on.
        /// </summary>
        private void SelectObject()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {


                ISelectObject selectedObject = hit.transform.GetComponent<ISelectObject>();


                if (hit.transform.tag == "FieldPlot")
                {
                    Debug.Log("Clicked on " + hit.transform.name);
                    towerBuilderController.TurnOnTowerMenu(hit.transform.GetComponent<FieldPlot>());
                }

            }
        }

        /// <summary>
        /// Sets up for the next level.
        /// </summary>
        public void NextLevelSetUp()
        {
            if (currentCampaign.lastLevelPlayed < (currentCampaign.allLevelDetails.Count - 1)) {
                currentCampaign.lastLevelPlayed++;
                buildAreaController.SpawnPlots(currentCampaign.allLevelDetails[currentCampaign.lastLevelPlayed]);
                StartCoroutine(DeplayPathSetup());
                testNextButton.SetActive(false);
            }
        }

        /// <summary>
        /// Checks to see how many enemies are still on the field.
        /// </summary>
        public void TurnOnNextLevelButton()
        {
            testNextButton.SetActive(true);
        }

  
        /// <summary>
        /// Loads the campaign.
        /// </summary>
        public void LoadCampaign()
        {
            //Used for when editing stuff in editor and still able to use the function of the script through unity editor. 
            if (levelDataBuilder == null) {
                levelDataBuilder = new LevelDataBuilder();
            }

            currentCampaign = levelDataBuilder.GetCampaign();

            buildAreaController.SpawnPlots(currentCampaign.allLevelDetails[currentCampaign.lastLevelPlayed]);

            SetupSpawningEnemiesPath();
        }

        public void StartSpawningCretures()
        {
            enemySpawnController.BeginSpawnCreatures();
        }

        /// <summary>
        /// Creates the field to be able to be played.
        /// </summary>
        private void BuildPlayableArea()
        {
            buildAreaController = SpawnAndReturnPart<BuildAreaController>(buildAreaControllerPrefab.gameObject);
            enemySpawnController = SpawnAndReturnPart<EnemySpawnController>(enemySpawnControllerPrefab.gameObject);
        }

        /// <summary>
        /// Creates the body for a part.
        /// </summary>
        /// <typeparam name="T">the type of object the part is</typeparam>
        /// <param name="part">The part</param>
        /// <returns>The script for the part</returns>
        private T SpawnAndReturnPart<T>(GameObject part)
        {
            GameObject partBody = Instantiate(part);

            partBody.transform.SetParent(transform);

            partBody.transform.localPosition = new Vector3(0, 0, 0);
            partBody.transform.localScale = new Vector3(1, 1, 1);

            return partBody.GetComponent<T>();
        }

        private void SetupSpawningEnemiesPath()
        {
           
            List<Vector3> path = buildAreaController.GetPathAsVector3(currentCampaign.allLevelDetails[currentCampaign.lastLevelPlayed].pathwayPlots);

            enemySpawnController.SetUpCreturesToSpawn(path, currentCampaign.allLevelDetails[currentCampaign.lastLevelPlayed].spawnRate, currentCampaign.allLevelDetails[currentCampaign.lastLevelPlayed].enimiesAmount);
        }

        /// <summary>
        /// Delays the path setup.
        /// </summary>
        /// <returns></returns>
        IEnumerator DeplayPathSetup()
        {
            yield return new WaitForSeconds(0.1f);
            SetupSpawningEnemiesPath();
        }
        
    }
}
