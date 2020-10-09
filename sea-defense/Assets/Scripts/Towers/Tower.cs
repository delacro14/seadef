using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tower : MonoBehaviour
{
    [SerializeField]
    public string projectileType;
    private SpriteRenderer mySprite;
    private GameObject parent;

    public PoisonSplash splashPrefab;
    private Ship target;

    [SerializeField]
    public Ship Target
    {
        get {return target;}
    }
    private Queue<Ship> ships = new Queue<Ship>();
    private bool canAttack = true;

    public TowerUpgrades[] Upgrades {get; set; }

    [SerializeField]
    private float tickTime;
    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get { return tickTime; }
    }
    public float TickDamage
    {
        get { return tickDamage; }
    }
    [SerializeField]
    private float slowFactor;
    public float SlowFactor
    {
        get { return slowFactor; }
    }
    [SerializeField]
    private float damage;
    public float Damage
    {
        get{
            return damage;
        }
    }
    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }
        set
        {
            this.debuffDuration = value;
        }
    }
    public float Proc
    {
        get
        {
            return proc;
        }
        set
        {
            this.proc = value;
        }
    }
    public float FiringRange{
        get
        {
            return firingRange;
        }
    }
    public float AttackCooldown{ get {return attackCooldown;} }
    private float attackRate;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private float projectileSpeed = 3f;
    [SerializeField]
    private float firingRange = 3f;
    [SerializeField]
    private float debuffDuration;
    [SerializeField]
    private float proc;
    public float ProjectileSpeed
    {
        get{ return projectileSpeed; }
    }
    private Animator myAnimator;

    public string getStats()
    {
        if(NextUpgrade != null)
        {
            switch(projectileType)
            {
                case "fire":
                    return string.Format("<color=#ffa500ff><size=18><b>Fire Tower</b></size></color>  <color=#BA352B>(Level {0})</color>\nDamage: {1} <color=#00ff00ff>+{7}</color> \n Attack Speed: {2}\nRange: {3} <color=#00ff00ff>+{8}</color>\n-------------\nDebuff chance: {4}% <color=#00ff00ff>+{9}%</color> \nDebuff duration: {5}sec <color=#00ff00ff>+{10}sec</color> \nDebuff damage: {6} \n <color=#cb3b3b><size=16><b><i>chance to burn the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, TickDamage, NextUpgrade.Damage, NextUpgrade.Range, NextUpgrade.Proc, NextUpgrade.DebuffDuration);
                case "ice":
                    return string.Format("<color=#00ffffff><size=18><b>Ice Tower</b></size></color>  <color=#BA352B>(Level {0})</color>\nDamage: {1} <color=#00ff00ff>+{7}</color> \nAttack Speed: {2}\nRange: {3}  <color=#00ff00ff>+{8}</color>\n-------------\nDebuff chance: {4}% <color=#00ff00ff>+{9}%</color> \nDebuff duration: {5}sec\nSlow %: {6} <color=#00ff00ff>+{10}%</color>\n <color=#2EA3D1><size=16><b><i>chance to slow the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, SlowFactor, NextUpgrade.Damage, NextUpgrade.Range, NextUpgrade.Proc, NextUpgrade.SlowingFactor);
                case "poison":
                    return string.Format("<color=#00ff00ff><size=18><b>Poison Tower</b></size></color>  <color=#BA352B>(Level {0})</color>\nDamage: {1} <color=#00ff00ff>+{7}</color> \n Attack Speed: {2}\nRange: {3} <color=#00ff00ff>+{8}</color>\n-------------\nDebuff chance: {4}% <color=#00ff00ff>+{9}%</color>\nDebuff duration: {5}sec\nSplash damage: {6} <color=#00ff00ff>+{10}</color>\n <color=#239822><size=16><b><i>chance to drop a splash</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, TickDamage, NextUpgrade.Damage, NextUpgrade.Range, NextUpgrade.Proc, NextUpgrade.TickDamage);    
                case "storm":
                    return string.Format("<color=#add8e6ff><size=18><b>Storm Tower</b></size></color>  <color=#BA352B>(Level {0})</color>\nDamage: {1} <color=#00ff00ff>+{6}</color> \n Attack Speed: {2}\nRange: {3} <color=#00ff00ff>+{7}</color>\n-------------\nDebuff chance: {4}% <color=#00ff00ff>+{8}%</color>\nStun duration: {5}sec <color=#00ff00ff>+{9}sec</color>\n <color=#D2AB2F><size=16><b><i>chance to stun the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, NextUpgrade.Damage, NextUpgrade.Range, NextUpgrade.Proc, NextUpgrade.DebuffDuration);                
                default:
                    return null;
            }
        }
        switch(projectileType)
            {
                case "fire":
                    return string.Format("<color=#ffa500ff><size=18><b>Fire Tower</b></size></color>  <color=#BA352B>Level {0}</color>\nDamage: {1} \n Attack Speed: {2}\nRange: {3}\n-------------\nDebuff chance: {4}%\nDebuff duration: {5}sec\nDebuff damage: {6}\n <color=#cb3b3b><size=16><b><i>chance to burn the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, TickDamage);
                case "ice":
                    return string.Format("<color=#00ffffff><size=18><b>Ice Tower</b></size></color>  <color=#BA352B>Level {0}</color>\nDamage: {1} \nAttack Speed: {2}\nRange: {3}\n-------------\nDebuff chance: {4}%\nDebuff duration: {5}sec\nSlow %: {6}\n <color=#2EA3D1><size=16><b><i>chance to slow the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, SlowFactor);
                case "poison":
                    return string.Format("<color=#00ff00ff><size=18><b>Poison Tower</b></size></color>  <color=#BA352B>Level {0}</color>\nDamage: {1} \n Attack Speed: {2}\nRange: {3}\n-------------\nDebuff chance: {4}%\nDebuff duration: {5}sec\nSplash damage: {6}\n <color=#239822><size=16><b><i>chance to drop a splash</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration, TickDamage);    
                case "storm":
                    return string.Format("<color=#add8e6ff><size=18><b>Storm Tower</b></size></color>  <color=#BA352B>Level {0}</color>\nDamage: {1} \n Attack Speed: {2}\nRange: {3}\n-------------\nDebuff chance: {4}%\nStun duration: {5}sec\n <color=#D2AB2F><size=16><b><i>chance to stun the ship</i></b></size></color>", Level, Damage, AttackCooldown, FiringRange, Proc, DebuffDuration);                
                default:
                    return null;
            }
    }
    public TowerUpgrades NextUpgrade
    {
        get
        {
            if(Upgrades.Length > Level-1)
            {
                return Upgrades[Level-1];
            }
            return null;
        }
    }
    public int Level {get; set;}

    public int Price { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        Upgrades = new TowerUpgrades[]
        {
            new TowerUpgrades((this.Price/4),2,0.5f,10,0.1f,1,10),
            new TowerUpgrades((this.Price/3),3,0.5f,10,0.1f,1,15),
            new TowerUpgrades((this.Price/2),4,0.5f,10,0.1f,1,20)
        };
        myAnimator = transform.parent.GetComponent<Animator>();
        parent = this.transform.parent.gameObject;
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        this.GetComponent<Transform>().localScale = new Vector3(firingRange*2,firingRange*2,0);
    }
    public void Select()
    {
        mySprite.enabled = !mySprite.enabled;
        GameObject.FindObjectOfType<GameManager>().updateTooltip();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ship")
        {
            ships.Enqueue(other.GetComponent<Ship>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ship")
        {
            target = null;
        }
    }
    private void Attack()
    {
        if(!canAttack)
        {
            attackRate += Time.deltaTime;
            if(attackRate >= attackCooldown)
            {
                canAttack = true;
                attackRate = 0;
            }
        }
        if(target==null && ships.Count > 0)
        {
            target = ships.Dequeue();
        }
        if(target!=null && target.isAlive)
        {
            if(canAttack)
            {
                Shoot();
                myAnimator.SetTrigger("Attack");
                canAttack = false;
            }
        }
        if (target != null && !target.isAlive)
        {
           target = null; 
        }
    }
    private void Shoot()
    {
        Projectile projectile = GameObject.FindObjectOfType<GameManager>().Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;

        projectile.Init(this);
    }
    public void Upgrade()
    {
        GameObject.FindObjectOfType<GameManager>().Gold -= NextUpgrade.Price;
        Price += NextUpgrade.Price; 

        this.damage += NextUpgrade.Damage;
        this.firingRange += NextUpgrade.Range;
        this.proc += NextUpgrade.Proc;
        this.debuffDuration += NextUpgrade.DebuffDuration;
        this.tickDamage += NextUpgrade.TickDamage;
        this.slowFactor += NextUpgrade.SlowingFactor;
        Level++;
        GameObject.FindObjectOfType<GameManager>().updateTooltip();
    }
}
