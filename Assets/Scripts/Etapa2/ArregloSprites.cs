using UnityEngine;

public class ArregloSprites : MonoBehaviour
{
    int spriteCount;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArr;
    private void Awake()
    {
        spriteCount = Random.Range(0, 10);
        Imagenes();
        Debug.Log(spriteCount);
    }

    void Imagenes()
    {
        if (spriteCount <= 5)
        {
            spriteRenderer.sprite = spriteArr[0];
        }
        else if (spriteCount >= 6)

        {
            spriteRenderer.sprite = spriteArr[1];
        }
    }
}
