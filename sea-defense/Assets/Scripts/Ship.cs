using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public bool isAlive = true;
    [SerializeField]
    private Stat health;
    private Animator myAnimator;
    [SerializeField] 
    private float speed = 2f;
    public float MaxSpeed { get; set; }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            this.speed = value;
        }
    }
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsRemoving = new List<Debuff>();
    private List<Debuff> newDebuffs = new List<Debuff>();

    private void Awake()
    {
        MaxSpeed = speed;
        myAnimator = GetComponent<Animator>();
        health.Initialize();
    }
    private void Update()
    {
        DebuffHandler();
    }
    public void Spawn(int health)
    {
        transform.position = new Vector3(GameObject.Find("Start Spawn").transform.position.x, GameObject.Find("Start Spawn").transform.position.y, GameObject.Find("Start Spawn").transform.position.z);
        this.health.MaxVal = health;
        this.health.CurrentValue = this.health.MaxVal;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "End Spawn")
        {
            GameObject.FindObjectOfType<GameManager>().Lives--;
        }
    }
    public void DestroyShip()
    {
        Destroy(gameObject);
        isAlive = false;
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        gm.removeShip(this);
    }
    public void TakeDamage(float damage)
    {
        if(isAlive)
        {
            health.CurrentValue-= damage;
        }
        if(health.CurrentValue <= 0)
        {
            isAlive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindObjectOfType<GameManager>().Gold+=25;
            Debug.Log("Calling animator for die");
            GameObject.FindObjectOfType<SoundManager>().PlaySFX("Swoosh");
            myAnimator.SetTrigger("Die");
            GetComponent<SpriteRenderer>().sortingOrder--;
        }
    }
    public void AddDebuff(string debuff, float slowFactor, PoisonSplash splashPrefab, float tickDamage, float tickTime, float duration)
    {
        Debuff temp;
        switch(debuff)
        {
            case "fire":
                temp = new FireDebuff(tickDamage, tickTime, duration, this);
                break;
            case "ice":
                temp = new IceDebuff(slowFactor, duration, this);
                break;
            case "storm":
                temp = new StormDebuff(this, duration);
                break;
            case "poison":
                temp = new PoisonDebuff(tickDamage, tickTime, splashPrefab, duration, this);
                break;
            default:
                Debug.Log("Debuff error");
                temp = new FireDebuff(tickDamage, tickTime, duration, this);
                break;
        }
        if(!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            newDebuffs.Add(temp);
        }
    }
    public void RemoveDebuff(Debuff debuff)
    {
        debuffsRemoving.Add(debuff);
    }
    private void DebuffHandler()
    {
        if(newDebuffs.Count >0)
        {
            debuffs.AddRange(newDebuffs);
            newDebuffs.Clear();
        }
        foreach (Debuff debuff in debuffsRemoving)
        {
            debuffs.Remove(debuff);
        }
        debuffsRemoving.Clear();
        foreach (Debuff debuff in debuffs)
        {
            //Debug.Log(debuff.GetType());
            debuff.Update();
        }
    }
}
