using UnityEditor;
using UnityEngine;

//Throw this inside YourGame/Assets/Editor/Importer.cs
public class Importer : AssetPostprocessor
{

    const int PostProcessOrder = 0;

    public override int GetPostprocessOrder()
    {
        return PostProcessOrder;
    }

    void OnPreprocessTexture()
    {
        var textureImporter = assetImporter as TextureImporter;

        textureImporter.filterMode = FilterMode.Point;
        //textureImporter.filterMode = FilterMode.Bilinear;
        //textureImporter.filterMode = FilterMode.Trilinear;

        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        //textureImporter.textureCompression = TextureImporterCompression.Compressed;
        //textureImporter.textureCompression = TextureImporterCompression.CompressedLQ;
        //textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;

        //textureImporter.spritePixelsPerUnit = 16;
        //textureImporter.spritePixelsPerUnit = 32;
        //textureImporter.spritePixelsPerUnit = 64;
    }
}
