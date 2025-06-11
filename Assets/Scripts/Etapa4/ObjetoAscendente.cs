using UnityEngine;

public class ObjetoAscendente : MonoBehaviour
{
    public float velocidadSubida = 2f;
    public Sprite[] spriteArr;
    public int spriteCount;
    public SpriteRenderer s_Renderer;

    

    private void Awake()
    {
        spriteCount = Random.Range(0, 10);
        Imagenes();
        Debug.Log(spriteCount);
    }
    void Update()
    {
        transform.position += Vector3.up * velocidadSubida * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    } 

    void Imagenes()
    {
        if (spriteCount <= 5)
        {
            s_Renderer.sprite = spriteArr[0];
        }
        else if (spriteCount >= 6)
        {
            s_Renderer.sprite = spriteArr[1];
        }
    }
}

