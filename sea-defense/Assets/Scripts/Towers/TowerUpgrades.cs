using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades
{
    public int Price {get; private set;}
    public float Damage {get; private set;}
    public float Range {get; private set;}
    public float Proc {get; private set; }    
    public float DebuffDuration {get; private set; }
    public float TickDamage {get; private set; }
    public float SlowingFactor {get; private set; }

    public TowerUpgrades(int price, float damage, float range, float proc, float debuffDuration, float tickDamage, float slowingFactor)
    {
        this.Price = price;
        this.Damage = damage;
        this.Range = range;
        this.Proc = proc;
        this.DebuffDuration = debuffDuration;
        this.TickDamage = tickDamage;
        this.SlowingFactor = slowingFactor;
    }

}
