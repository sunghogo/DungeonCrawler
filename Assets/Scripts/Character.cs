using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Starting Stats")]
    [SerializeField] protected float lvl = 1;
    [SerializeField] protected float maxHp = 20;
    [SerializeField] protected float maxMp = 10;
    [SerializeField] protected float maxXp = 10;
    [SerializeField] protected float xpGiven = 1;
    [SerializeField] protected float atk = 5;
    [SerializeField] protected float def = 5;
    [SerializeField] protected float spd = 5;

    public float LVL { get; protected set; }
    public float MAX_HP { get; protected set; } 
    public float HP { get; protected set; }
    public float MAX_MP { get; protected set; }
    public float MP { get; protected set; }
    public float MAX_XP { get; protected set; }
    public float XP { get; protected set; }
    public float XP_GIVEN { get; protected set; }
    public float ATK { get; protected set; }
    public float DEF { get; protected set; }
    public float SPD { get; protected set; }

    public float GetStat(StatType stat)
    {
        return stat switch
        {
            StatType.LVL      => LVL,
            StatType.MAX_HP   => MAX_HP,
            StatType.HP       => HP,
            StatType.MAX_MP   => MAX_MP,
            StatType.MP       => MP,
            StatType.MAX_XP   => MAX_XP,
            StatType.XP       => XP,
            StatType.XP_GIVEN => XP_GIVEN,
            StatType.ATK      => ATK,
            StatType.DEF      => DEF,
            StatType.SPD      => SPD,
            _ => 0
        };
    }

    void InitializeStats() {
        LVL = lvl;
        HP = MAX_HP = maxHp;
        MP = MAX_MP = maxMp; 
        MAX_XP = maxXp;
        XP = 0; 
        XP_GIVEN = xpGiven;
        ATK = atk;
        DEF = def; 
        SPD = spd;  
    }

    void Awake() {
        InitializeStats();
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

    protected abstract void OnStart();
    protected abstract void OnUpdate();
    protected abstract void OnAttack();
    protected abstract void OnTakeDamage();

    public bool IsAlive() => HP > 0;
}
