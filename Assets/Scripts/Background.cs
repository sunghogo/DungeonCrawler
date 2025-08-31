using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void HandleGameOver()
    {
        spriteRenderer.enabled = false;
    }

    void HandleGameSart()
    {
        spriteRenderer.enabled = true;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.OnGameOver += HandleGameOver;
        GameManager.OnGameStart += HandleGameSart;
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameSart;
    }
}
