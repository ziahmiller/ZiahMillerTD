using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZiahTowerDefense
{
    public class LevelBuilderWindow : EditorWindow
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
        string editingPlotMessge = "EDITING PLOTS FOR TOWERS.Just click the tower plots you want to have active or not.";

        Vector2 scrollPos;

        [MenuItem("Window/Level Builder")]
        public static void ShowWindow()
        {
            loaded = false;
            GetWindow<LevelBuilderWindow>("Level Builder");

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
                if (GUILayout.Button("Next Level "))
                {
                    ToggleThroughLevels();

                }

                DisplayCurrentCampaignGrid();
            }
        }

        /// <summary>
        /// Displays the info for current campaign.
        /// </summary>
        private void DisplayCurrentCampaignGrid()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);



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

            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        public void EditFieldPlotsControls()
        {
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

        public void SpawnButtonForPlot(Plot plot)
        {
            GUIStyle newStyle = new GUIStyle(GUI.skin.button);
            if (plot.typeOfPlot == Plot.TypeOfPlot.path)
            {

            }
            else
            {

            }

            if (GUILayout.Button(plot.typeOfPlot.ToString().ToUpper(), GUILayout.Width(50), GUILayout.Height(50)))
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

        public void ChangePlot(Plot plot)
        {
            if (selectingPath && plot.typeOfPlot != Plot.TypeOfPlot.path)
            {
                plot.typeOfPlot = Plot.TypeOfPlot.path;
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
