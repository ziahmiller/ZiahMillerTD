using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ZiahTowerDefense
{
    [CustomEditor(typeof(LevelDataBuilder))]
    public class LevelDataBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelDataBuilder levelDataBuilder = (LevelDataBuilder)target;

            
            if (GUILayout.Button("Create Base Level Data"))
            {
                levelDataBuilder.BuildBaseData();
            }
        }
    }
}
