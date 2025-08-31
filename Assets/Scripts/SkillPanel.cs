using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public static SkillPanel Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject subcontainer;

    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        subcontainer.SetActive(true);
        IsActive = true;
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

    void HandleGameSart()
    {
        Activate();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GameManager.OnGameOver += HandleGameOver;
        GameManager.OnGameStart += HandleGameSart;
    }

    void OnDestroy()
    {
        GameManager.OnGameOver -= HandleGameOver;
        GameManager.OnGameStart -= HandleGameSart;
    }
}
