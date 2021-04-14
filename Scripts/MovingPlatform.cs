using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public GameObject wall;
    public GameObject wallHorizontal;

    public bool isMovingHorizontal;
    public bool isRotating;

    public bool isHorizontal;
    public bool isMoving;

    public float speed;
    public float bound;
    public float offset;

    public int direction = 1;

    private float edgeTR, edgeBL;
    private Vector2 initialPosition;

    private CurrentSceneManager csManager;

    private void Awake()
    {

        initialPosition = transform.position;

        if (isMovingHorizontal)
        {
            edgeTR = initialPosition.x - offset + bound;
            edgeBL = initialPosition.x - offset - bound;
        }
        else
        {
            edgeTR = initialPosition.y - offset + bound;
            edgeBL = initialPosition.y - offset - bound;
        }

    }

    private void Start()
    {

        csManager = GameObject.Find("Manager").GetComponent<CurrentSceneManager>();

    }

    void Update()
    {

        if (isMoving)
        {
            if (isMovingHorizontal)
            {

                if (!(direction > 0 && transform.position.x < edgeTR || direction < 0 && transform.position.x > edgeBL)) direction *= -1;

                transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

            }
            else if (!isRotating)
            {

                if (!(direction > 0 && transform.position.y < edgeTR || direction < 0 && transform.position.y > edgeBL)) direction *= -1;

                transform.Translate(Vector2.up * direction * speed * Time.deltaTime);

            }
            else
            {
                transform.Rotate(Vector3.forward * direction * speed * Time.deltaTime);
            }
        }
        
    }

    public void GenerateWall()
    {

        if (!isHorizontal) Instantiate(wall, transform.position, transform.rotation, csManager.gameObject.transform);
        else Instantiate(wallHorizontal, transform.position, transform.rotation, csManager.gameObject.transform);
        Destroy(gameObject);

    }

    public void StartMoving()
    {

        isMoving = true;

    }

}
