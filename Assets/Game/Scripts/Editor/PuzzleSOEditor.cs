using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.ScriptableObjects.Puzzles;

// referencia
// https://github.com/UnityTechnologies/open-project-1/blob/devlogs/2-scriptable-objects/UOP1_Project/Assets/Scripts/Editor/GameSceneSOEditor.cs

namespace EditorScritps
{
    [CustomEditor(typeof(PuzzleSO), true)]
    public class PuzzleSOEditor : Editor
    {
        private const string NO_SCENES_WARNING = "There is no Scene associated to this location yet. Add a new scene with the dropdown below";
        private GUIStyle _headerLabelStyle;
        private static readonly string[] _excludedProperties = { "m_Script", "sceneName" };

        private string[] _sceneList;
        private PuzzleSO _gameSceneInspected;

        private void OnEnable()
        {
            _gameSceneInspected = target as PuzzleSO;
            PopulateScenePicker();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();

            DrawPropertiesExcluding(serializedObject, _excludedProperties);

            EditorGUILayout.Space();

            DrawScenePicker();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawScenePicker()
        {
            var sceneName = _gameSceneInspected.SceneName;
            EditorGUI.BeginChangeCheck();
            var selectedScene = _sceneList.ToList().IndexOf(sceneName);

            if (selectedScene < 0)
            {
                EditorGUILayout.HelpBox(NO_SCENES_WARNING, MessageType.Warning);
            }

            selectedScene = EditorGUILayout.Popup("Scene", selectedScene, _sceneList);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed selected scene");
                _gameSceneInspected.SceneName = _sceneList[selectedScene];
                // MarkAllDirty();
            }
        }

        /// <summary>
        /// Populates the Scene picker with Scenes included in the game's build index
        /// </summary>
        private void PopulateScenePicker()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            _sceneList = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                _sceneList[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }

        /// <summary>
        /// Marks scenes as dirty so data can be saved
        /// </summary>
        private void MarkAllDirty()
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkAllScenesDirty();
        }
    }
}