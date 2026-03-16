using UnityEngine;
using System.Collections;

public class RedEnemyScript : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed = 2f;
    [SerializeField] float waitTime = 3f;
    [SerializeField] Collider2D body;
    int countHealth;
    private int patrolDestination = 0;
    private bool isWaiting = false;
    private bool isDead = false;


    private Animator redAnim;

    void Start()
    {
        redAnim = GetComponent<Animator>();
        redAnim.SetBool("IsIdle", true);
        
    }

    void Update()
    {
        

        Patrol();
    }
    public void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;

        rb.linearVelocity = new Vector2(0, 5f);
        Destroy(gameObject, 5f);
    }

    void Patrol()
    {

        if (!isWaiting)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[0].position, speed * Time.deltaTime);
                Flip(true);
                if (Vector2.Distance(transform.position, wayPoints[0].position) < 0.5f)
                {
                    patrolDestination = 1;

                    StartCoroutine(WaitAndMoving());
                }
            }
        }
        if (!isWaiting)
        {
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[1].position, speed * Time.deltaTime);
                Flip(false);
                if (Vector2.Distance(transform.position, wayPoints[1].position) < 0.5f)
                {
                    patrolDestination = 0;

                    StartCoroutine(WaitAndMoving());
                }
            }
        }

    }

    IEnumerator WaitAndMoving()
    {
        isWaiting = true;

        redAnim.SetBool("IsIdle", true);
        redAnim.SetBool("IsFly", false);

        yield return new WaitForSeconds(3);

        isWaiting = false;
        redAnim.SetBool("IsFly", true);
        redAnim.SetBool("IsIdle", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            countHealth++;
            redAnim.SetTrigger("HitRed");

            if (countHealth == 3)
            {
                Die();
                
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            redAnim.ResetTrigger("HitRed");
        }
    }


    void Flip(bool faceright)
    {
        Vector3 scalar = transform.localScale;
        if (faceright)
        {
            scalar.x = Mathf.Abs(scalar.x);
        }
        else
        {
            scalar.x = -Mathf.Abs(scalar.x);
        }
        transform.localScale = scalar;
    }
}
