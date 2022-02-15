using UnityEngine;

public class Fading : MonoBehaviour 
{
    public Texture2D texture;
    public float speed = 0.8f;

    private const int drawDepth = -1000;
    private float alpha = 1f;
    private float fadeDir = -1;
    
	private void OnGUI()
    {
        alpha += fadeDir * speed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }

    public float BeginFade(float direction)
    {
        fadeDir = direction;
        return speed;
    }

    private void LoadLevel()
    {
        BeginFade(-0.5f);
    }
}
