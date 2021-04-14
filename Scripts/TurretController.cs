using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public GameObject shot;

    /*Possibilmente privatizzare*/
    public LifeBar lifeBar;

    private bool canShoot;

    private GameObject player;

    private void Awake()
    {

        canShoot = true;

    }

    private void Start()
    {

        player = GameObject.Find("Player");

    }

    private void Update()
    {

        Vector2 playerPosition = player.transform.position;

        if (playerPosition.x < 19.4f && playerPosition.x > -12.8f && playerPosition.y < 30.1f && playerPosition.y > 14.3f)
        {

            if (!lifeBar.IsDead()) 
            {

                lifeBar.Show();

                Vector2 targetDir = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
                targetDir.Normalize();

                float rotationZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotationZ);

                StartCoroutine(Shoot());

            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            lifeBar.Hide();
        }

    }

    private IEnumerator Shoot()
    {

        if (canShoot)
        {
            canShoot = false;
            yield return new WaitForSeconds(0.5f);
            Instantiate(shot, transform.position, transform.rotation);
            canShoot = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Shot"))
        {
            if (!lifeBar.IsDead()) lifeBar.Hit();
        }

    }

}
