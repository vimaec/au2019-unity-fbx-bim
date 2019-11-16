using UnityEditor;
using UnityEngine;

public class FBXCustomProperties : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        var modelImporter = assetImporter as ModelImporter;

        // Add colliders 
        modelImporter.addCollider = true;
    }

    void OnPostprocessModel(GameObject g)
    {
        // Disable imported cameras (otherwise you will have to do it manually)
        DisableCamera(g);
        MakeStatic(g);
    }

    void DisableCamera(GameObject g)
    {
        var camera = g.GetComponent<Camera>();
        if (camera != null)
            camera.enabled = false;
        foreach (Transform c in g.transform)
            DisableCamera(c.gameObject);
    }

    void MakeStatic(GameObject g)
    {
        g.isStatic = true;
        foreach (Transform c in g.transform)
            MakeStatic(g);
    }

    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] names, object[] values)
    {
        if (values.Length > 0)
        {
            // The game object will be given a "BimData" component, 
            // which is class we have defined 
            var bimData = go.AddComponent<BimData>();

            // The first value will be the BIM data if imported from 3ds Max. 
            // Usually the corresponding name is something like `3DSUDP` for 
            // 3ds Max User Defined Property.
            var tmp = values[0].ToString();
            bimData.Items = tmp.Split('\n');                        
        }
    }
}
