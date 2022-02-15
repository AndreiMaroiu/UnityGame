using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class MakePlanetObject
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Scriptables/Planet Object")]
    public static void Create()
    {
        PlanetObject asset = ScriptableObject.CreateInstance<PlanetObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Skins/NewPlanetObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

    }
#endif
}
