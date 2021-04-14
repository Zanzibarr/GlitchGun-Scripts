using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject normalGun;
    public GameObject timeGun;

    public LayerMask groundMask;


    private float speed;
    private float jumpSpeed;
    private float fall;
    private float touchingLeftOrRight;

    private int keyIndex;

    private bool isOnGround;
    private bool isTouchingLeft;
    private bool isTouchingRight;
    private bool canWallJump;
    private bool canPickUpGun;

    private Rigidbody2D playerRb;

    private GameObject currentGun;
    private GameObject gunToPickUp;

    private GunController gunController;

    private CurrentSceneManager csManager;
    private GameManager gManager;

    private void Awake()
    {

        speed = 10f;
        jumpSpeed = 10f;
        fall = 2.5f;
        isOnGround = false;
        isTouchingLeft = false;
        isTouchingRight = false;
        canPickUpGun = false;

        keyIndex = 0;

    }

    private void Start()
    {

        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        csManager = gManager.GetCurrentSceneManager();

        playerRb = gameObject.GetComponent<Rigidbody2D>();

        currentGun = GameObject.Find("Start_Gun");

        gunController = GameObject.Find("Gun_Container").GetComponent<GunController>();
        gunController.isNormal = currentGun.GetComponent<GunCharacteristics>().isNormal;
        gunController.isTime = currentGun.GetComponent<GunCharacteristics>().isTime;

    }

    private void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        /*sistemare sistema di walljump*/
        isOnGround = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - .5f), new Vector2(.9f, .2f), 0f, groundMask);

        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - .5f, gameObject.transform.position.y), new Vector2(.2f, .9f), 0f, groundMask);

        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + .5f, gameObject.transform.position.y), new Vector2(.2f, .9f), 0f, groundMask);

        if (isTouchingLeft)
        {
            touchingLeftOrRight = 1;
        } else if (isTouchingRight)
        {
            touchingLeftOrRight = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isTouchingRight || isTouchingLeft) && !isOnGround)
        {
            canWallJump = true;
            Invoke("SetJumpingToFalse", .2f);
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }


        playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        }

        if (canWallJump)
        {
            playerRb.velocity = new Vector2(speed * touchingLeftOrRight, jumpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.E) && canPickUpGun)
        {
            PickupGun(gunToPickUp);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump_Platform"))
        {
            JumpBoost();
        }

        if (collision.CompareTag("Gun"))
        {
            canPickUpGun = collision.gameObject.GetComponent<GunCharacteristics>().isDrop;
            if (canPickUpGun)
            {
                gunToPickUp = collision.gameObject;
            }
        }

        if (collision.CompareTag("Key"))
        {
            keyIndex++;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            gManager.OnDead();
        }

        if (collision.CompareTag("Move_Camera"))
        {
            csManager.MoveCamera();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("End_Level"))
        {
            gManager.OnNext();
        }
        else if (collision.CompareTag("Finish_Speedrun"))
        {
            gManager.OnFinishSpeedrun();
        }
        else if (collision.CompareTag("Finish_Explore"))
        {
            gManager.OnFinishExplore();
        }
        else if (collision.CompareTag("Easter_Egg"))
        {
            gManager.OnEasterEgg();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Gun"))
        {
            canPickUpGun = collision.gameObject.GetComponent<GunCharacteristics>().isDrop;
            if (canPickUpGun)
            {
                gunToPickUp = collision.gameObject;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Gun"))
        {
            Invoke("SetCanPickupToFalse", 1);

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Exit_Door") && keyIndex>=4)
        {
            collision.gameObject.SetActive(false);
        }
        
    }

    private void SetCanPickupToFalse()
    {

        canPickUpGun = false;

    }

    private void SetJumpingToFalse()
    {

        canWallJump = false;

    }

    private void JumpBoost()
    {

        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed * 2);

    }

    private void PickupGun(GameObject gun)
    {

        GunCharacteristics gunChar = gun.GetComponent<GunCharacteristics>();

        bool isNormal = gunChar.isNormal;
        bool isTime = gunChar.isTime;

        GameObject gunPrefab = normalGun;

        if (isTime)
        {
            gunPrefab = timeGun;
        }

        gunController.isNormal = isNormal;
        gunController.isTime = isTime;

        GameObject nextGun = Instantiate(gunPrefab, currentGun.transform.position, currentGun.transform.rotation, gunController.gameObject.transform);

        DropGun(gun.transform);

        Destroy(currentGun);
        currentGun = nextGun;
        currentGun.GetComponent<GunCharacteristics>().isDrop = false;
        
        Destroy(gun);

    }

    private void DropGun(Transform dropSpecs)
    {

        GunCharacteristics currentGunChar = currentGun.GetComponent<GunCharacteristics>();

        Vector2 position = dropSpecs.position;
        Quaternion rotation = dropSpecs.rotation;

        GameObject drop = normalGun;

        if (currentGunChar.isTime)
        {
            drop = timeGun;
        }

        drop.GetComponent<GunCharacteristics>().isDrop = true;

        Instantiate(drop, position, rotation, csManager.gameObject.transform);

    }

}
