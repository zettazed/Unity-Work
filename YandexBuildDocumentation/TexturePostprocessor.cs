// #define TEXTURE_POSTPROCESS
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class TexturePostprocessor : AssetPostprocessor {

    void OnPostprocessTexture(Texture2D texture)
    {
//#if TEXTURE_POSTPROCESS
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.mipmapEnabled = false;
        textureImporter.textureCompression = TextureImporterCompression.Compressed;
        textureImporter.crunchedCompression = true;
        textureImporter.compressionQuality = 50;
//#endif
    }

}
#endif