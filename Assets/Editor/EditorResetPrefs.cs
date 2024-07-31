using UnityEditor;
using UnityEngine;

public class EditorResetPrefs : MonoBehaviour
{
    [MenuItem("Edit/Reset External Script Editor Preferences")]
    static void ResetExternalScriptEditorPrefs()
    {
        if (EditorUtility.DisplayDialog("Reset external script editor preferences?", "Reset external script editor preferences? This cannot be undone.", "Yes", "No"))
        {
            // Delete specific external script editor preferences
            EditorPrefs.DeleteKey("kScriptsDefaultApp");
            EditorPrefs.DeleteKey("kScriptEditorArgs");
            EditorPrefs.DeleteKey("kScriptEditorPath");
            EditorPrefs.DeleteKey("kScriptEditorArgsForMono");

            Debug.Log("External script editor preferences have been reset.");
        }
    }
}
