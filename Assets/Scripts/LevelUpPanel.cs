using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    public static LevelUpPanel Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject subcontainer;

    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        if (GameManager.Instance.GameStart)
        {
            subcontainer.SetActive(true);
            IsActive = true;
        }
    }

    public void Deactivate()
    {
        subcontainer.SetActive(false);
        IsActive = false;
    }

    void HandleGameOver()
    {
        Deactivate();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Deactivate();
        GameManager.OnGameOver += HandleGameOver;

    }
    
    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
    }
}
