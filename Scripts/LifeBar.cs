using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    /*Possibilmente privatizzare*/
    public GameObject[] life;

    private int health;

    private void Awake()
    {

        Hide();

        health = life.Length;

    }

    public void Show()
    {

        for (int index = 0; index < health; index++)
        {
            life[index].SetActive(true);
        }

    }

    public void Hide()
    {

        for (int index = 0; index < health; index++)
        {
            life[index].SetActive(false);
        }

    }

    public void Hit()
    {

        if (health > 0)
        {
            health--;
            Destroy(life[health]);
        }

    }

    public bool IsDead()
    {

        return health < 1;

    }

}
