using System;
using _Game.Scripts.Configs;
using _Game.Scripts.Configs.AsteroidConfig;
using _Game.Scripts.Configs.BulletConfig;
using _Game.Scripts.Configs.GameConfig;
using _Game.Scripts.Configs.MovementConfig;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    public class ConfigsEditorWindow : EditorWindow
    {
        protected SerializedObject serializedObject;
        protected SerializedProperty serializedProperty;
        protected AsteroidConfig[] asteroidConfigs;
        protected BulletConfig[] bulletConfigs;
        protected MovementConfig[] movementConfigs;
        protected GameConfig[] gameConfigs;

        protected string selectedPropertyPath;
        protected string selectedProperty;
        
        [MenuItem("AstroSoft/Configs")]
        private static void ShowWindow()
        {
            GetWindow<ConfigsEditorWindow>("Configs Editor");
        }

        private void OnEnable()
        {
            asteroidConfigs = GetAllInstances<AsteroidConfig>();
            bulletConfigs = GetAllInstances<BulletConfig>();
            movementConfigs = GetAllInstances<MovementConfig>();
            gameConfigs = GetAllInstances<GameConfig>();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            DrawSliderBar(gameConfigs);
            DrawSliderBar(bulletConfigs);
            DrawSliderBar(movementConfigs);
            DrawSliderBar(asteroidConfigs);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

            if (!String.IsNullOrEmpty(selectedProperty))
            {
                for (var i = 0; i < asteroidConfigs.Length; i++)
                {
                    if (asteroidConfigs[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(asteroidConfigs[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);   
                    }
                }
                for (var i = 0; i < bulletConfigs.Length; i++)
                {
                    if (bulletConfigs[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(bulletConfigs[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);   
                    }
                }
                for (var i = 0; i < movementConfigs.Length; i++)
                {
                    if (movementConfigs[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(movementConfigs[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);   
                    }
                }
                for (var i = 0; i < gameConfigs.Length; i++)
                {
                    if (gameConfigs[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(gameConfigs[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);   
                    }
                }
            }
            else
                EditorGUILayout.LabelField("Select An Item From The List");
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            Apply();
        }

        protected void DrawSliderBar(ScriptableObject[] allConfigScriptables)
        {
            foreach (ScriptableObject config in allConfigScriptables)
            {
                if (GUILayout.Button(config.name))
                    selectedPropertyPath = config.name;
            }

            if (!String.IsNullOrEmpty(selectedPropertyPath))
                selectedProperty = selectedPropertyPath;
        }
        
        protected void DrawProperties(SerializedProperty p)
        {
            while (p.NextVisible(true))
                EditorGUILayout.PropertyField(p, true);
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[] a = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

        protected void Apply() => serializedObject?.ApplyModifiedProperties();
    }
}