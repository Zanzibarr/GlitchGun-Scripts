using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{

    public bool isExplore = false;
    public GameObject bossFightCameraPosition;


    private int cameraIndex;

    private GameObject[] cameraSteps;

    private MoveBarrier glitch;
    private GameObject player;
    private Camera mainCamera;

    private GameManager gManager;

    private void Awake()
    {

        if (!isExplore)
        {
            cameraIndex = 0;

            cameraSteps = new GameObject[4];

            cameraSteps[0] = GameObject.Find("Camera Step_1");
            cameraSteps[1] = GameObject.Find("Camera Step_2");
            cameraSteps[2] = GameObject.Find("Camera Step_3");
            cameraSteps[3] = GameObject.Find("Camera Step_4");
        }
        
    }

    private void Start()
    {

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        player = GameObject.Find("Player");

        mainCamera = Camera.main;

        if (!isExplore)
        {

            glitch = GameObject.Find("Glitch").GetComponent<MoveBarrier>();

            mainCamera.transform.position = cameraSteps[cameraIndex++].transform.position;
            player.transform.position = new Vector2(-8, -5.95f);

            if (!gManager.IsEasy()) glitch.Begin();
            if (!gManager.IsHard()) glitch.SetSpeed(6f);
        } else
        {
            player.transform.position = new Vector2(0, -4.9f);
        }
    }

    private void Update()
    {
        
        if (isExplore)
        {

            Vector2 playerPosition = player.transform.position;

            if (playerPosition.x < 19.4f && playerPosition.x > -12.8f && playerPosition.y < 30.1f && playerPosition.y > 14.3f)
            {
                mainCamera.gameObject.transform.position = bossFightCameraPosition.transform.position;
                mainCamera.orthographicSize = 9.4f;
            } else
            {
                mainCamera.gameObject.transform.position = player.transform.position + new Vector3(0, 2.5f, -10);
                mainCamera.orthographicSize = 5;
            }
        }

    }

    public void MoveCamera()
    {

        mainCamera.transform.position = cameraSteps[cameraIndex++].transform.position;
        glitch.SetSpeed(glitch.GetSpeed()*1.2f);
        glitch.transform.position = new Vector2(glitch.transform.position.x - 1.5f, glitch.transform.position.y);

    }

}
