using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    private float tickTime;
    private float timeSinceTick;
    private PoisonSplash splash;

    private float splashDamage;
    public PoisonDebuff(float splashDamage, float tickTime, PoisonSplash splashPrefab, float duration, Ship target) : base (target, 1)
    {
        this.splashDamage = splashDamage;
        this.splash = splashPrefab;
        this.tickTime = tickTime;
    }
    public override void Update()
    {
        if(target != null)
        {
            timeSinceTick += Time.deltaTime;
            if(timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                Splash();
            }
        }
        base.Update();
    }
    private void Splash()
    {
        PoisonSplash tmp = GameObject.Instantiate(splash, target.transform.position, Quaternion.identity);
        tmp.Damage = splashDamage;
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), tmp.GetComponent<Collider2D>());
    }
}
