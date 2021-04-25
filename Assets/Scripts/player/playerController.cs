using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {
  public float speed;
  private float originalSpeed;
  public float jumpForce;
  private float moveInput;

  public float dashSpeed;
  //dash duration
  private float dashTime;
  public float startDashTime;
  //dash cooldown
  private float timeBtwnDash;
  public float startTimeBtwnDash;
  private bool countdown;

  public int health;
  public Slider healthBar;
  public GameObject deathFX;
  public Image healthBarFill;
  public Color lowHealthColor;
  //public Collider2D enemyCollider; (i have no clue what i put these in here for)
  //public Collider2D playerCollider; (keeping them in just in case something breaks, will probably be these)

  private Rigidbody2D rb;

  public bool facingRight = true;

  private bool isGrounded;
  public Transform groundCheck;
  public float checkRadius;
  public LayerMask whatIsGround;

  public int doubleJump;

  private Animator anim;
  private cameraShake shake;

  public float knockbackX;
  public float knockbackY;
  public float knockbackLength;
  public float knockbackCount;
  public bool knockFromRight;

    // Start is called before the first frame update
    void Start() {
      anim = GetComponent<Animator>();
      rb = GetComponent<Rigidbody2D>();
      shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<cameraShake>();
      dashTime = startDashTime;
      originalSpeed = speed;
      lowHealthColor = new Color(0.53725490196f, 0.21176470588f, 0.21176470588f, 1.0f);
    }

    void FixedUpdate() {
      //detects ground
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
      moveInput = Input.GetAxis("Horizontal");
      //left right move detectors
      if (moveInput == 0) {
        anim.SetBool("isRunning", false);
      } else {
        anim.SetBool("isRunning", true);
      }

      if (facingRight == false && moveInput > 0) {
        flip();
      } else if (facingRight == true && moveInput < 0) {
        flip();
      }

      //player knockback
      if(knockbackCount <= 0) {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
      } else {
        if(knockFromRight == true) {
          rb.velocity = new Vector2(-knockbackX, knockbackY);
        }
        if(knockFromRight == false) {
          rb.velocity = new Vector2(knockbackX, knockbackY);
        }
        knockbackCount -= Time.deltaTime;
      }
    }

    void Update() {
      //player HP controls
      healthBar.value = health;
      if (health <= 0) {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        shake.shake();
      }

      //dynamic health bar colour
      if (health <= 10)
        {
            healthBarFill.color = lowHealthColor;
        }

      //player on ground/ double jumps
      if (isGrounded == true) {
        doubleJump = 2;
        anim.SetBool("isJumping", false);
      } else {
        anim.SetBool("isJumping", true);
      }

      //if there is more than no jumps, jump
      if (Input.GetKeyDown(KeyCode.UpArrow) && doubleJump > 0) {
        anim.SetTrigger("takeoff");
        rb.velocity = Vector2.up * jumpForce;
        doubleJump--;
      } else if (Input.GetKeyDown(KeyCode.UpArrow) && doubleJump == 0 && isGrounded == true) {
        anim.SetTrigger("takeoff");
        rb.velocity = Vector2.up * jumpForce;
      }

      //if dash is not on cd, dash
      if (timeBtwnDash <= 0) {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime > 0) {
          speed = dashSpeed;
          timeBtwnDash = startTimeBtwnDash;
          countdown = true;
        }
      } else { //if cd is active, count down cd
        timeBtwnDash -= Time.deltaTime;
      }
      // if dash has started, count down from dash duration
      if (countdown == true) {
        dashTime -= Time.deltaTime;
        Debug.Log(dashTime);
      }
      //if dash is over, reset speed and dash duration
      if (dashTime <= 0) {
        dashTime = startDashTime;
        speed = originalSpeed;
        countdown = false;
      }
    }

    //player flipper
    void flip() {
      facingRight = !facingRight;
      Vector3 Scaler = transform.localScale;
      Scaler.x *= -1;
      transform.localScale = Scaler;
    }

    //damage taking
      public void takeDamage(int enemyDamage) {
      health -= enemyDamage;
    }
}