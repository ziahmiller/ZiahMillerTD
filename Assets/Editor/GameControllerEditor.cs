using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace ZiahTowerDefense
{
    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameController gameController = (GameController)target;

            if (GUILayout.Button("Load Current Campaign"))
            {
                gameController.LoadCampaign();
            }
        }
    }
}
