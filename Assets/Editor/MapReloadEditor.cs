using Map_Folder;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    
    [CustomEditor(typeof(MapCreator))]
    public class MapReloadEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MapCreator myScript = (MapCreator)target;
            if (GUILayout.Button("刷新地图"))
            {
                myScript.ReloadMap();
            }
        }
    }
}