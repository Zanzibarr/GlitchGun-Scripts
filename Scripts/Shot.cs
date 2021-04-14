using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public bool isNormal;
    public bool isTime;
    public bool isTurrettShot = false;

    private float speed;

    private void Awake()
    {

        speed = 25f;

    }

    private void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Glitch_Platform") && isNormal)
        {

            collision.gameObject.GetComponent<Animator>().SetBool("Destroy_Animation", true);

        }
        else if (collision.CompareTag("Move_Platform") && isTime)
        {

            MovingPlatform wall = collision.GetComponent<MovingPlatform>();

            if (wall.isMoving) wall.GenerateWall();
            else wall.StartMoving();

        }

        if (IsObstacle(collision))
        {
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private bool IsObstacle(Collider2D collision)
    {

        bool ret;

        ret = collision.CompareTag("Glitch_Platform");
        ret |= collision.CompareTag("Move_Platform");
        ret |= collision.CompareTag("Jump_Platform");
        ret |= collision.CompareTag("Platform");
        ret |= collision.CompareTag("Exit_Door");
        ret |= (collision.CompareTag("Enemy") && !isTurrettShot);

        return ret;

    }

}
