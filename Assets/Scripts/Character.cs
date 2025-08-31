using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour, ICombatant
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

    [field: Header("Character Name")]
    [field: SerializeField] public string CharacterName { get; protected set; }

    [Header("Level Up Amount")]
    [SerializeField] float maxXPScaling = 2f;

    [field: Header("Skills")]
    [field: SerializeReference] public List<IAttack> Attacks { get; protected set; } = new List<IAttack> { new BasicAttack() };
    [field: SerializeReference] public IAttack selectedAttack { get; protected set; }

    protected float lastDamageTaken = 0f;
     
    public float GetStat(StatType stat)
    {
        return stat switch
        {
            StatType.LVL => LVL,
            StatType.MaxHP => MaxHP,
            StatType.HP => HP,
            StatType.MaxMP => MaxMP,
            StatType.MP => MP,
            StatType.MaxXP => MaxXP,
            StatType.XP => XP,
            StatType.XPDrop => XPDrop,
            StatType.ATK => ATK,
            StatType.DEF => DEF,
            StatType.SPD => SPD,
            _ => 0
        };
    }

    public void IncreaseStat(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.LVL:
                LVL += amount;
                break;
            case StatType.MaxHP:
                MaxHP += amount;
                HP += amount;
                break;
            case StatType.HP:
                HP = Mathf.Min(HP + amount, MaxHP);
                break;
            case StatType.MaxMP:
                MaxMP += amount;
                MP += amount;
                break;
            case StatType.MP:
                MP = Mathf.Min(MP + amount, MaxMP);
                break;
            case StatType.MaxXP:
                MaxXP += amount;
                break;
            case StatType.XP:
                XP += amount;
                break;
            case StatType.XPDrop:
                XPDrop += amount;
                break;
            case StatType.ATK:
                ATK += amount;
                break;
            case StatType.DEF:
                DEF += amount;
                break;
            case StatType.SPD:
                SPD += amount;
                break;
            default:
                break;
        }
    }

    protected void InitializeStats()
    {
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

    public void SelectAttack(AttackType attackType)
    {
        for (int i = 0; i < Attacks.Count; i++)
        {
            if (Attacks[i].Type == attackType)
            {
                if (Attacks[i].MPCost < MP) selectedAttack = Attacks[i];
            }
        }
        OnSelectAttack();
    }

    public bool Attack(ICombatant target)
    {
        if (selectedAttack == null || target == null || !IsAlive()) return false;

        if (selectedAttack.MPCost > MP)
        {
            SelectAttack(AttackType.BasicAttack);
        }
        else
        {
            MP -= selectedAttack.MPCost;
        }
        selectedAttack.Execute(this, target);
        OnAttack();
        return true;
    }

    public void TakeDamage(float damage)
    {
        lastDamageTaken = Mathf.Max(damage, 0f);
        HP = Mathf.Max(HP - lastDamageTaken, 0f);
        OnTakeDamage();
        if (HP <= 0f) OnDeath();
    }

    public void GainXP(float experience)
    {
        XP += experience;
        OnXPGain();

        while (XP >= MaxXP)
        {
            ++LVL;
            XP -= MaxXP;
            MaxXP *= maxXPScaling;
            OnLevelUp();
        }
    }

    public bool IsAlive() => HP > 0f;
    
    void Awake()
    {
        InitializeStats();
        selectedAttack = Attacks[0];
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        OnDestroyed();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnSelectAttack() { }
    protected virtual void OnAttack() { }
    protected virtual void OnTakeDamage() { }
    protected virtual void OnXPGain() { }
    protected virtual void OnLevelUp() { }
    protected virtual void OnDeath() { }
    protected virtual void OnDestroyed() { }
    
}
