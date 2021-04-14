using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject normalShot;
    public GameObject timeShot;
    public bool isNormal;
    public bool isTime;

    private GameObject csManager;

    private void Start()
    {

        csManager = GameObject.Find("Manager");

    }

    private void Update()
    {

        SpriteRenderer gunSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        targetDir.Normalize();

        float rotationZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(rotationZ) > 95) gunSpriteRenderer.flipY = true;
        else if (Mathf.Abs(rotationZ) < 85) gunSpriteRenderer.flipY = false;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        gameObject.GetComponentInChildren<GunCharacteristics>().gameObject.transform.localPosition = new Vector2(.5f, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (isNormal) Instantiate(normalShot, transform.position, transform.rotation, csManager.transform);
            else if (isTime) Instantiate(timeShot, transform.position, transform.rotation, csManager.transform);
        }

    }

}
