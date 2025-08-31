using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;
    [SerializeField] float dropSpeed = 2f;

    TMP_Text tmp;

    public void SetText(string value)
    {
        tmp.text = value;
    }

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        GameManager.OnGameOver += HandleGameOver;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
    }

    void HandleGameOver()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(0f, -1f * dropSpeed * Time.fixedDeltaTime, 0f);
    }
}
