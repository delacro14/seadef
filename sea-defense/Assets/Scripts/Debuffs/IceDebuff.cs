using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : Debuff
{
    private float slowFactor;
    private bool applied;
    public IceDebuff(float slowFactor, float duration, Ship target) : base (target, 1)
    {
        this.slowFactor = slowFactor;
    }

    public override void Update()
    {
        if (target != null)
        {
            if(!applied)
            {
                applied = true;
                target.Speed -= (target.MaxSpeed * slowFactor) / 100; 
            }
        }
        base.Update();
    }
    public override void Remove()
    {
        target.Speed = target.MaxSpeed;
        base.Remove();
    }
}
