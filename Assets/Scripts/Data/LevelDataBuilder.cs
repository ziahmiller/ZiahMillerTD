using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// Controls the level data and sorting level info.
    /// </summary>
    public class LevelDataBuilder : MonoBehaviour
    {
        /// <summary>
        /// Playable Levels.
        /// </summary>
        private Campaign allLevelDetails;

        /// <summary>
        /// Loads the campaign details that is saved on Application.persistentDataPath
        /// </summary>
        /// <returns></returns>
        public Campaign GetCampaign() {

            string fileDetails = File.ReadAllText(Application.persistentDataPath + "/LevelDetails.txt");

            Campaign newCampaign = JsonUtility.FromJson<Campaign>(fileDetails);

            return newCampaign;

        }

        /// <summary>
        /// Builds the base of data you can structure more levels off of. Saves it to Application.persistentDataPath
        /// </summary>
        public void BuildBaseData()
        {
            
            Campaign newCampaign = new Campaign();
            newCampaign.titleOfLevelDetails = "testing";
            newCampaign.lastLevelPlayed = 0;

            LevelDetails newLevel = GenerateNewLevelDetails(6, 6);
            newLevel.levelTitle = "PreMade 1";
            LevelDetails newLevel2 = GenerateNewLevelDetails(6, 7);
            newLevel2.levelTitle = "PreMade 2";
            LevelDetails newLevel3 = GenerateNewLevelDetails(9, 8);
            newLevel3.levelTitle = "PreMade 3";

            newCampaign.allLevelDetails.Add(newLevel);
            newCampaign.allLevelDetails.Add(newLevel2);
            newCampaign.allLevelDetails.Add(newLevel3);

            SaveCampaign(newCampaign);
        }

        /// <summary>
        /// Saves the campaign as a json file
        /// </summary>
        /// <param name="campaingn">campaign to save</param>
        public void SaveCampaign(Campaign campaingn) {

            string filejson = JsonUtility.ToJson(campaingn);

            File.WriteAllText(Application.persistentDataPath + "/LevelDetails.txt", filejson);

            Debug.Log(Application.persistentDataPath + "/LevelDetails.txt " + campaingn.allLevelDetails.Count);
        }

        /// <summary>
        /// Generates a new level with a set number of rows and plots in rows
        /// </summary>
        /// <param name="xRow">Rows</param>
        /// <param name="zLine">Plots in row</param>
        /// <returns>New LevelDetails</returns>
        public LevelDetails GenerateNewLevelDetails(int xRow, int zLine)
        {
            LevelDetails newLevel = new LevelDetails();
            newLevel.levelTitle = "New Level";
            newLevel.enimiesAmount = 10;
            newLevel.spawnRate = 2;

            for (var x = 0; x < xRow; x++)
            {
                List<Plot> newRow = new List<Plot>();
                for (var z = 0; z < zLine; z++)
                {
                    newRow.Add(new Plot(x, z, Plot.TypeOfPlot.tower));
                }

                RowOfPlots rowOfPlots = new RowOfPlots();
                rowOfPlots.titleOfRow = "Row " + (x + 1);
                newLevel.rowOfPlots.Add(rowOfPlots);

                newLevel.rowOfPlots[x].plotsInRow = newRow;
            }

            return newLevel;
        }
        
    }
}
