using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZiahTowerDefense
{
    public class EnemySpawnController : MonoBehaviour
    {

        public UnityEvent DoneSpawningCraetures;

        [SerializeField]
        private Creature enemyPrefab;

        [SerializeField]
        private Vector3 spawnPoint;


        private float spawnTimer;

        private int amountToSpawn;

        private bool spawningCreatures = false;

        private List<Vector3> pathForEnemiesToFollow = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (spawningCreatures && !AreEnemiesOnField())
            {
                spawningCreatures = false;
                DoneSpawningCraetures.Invoke();
            }
        }

        public void SetUpCreturesToSpawn(List<Vector3> path, float timer, int amount) {
            spawnPoint = path[0];
            pathForEnemiesToFollow = path;
            spawnTimer = timer;
            amountToSpawn = amount;

            Debug.Log("SpawnPoint  = " + spawnPoint + ", pathForEnemiesToFollow  = " + pathForEnemiesToFollow.Count + ", spawnTimer  = " + spawnTimer + ", amountToSpawn = " + amountToSpawn);
        }

        public void BeginSpawnCreatures()
        {
            
            StartCoroutine(SpawnTheCreturesOverTime());
        }

        IEnumerator SpawnTheCreturesOverTime() {
            yield return new WaitForEndOfFrame();
            spawningCreatures = true;
            for (var i = 0; i <= amountToSpawn; i++ ) {
                
                SpawnNewCreature();
                yield return new WaitForSeconds(spawnTimer);
            }

            
        }

        private void SpawnNewCreature()
        {
            
            Creature newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.SetParent(transform);
            newEnemy.transform.position = spawnPoint;
            newEnemy.transform.localScale = new Vector3(1, 1, 1);

            //Debug.Log(pathForEnemiesToFollow.Count + " Spawning creature " + pathForEnemiesToFollow[0]);
            newEnemy.SutUpPathway(pathForEnemiesToFollow);
            newEnemy.SetUpNextPoit(pathForEnemiesToFollow[0]);

        }

        public bool AreEnemiesOnField()
        {
            if (transform.childCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}