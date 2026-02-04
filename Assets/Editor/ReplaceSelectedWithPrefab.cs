using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplaceSelectedWithPrefab
{
    [MenuItem("Tools/Replace Selected With Prefab")]
    static void Replace()
    {
        // Prefab trong Project
        GameObject prefab = Selection.activeObject as GameObject;

        if (prefab == null || !PrefabUtility.IsPartOfPrefabAsset(prefab))
        {
            Debug.LogError("❌ Hãy chọn Prefab trong Project trước!");
            return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogError("❌ Chưa chọn object nào trong Scene!");
            return;
        }

        foreach (GameObject oldObj in selectedObjects)
        {
            // Chỉ xử lý object thuộc Scene
            if (!oldObj.scene.IsValid())
                continue;

            GameObject newObj =
                (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            // Đưa prefab vào đúng scene
            SceneManager.MoveGameObjectToScene(newObj, oldObj.scene);

            // Giữ transform
            newObj.transform.SetParent(oldObj.transform.parent, true);
            newObj.transform.position = oldObj.transform.position;
            newObj.transform.rotation = oldObj.transform.rotation;
            newObj.transform.localScale = oldObj.transform.localScale;

            Undo.RegisterCreatedObjectUndo(newObj, "Replace With Prefab");
            Undo.DestroyObjectImmediate(oldObj);
        }
    }
}
