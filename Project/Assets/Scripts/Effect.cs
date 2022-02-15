using UnityEngine;

[ExecuteInEditMode]
public class Effect : MonoBehaviour
{
    public Material BlurMaterial;
    [Range(0, 10)]
    public static int Iterations = 10;
    [Range(0, 4)]
    public static int DownRes = 2;

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        int width = src.width >> DownRes;
        int height = src.height >> DownRes;

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(src, rt);

        for (int i = 0; i < Iterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(rt, rt2, BlurMaterial);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Graphics.Blit(rt, dst);
        RenderTexture.ReleaseTemporary(rt);
    }
}
