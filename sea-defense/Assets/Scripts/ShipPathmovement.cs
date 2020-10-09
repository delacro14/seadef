using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPathmovement : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;

    int waypointIndex = 0;
    bool isSet = false;
    [SerializeField] int shiporder;

    //declare Animator variable;
    private Animator animator;
    public GridPoint shipPoint;
    private float movespeed;

    private levelLayoutManager lm;
    
    void Start()
    {
        lm = GameObject.FindObjectOfType<levelLayoutManager>();
        animator = this.GetComponent<Animator>();
        foreach (var item in lm.nodes)
        {
            var spe = new GridPoint(item.x,item.y);
            waypoints.Add(lm.Tiles[spe].gameObject);
        }
    }
    void Update()
    {
        movespeed = gameObject.GetComponent<Ship>().Speed;
        Move();
    }
    // Update is called once per frame
    private void Move()
    {
        if(gameObject.GetComponent<Ship>().isAlive)
        {
            if (waypointIndex <= waypoints.Count -1)
            {
                var targetPosition = new Vector3(waypoints[waypointIndex].GetComponent<GridTile>().worldPos.x,waypoints[waypointIndex].GetComponent<GridTile>().worldPos.y,0);
                var movementThisFrame = movespeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
                Animate(transform.position, targetPosition);
                if(transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                this.GetComponent<Ship>().DestroyShip();
            }
        }
    }
    public void Animate(Vector3 currentPos, Vector3 newPos)
    {
        if(currentPos.y > newPos.y)
        {
            animator.SetInteger("Horizontal",0);
            animator.SetInteger("Vertical",1);
        }
        else if(currentPos.y < newPos.y)
        {
            animator.SetInteger("Horizontal",0);
            animator.SetInteger("Vertical",-1);
        }
        else if(currentPos.y == newPos.y)
        {
            if(currentPos.x > newPos.x)
            {
            animator.SetInteger("Horizontal",-1);
            animator.SetInteger("Vertical",0);
            }
            else if(currentPos.x < newPos.x)
            {
            animator.SetInteger("Horizontal",1);
            animator.SetInteger("Vertical",0);
            }
        }

    }




}
