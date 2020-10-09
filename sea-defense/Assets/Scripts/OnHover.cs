using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer rangeRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rangeRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(spriteRenderer.enabled)
        {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    public void Activate(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
        rangeRenderer.enabled = true;

    }
    public void Deativate()
    {
        spriteRenderer.enabled = false;
        rangeRenderer.enabled = false;
        GameObject.FindObjectOfType<GameManager>().clicked = null;
    }
}
