using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSplash : MonoBehaviour
{
    public float Damage { get; set; }
    private float maxTime = 5;
    private float time = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ship")
        {
            other.GetComponent<Ship>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(time >= maxTime)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }
}
