using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Starting Stats")]
    [SerializeField] protected float startingLVL = 1;
    [SerializeField] protected float startingMaxHP = 20;
    [SerializeField] protected float startingMaxMP = 10;
    [SerializeField] protected float startingMaxXP = 10;
    [SerializeField] protected float startingXPDrop = 1;
    [SerializeField] protected float startingATK = 5;
    [SerializeField] protected float startingDEF = 5;
    [SerializeField] protected float startingSPD = 5;

    [field: Header("Current Stats")]
    [field: SerializeField] public float LVL { get; protected set; }
    [field: SerializeField] public float MaxHP { get; protected set; } 
    [field: SerializeField] public float HP { get; protected set; }
    [field: SerializeField] public float MaxMP { get; protected set; }
    [field: SerializeField] public float MP { get; protected set; }
    [field: SerializeField] public float MaxXP { get; protected set; }
    [field: SerializeField] public float XP { get; protected set; }
    [field: SerializeField] public float XPDrop { get; protected set; }
    [field: SerializeField] public float ATK { get; protected set; }
    [field: SerializeField] public float DEF { get; protected set; }
    [field: SerializeField] public float SPD { get; protected set; }

    public float GetStat(StatType stat)
    {
        return stat switch
        {
            StatType.LVL      => LVL,
            StatType.MaxHP   => MaxHP,
            StatType.HP       => HP,
            StatType.MaxMP   => MaxMP,
            StatType.MP       => MP,
            StatType.MaxXP   => MaxXP,
            StatType.XP       => XP,
            StatType.XPDrop => XPDrop,
            StatType.ATK      => ATK,
            StatType.DEF      => DEF,
            StatType.SPD      => SPD,
            _ => 0
        };
    }

    void InitializeStats() {
        LVL = startingLVL;
        HP = MaxHP = startingMaxHP;
        MP = MaxMP = startingMaxMP; 
        MaxXP = startingMaxXP;
        XP = 0; 
        XPDrop = startingXPDrop;
        ATK = startingATK;
        DEF = startingDEF; 
        SPD = startingSPD;  
    }

    void Awake()
    {
        InitializeStats();
        OnAwake();
    }

    void Start() {
        OnStart();
    }

    void Update() {
        OnUpdate();
    }

    public virtual void Attack(Character target) {
        target.TakeDamage(ATK);
        OnAttack();
    }

    public virtual void TakeDamage(float ATK)
    {
        float dmg =  Mathf.Max(ATK - DEF, 0);
        HP = Mathf.Max(HP - dmg, 0);
        OnTakeDamage();
    }

    protected abstract void OnAwake();
    protected abstract void OnStart();
    protected abstract void OnUpdate();
    protected abstract void OnAttack();
    protected abstract void OnTakeDamage();

    public bool IsAlive() => HP > 0;
}
