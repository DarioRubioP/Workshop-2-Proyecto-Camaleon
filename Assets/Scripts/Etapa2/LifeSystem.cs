using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [Header("Life Settings")]
    [SerializeField] private int maxLife = 3;
    [SerializeField] private int currentLife;

    [Header("UI References")]
    [SerializeField] private Text lifeText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Collision Settings")]
    [SerializeField] private string obstacleTag = "Obstaculo";

    [Header("Effects")]
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float immunityTime = 1f;

    private bool isImmune = false;
    private SpriteRenderer spriteRenderer;

    public HealthBar healthBar;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLife = maxLife;
        //UpdateLifeDisplay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (isImmune) return;

        currentLife--;

        if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);
        if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);

        //UpdateLifeDisplay();
        healthBar.UpdateHealth(currentLife, maxLife);

        if (currentLife <= 0)
        {
            GameOver(); 
        }
        else
        {
            StartCoroutine(ImmunityRoutine());
        }
    }

    private IEnumerator ImmunityRoutine()
    {
        isImmune = true;
        float elapsedTime = 0f;
        while (elapsedTime < immunityTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }
        spriteRenderer.enabled = true;

        isImmune = false;
    }

    /*private void UpdateLifeDisplay()
    {
        if (lifeText != null)
        {
            lifeText.text = $"Vidas: {currentLife}/{maxLife}";
        }
    }*/

    private void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            
        }
        gameObject.SetActive(false);

    }

    public void RestartGame()
    {
        currentLife = maxLife;
        //UpdateLifeDisplay();
        gameObject.SetActive(true);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
}