using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZiahTowerDefense
{
    public class LevelBuilderTool : EditorWindow
    {

        /// <summary>
        /// LevelDataBuilder to access the campaign and other level details.
        /// </summary>
        private LevelDataBuilder levelDataBuilder = new LevelDataBuilder();

        /// <summary>
        /// the current campaign the player is doing.
        /// </summary>
        private Campaign currentCampaign;

        /// <summary>
        /// If the currentCampaign is loaded.
        /// </summary>
        private static bool loaded = false;

        /// <summary>
        /// Level Designing in the inspector
        /// </summary>
        int levelToLookAt = 0;

        /// <summary>
        /// If user is building a path with the plots.
        /// </summary>
        private bool selectingPath;

        /// <summary>
        /// Lets the user know what is being edited.
        /// </summary>
        string editingPlotMessge = "EDITING PLOTS FOR TOWERS. Just click the tower plots you want to have active or not.";

        /// <summary>
        /// For scrolling when the window is to small.
        /// </summary>
        Vector2 scrollPos;

        int xNumber = 8;

        int zNumber = 8;

        [MenuItem("Window/Level Builder Tool")]
        public static void ShowWindow()
        {
            loaded = false;
            GetWindow<LevelBuilderTool>("Level Builder Tool");

        }

        private void OnGUI()
        {

            if (!loaded)
            {
                if (GUILayout.Button("Load Current Campaign"))
                {
                    currentCampaign = levelDataBuilder.GetCampaign();
                    loaded = true;
                }
            }
            else
            {
                DisplayCurrentCampaignGrid();
            }
        }

        /// <summary>
        /// Displays the info for current campaign.
        /// </summary>
        private void DisplayCurrentCampaignGrid()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            ShowLevelSelectionOptions();

            DisplayEnemyInfo(currentCampaign.allLevelDetails[levelToLookAt]);

            EditFieldPlotsControls();


            for (var i = 0; i < currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots.Count; i++)
            {
                GUILayout.BeginHorizontal("box");
                List<Plot> plotSection = currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots[i].plotsInRow;

                for (var p = 0; p < plotSection.Count; p++)
                {
                    SpawnButtonForPlot(plotSection[p]);
                }
                GUILayout.EndHorizontal();
            }
            

            DisplayFileOptions();

            EditorGUILayout.EndScrollView();
            GUIUtility.ExitGUI();
        }

        /// <summary>
        /// Displays the enemies about a level to the user.
        /// </summary>
        /// <param name="levelDetails">The level to display</param>
        private void DisplayEnemyInfo(LevelDetails levelDetails)
        {

            GUILayout.TextArea("Info about the enemies in this level.");
            GUILayout.BeginHorizontal("box");

            GUILayout.TextArea("This level has " + levelDetails.enimiesAmount + " enemies.", GUILayout.Width(100), GUILayout.Height(50));
            
            if (GUILayout.Button("+", GUILayout.Width(50), GUILayout.Height(50)))
            {
                levelDetails.enimiesAmount += 1;
            }
            if (GUILayout.Button("-", GUILayout.Width(50), GUILayout.Height(50)))
            {
                if (levelDetails.enimiesAmount > 1) {
                    levelDetails.enimiesAmount -= 1;
                }
            }

            GUILayout.TextArea("The enemies spawn every " + levelDetails.spawnRate + " seconds.", GUILayout.Width(100), GUILayout.Height(50));

            if (GUILayout.Button("+", GUILayout.Width(50), GUILayout.Height(50)))
            {
                levelDetails.spawnRate += 0.1f;
            }
            if (GUILayout.Button("-", GUILayout.Width(50), GUILayout.Height(50)))
            {
                if (levelDetails.spawnRate > 0.1f) {
                    levelDetails.spawnRate -= 0.1f;
                }
            }

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// The controls for editing the plots.
        /// </summary>
        private void EditFieldPlotsControls()
        {
            GUILayout.TextArea(editingPlotMessge);

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("SET UP PATH (Will clear path)", GUILayout.Width(200), GUILayout.Height(50)))
            {
                ClearAllPaths();
                editingPlotMessge = "EDITING PLOTS FOR PATHING. CLEARED THE LAST PATH.";
                selectingPath = true;
            }

            if (GUILayout.Button("TOGGLE TOWER PLOTS", GUILayout.Width(200), GUILayout.Height(50)))
            {
                editingPlotMessge = "EDITING PLOTS FOR TOWERS. Just click the tower plots you want to have active or not.";
                selectingPath = false;
            }
            GUILayout.EndHorizontal();

            GUILayout.TextArea(editingPlotMessge);
        }

        /// <summary>
        /// Displays to the player level navigation for the current campaign
        /// </summary>
        private void ShowLevelSelectionOptions()
        {
            GUILayout.TextArea("Select what level to edit or add a new one." );
            GUILayout.TextArea("LEVEL " + (levelToLookAt + 1) + "/" + currentCampaign.allLevelDetails.Count + " SELECTED TO EDIT.");
         
            if (GUILayout.Button("Edit Next Level", GUILayout.Width(200), GUILayout.Height(50)))
            {
                
                ToggleThroughLevels();

            }

            GUILayout.BeginHorizontal("box");

            GUILayout.BeginHorizontal("box");

            xNumber = EditorGUILayout.IntField("Number of rows", xNumber);
            zNumber = EditorGUILayout.IntField("Number of plots in a row:", zNumber);

            GUILayout.EndHorizontal();

            if (GUILayout.Button("Add another Level",  GUILayout.Width(200), GUILayout.Height(50)))
            {
                LevelDetails newLevel = levelDataBuilder.GenerateNewLevelDetails(xNumber, zNumber);
                newLevel.levelTitle = "Level " + (currentCampaign.allLevelDetails.Count + 1);
                
                currentCampaign.allLevelDetails.Add(newLevel);

                levelToLookAt = (currentCampaign.allLevelDetails.Count - 1);
            }

            GUILayout.EndHorizontal();

        }

        /// <summary>
        /// Displays saving and loading for the user
        /// </summary>
        private void DisplayFileOptions()
        {
            
            GUILayout.TextArea("You can save the changes you made or reload the file and start over.");

            GUILayout.BeginHorizontal("box");

            GUIStyle newStyle = new GUIStyle(GUI.skin.button);
            newStyle.fixedHeight = 50;
            newStyle.fixedWidth = 200;

            newStyle.normal.textColor = Color.green;

            if (GUILayout.Button("SAVE", newStyle))
            {
                levelDataBuilder.SaveCampaign(currentCampaign);
            }

            newStyle.normal.textColor = Color.red;

            if (GUILayout.Button("RELOAD", newStyle))
            {
                levelToLookAt = 0;
                currentCampaign = levelDataBuilder.GetCampaign();
            }

            GUILayout.EndHorizontal();
            
        }

        /// <summary>
        /// Spawns the buttons for the plots that are on the field.
        /// </summary>
        /// <param name="plot">Plot to spawn a button for</param>
        public void SpawnButtonForPlot(Plot plot)
        {
            GUIStyle newStyle = new GUIStyle(GUI.skin.button);
            newStyle.fixedHeight = 50;
            newStyle.fixedWidth = 50;

            if (plot.typeOfPlot == Plot.TypeOfPlot.path)
            {
                newStyle.normal.textColor = Color.green;
            }
            else if (plot.typeOfPlot == Plot.TypeOfPlot.tower)
            {
                newStyle.normal.textColor = Color.white;
            }
            else {
                newStyle.normal.textColor = Color.black;
            }

            if (GUILayout.Button(plot.typeOfPlot.ToString().ToUpper(), newStyle))
            {
                ChangePlot(plot);
            }
        }

        /// <summary>
        /// Toggles through the levels so you can edit one at a time.
        /// </summary>
        private void ToggleThroughLevels()
        {

            levelToLookAt += 1;

            if (levelToLookAt >= currentCampaign.allLevelDetails.Count)
            {
                levelToLookAt = 0;
            }
            else if (levelToLookAt <= 0)
            {
                levelToLookAt = currentCampaign.allLevelDetails.Count - 1;
            }
        }

        /// <summary>
        /// Changes the plot type based on user input
        /// </summary>
        /// <param name="plot">Plot to change</param>
        public void ChangePlot(Plot plot)
        {
            if (selectingPath && plot.typeOfPlot != Plot.TypeOfPlot.path)
            {
                plot.typeOfPlot = Plot.TypeOfPlot.path;
                currentCampaign.allLevelDetails[levelToLookAt].pathwayPlots.Add(plot);
                Debug.Log("path is now " + currentCampaign.allLevelDetails[levelToLookAt].pathwayPlots.Count + " long");
            }
            else if (!selectingPath && plot.typeOfPlot != Plot.TypeOfPlot.path)
            {
                if (plot.typeOfPlot == Plot.TypeOfPlot.tower)
                {
                    plot.typeOfPlot = Plot.TypeOfPlot.empty;
                }
                else
                {
                    plot.typeOfPlot = Plot.TypeOfPlot.tower;
                }
            }
        }

        /// <summary>
        /// Resets all the path plots to tower plots to remap the path.
        /// </summary>
        private void ClearAllPaths()
        {
            currentCampaign.allLevelDetails[levelToLookAt].pathwayPlots = new List<Plot>();
            for (var i = 0; i < currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots.Count; i++)
            {
                for (var j = 0; j < currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots[i].plotsInRow.Count; j++)
                {
                    if (currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots[i].plotsInRow[j].typeOfPlot == Plot.TypeOfPlot.path)
                    {
                        currentCampaign.allLevelDetails[levelToLookAt].rowOfPlots[i].plotsInRow[j].typeOfPlot = Plot.TypeOfPlot.tower;
                    }
                }
            }
        }


    }
}
