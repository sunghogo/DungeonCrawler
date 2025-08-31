using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    public static LevelUpPanel Instance { get; private set; }

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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Deactivate();
    }
}
