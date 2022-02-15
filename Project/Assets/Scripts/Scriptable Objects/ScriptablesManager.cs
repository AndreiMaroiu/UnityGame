using UnityEngine;

public static class ScriptablesManager
{
    public static T[] GetAllScriptables<T>()
    {
        return Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
    }
}
