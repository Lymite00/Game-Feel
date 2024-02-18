using System;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public Rigidbody2D theRB;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    public BoxCollider2D myboxcol;

    public BulletScript shotToFire;
    public Transform shotPoint;

    public GameObject bulletParticle;
    public GameObject jumpParticle;

    // Bool part
    private bool doubleJump;

    // Recoil
    public float recoilForce;
    public float recoilDuration;

    private float recoilTimer;
    private Vector2 recoilDirection;

    //görünmezlik etkisi
    private float reChargeCounter;
    
    public GameObject bombPrefab; // Bomba prefabini tutacak değişken
    public Transform throwPoint; // Bombanın atılacağı nokta
    public float throwForce = 10f; // Bombanın atma kuvveti
    
    private int bombCount;

    public GameObject volume;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        bombCount = 10;
    }


    void Update()
    {
        if (global.instance.volumeBool)
        {
            volume.SetActive(true);
        }
        else
        {
            volume.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.B) && bombCount > 0)
        {
            ThrowBomb();
            bombCount--;
        }
        if (recoilTimer > 0)
        {
            // Recoil süresi boyunca recoil uygulanıyor
            theRB.velocity = recoilDirection * recoilForce;
            recoilTimer -= Time.deltaTime;
        }
        else
        {
            // Yukarı dash
            /*if (Input.GetKeyDown(KeyCode.Q))
            {
                theRB.velocity = new Vector2(theRB.velocity.x, transform.localScale.y * dashSpeed);
            }*/
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameManager.instance.ToggleJuice();
                
            }
            jumpcheck();

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            theRB.velocity = new Vector2(horizontalInput * moveSpeed, theRB.velocity.y);

            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = Vector3.one;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                if (GameManager.instance.juice)
                {
                    CameraShaker.Instance.ShakeOnce(3f, 3f, 0f, 0.5f);
                    PlayerController.instance.ShotSound();
                    Instantiate(bulletParticle, shotPoint.position, shotPoint.rotation);
                    anim.SetTrigger("Fired");
                    if (isOnGround)
                    {
                        ApplyRecoil();
                    }
                }
            }

            // Ses dosyasını çalma
            if (Mathf.Abs(theRB.velocity.x) > 0 && isOnGround && GameManager.instance.juice)
            {
                PlayerController.instance.PlayFootstepSound();
            }

            if (Input.GetButtonDown("Jump") && (isOnGround || doubleJump))
            {
                if (isOnGround)
                {
                    doubleJump = true;
                }
                else
                {
                    doubleJump = false;
                    if (GameManager.instance.juice)
                    {
                        PlayerController.instance.PlayFootstepSound();
                        anim.SetTrigger("doubleJump");
                    }
                }
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            anim.SetBool("isOnGround", isOnGround);
            if (GameManager.instance.juice)
            {
                anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            LevelController.instance.SpawnEnemy();
            LevelController.instance.canSpawn = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.CompareTag("Finish 1"))
        {
            GameManager.instance.level2bool = true;
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.CompareTag("Finish 2"))
        {
            GameManager.instance.level3bool = true;
            SceneManager.LoadScene(3);
        }
        if (collision.gameObject.CompareTag("Finish 3"))
        {
            GameManager.instance.level4bool = true;
            SceneManager.LoadScene(4);
        }
        if (collision.gameObject.CompareTag("Finish 4"))
        {
            GameManager.instance.level5bool = true;
            SceneManager.LoadScene(5);
        }
        if (collision.gameObject.CompareTag("Finish 5"))
        {
            LevelController.instance.Win();
        }
    }
    void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();

        if (bombRigidbody != null)
        {
            Vector2 throwDirection = transform.localScale.x > 0 ? throwPoint.right : -throwPoint.right;
            bombRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }
    public void jumpcheck()
    {
        if (myboxcol.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (!isOnGround && GameManager.instance.juice)
            {
                PlayJumpParticle();
            }
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
    public void ApplyRecoil()
    {
        recoilTimer = recoilDuration;
        recoilDirection = -transform.right * transform.localScale.x;
    }
    void PlayJumpParticle()
    {
        if (jumpParticle!= null)
        {
            Instantiate(jumpParticle, groundPoint.position, Quaternion.identity);
        }
    }
}
