using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    
    bool IsGrounded = false;
    [SerializeField] public float jumpPower = 3f;
    [SerializeField] public Collider2D attackCollider;
    [SerializeField] public Collider2D hitCollider;
    [SerializeField] Collider2D body;
    [SerializeField] Collider2D legs;
    [SerializeField] public int health = 3;
    [SerializeField] private float speed;
    float horizontalInput;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject heart3;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Run();
        Jump();
        
    }
    void Run()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed , rb.linearVelocityY);
        anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        if (rb.linearVelocityX < 0f)
        {
            sprite.flipX = true;
        }
        else if (rb.linearVelocityX > 0f)
        {
            sprite.flipX = false;
        }
        
        
    }
    public virtual void Jump()
    {
        
        
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower );
            IsGrounded = false;
            anim.SetBool("IsJumping", !IsGrounded);           
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        anim.SetBool("IsJumping", !IsGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("HitBox"))
        {
            health--;
            health = Mathf.Clamp(health, 0, 3);

            anim.SetBool("IsHit", true);  // Включаем анимацию при любом попадании

            switch (health)
            {
                case 3:
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(true);
                    break;
                case 2:
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(false);
                    break;
                case 1:
                    heart1.SetActive(true);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    break;
                case 0:
                    heart1.SetActive(false);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    DeadPlayer();
                    break;
            }

            // Если анимация должна проигрываться повторно, можно сбрасывать флаг
            StartCoroutine(ResetHitAnimation());
        }
    }

    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(0.5f);  // Время анимации "hit"
        anim.SetBool("IsHit", false);
    }
    void DeadPlayer()
    {
        body.enabled = false;
        legs.enabled = false;
    }

    public void EndHit()
    {
        anim.SetBool("IsHit", false);
    }
    
   

    

}
