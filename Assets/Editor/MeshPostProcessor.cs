using UnityEditor;
using UnityEngine;

public class MeshPostProcessor : AssetPostprocessor
{
    void OnPostprocessModel(GameObject gameObject)
    {
        // Check if the asset being processed is a mesh
        if (assetImporter is ModelImporter importer)
        {
            ModelImporter modelImporter = (ModelImporter)assetImporter;

            // Set the read/write enabled flag for the imported meshes
            modelImporter.isReadable = true;

            // Apply the modifications
            modelImporter.SaveAndReimport();
        }
    }
}