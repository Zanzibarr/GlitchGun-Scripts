using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrier : MonoBehaviour
{

    private float speed;

    private bool startCond;

    private void Awake()
    {

        speed = 5f;
        startCond = false;
        
    }

    private void Update()
    {

        if(startCond) transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    public void Begin()
    {
        startCond = true;
    }

    public void SetSpeed(float aSpeed)
    {

        speed = aSpeed;

    }

    public float GetSpeed()
    {

        return speed;

    }

}
