using UnityEngine;

public static class MaterialUtils
{
    //https://www.youtube.com/watch?v=YCi4BnnQV2s
    public enum BlendMode
    {
        Opaque,
        Transparent
    }

    public static void SetupBlendMode(Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            //Sets the blendmode of the renderer to transparent
            case BlendMode.Transparent:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                material.SetFloat("_Mode", 3.0f);
                break;
            //Sets the blendmode of the renderer to Opaque
            case BlendMode.Opaque:
                material.SetOverrideTag("RenderType", "");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                material.SetFloat("_Mode", 0.0f);
                break;
            default:
                Debug.LogWarning("Warning: BlendMode: " + blendMode + " is not yet implemented!");
                break;
        }
    }
}
