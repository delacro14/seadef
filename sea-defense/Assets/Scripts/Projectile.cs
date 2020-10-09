using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Ship target;
    private Tower parent;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }
    public void Init(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    private void MoveToTarget()
    {
        if(target!=null && target.isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);
            Vector2 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }
        else if(!target.isAlive)
        {
            Debug.Log("Destroying projectile");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Ship")
        {
            if(target.gameObject == other.gameObject)
            {
                target.TakeDamage(parent.Damage);
                myAnimator.SetTrigger("Hit");

                ApplyDebuff();
            }
        }
    }
    private void ApplyDebuff()
    {
        float roll = Random.Range(0,100);
        if(roll <= parent.Proc)
        {
            Debug.Log("Applying debuff");
            target.AddDebuff(parent.projectileType, parent.SlowFactor, parent.splashPrefab, parent.TickDamage, parent.TickTime, parent.DebuffDuration);
        }
    }
}
