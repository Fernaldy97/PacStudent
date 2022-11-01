using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public float speed = 1.0f;

    private Vector2 direction = Vector2.up;
    private float directionChangeTime = 0.0f;

    private Rigidbody2D rb2d;
    private CircleCollider2D cc2d;

   
    void Start()
    {
        //Get component for both rigidbody 2d and circle collider 2d
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //Change direction
        if (!openDirection(direction))
        {
            if (ableToChange())
            {
                changeDirection();
            }
            else if (rb2d.velocity.magnitude < speed)
            {
                changeRandomly();
            }
        }
        else if (ableToChange() && Time.time > directionChangeTime)
        {
            changeRandomly();
        }
        else if (rb2d.velocity.magnitude < speed)
        {
            changeRandomly();
        }

        //Ghost movement
        rb2d.velocity = direction * speed;
        if (rb2d.velocity.x == 0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        }
        if (rb2d.velocity.y == 0)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
        }
    }

    private bool openDirection(Vector2 direction)
    {
        RaycastHit2D[] r2ds = new RaycastHit2D[10];
        cc2d.Cast(direction, r2ds, 1f, true);
        foreach (RaycastHit2D r2d in r2ds)
        {
            if (r2d && r2d.collider.gameObject.tag == "Impassable")
            {
                return false;
            }
        }
        return true;
    }

    private bool ableToChange()
    {   
        //Change direction randomly
        Vector2 perpRight = Utility.Right(direction);
        bool openRight = openDirection(perpRight);
        Vector2 perpLeft = Utility.Left(direction);
        bool openLeft = openDirection(perpLeft);
        return openRight || openLeft;
    }

    private void changeRandomly()
    {
        //Unable to change direction for a second
        directionChangeTime = Time.time + 1;
        if (Random.Range(0, 2) > 0)
        {
            changeDirection();
        }
    }

    private void changeDirection()
    {
        //Unable to change direction for a second
        directionChangeTime = Time.time + 1;
        Vector2 perpRight = Utility.Right(direction);
        bool openRight = openDirection(perpRight);
        Vector2 perpLeft = Utility.Left(direction);
        bool openLeft = openDirection(perpLeft);
        if (openRight || openLeft)
        {
            int choice = Random.Range(0, 2);
            if (!openLeft || (choice == 0 && openRight))
            {
                direction = perpRight;
            }
            else
            {
                direction = perpLeft;
            }
        }
        else
        {
            direction = -direction;
        }
    }

    //Player
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector2(0, 0);
        }
    }
}
