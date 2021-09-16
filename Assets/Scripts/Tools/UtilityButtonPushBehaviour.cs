using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This exists as a utility to make testing in scene easier
/// </summary>
public class UtilityButtonPushBehaviour : MonoBehaviour
{
    public bool InvokeInUpdate = false;
    public KeyCode Key;
    public UnityEvent ButtonPush;

    private void Update()
    {
        if (InvokeInUpdate || Input.GetKeyDown(Key))
            ButtonPush.Invoke();
    }

    /*
    [MenuItem("GameObject/Utility/Utility Button", false, 10)]
    static void CreateUtilityButton(MenuCommand menuCommand)
    {
        // Creat a new object
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/_Game/Level/Prefabs/Utility/UtilityButton.prefab", typeof(GameObject));
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // Ensure it gets reparented if this was a context click
        GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

        // Register creation in the undo system
        Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);

        // Select this object
        Selection.activeObject = obj;
    }
*/
}

#if UNITY_EDITOR
[CustomEditor(typeof(UtilityButtonPushBehaviour))]
class UtilityButtonPushBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This prefab exists to give an event and a button push to help test things in scene.", MessageType.Info);

        base.OnInspectorGUI();

        UtilityButtonPushBehaviour script = target as UtilityButtonPushBehaviour;

        if (GUILayout.Button("Invoke Button Push"))
            script.ButtonPush.Invoke();
    }
}
#endif