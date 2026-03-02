using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator enemyAnim;
    SpriteRenderer animSprite;
    Rigidbody2D enemyRig;
    public PlayerScript playerSc;
    private int patrolDestination;
    private bool IsWaiting = false;
    public int touchCount;
    [SerializeField] Transform player;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Collider2D hitCollider;
    [SerializeField] Collider2D hit;
    [SerializeField] Collider2D body;
    [SerializeField] float speed = 1f;
    
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("»грок с тегом 'Player' не найден на сцене!");
        }
        enemyRig = GetComponent<Rigidbody2D>();
        animSprite = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        Patrol();
        //Attack();
        //AttackandHit();
    }
    //void Attack()
    //{
    //    void OnCollisionEnter2D(Collision2D collision)
    //    {
    //        if (collision.collider == playerSc.attackCollider)
    //        {
    //            enemyAnim.ResetTrigger("Attack");
    //            enemyAnim.SetTrigger("Attack");

    //        }
    //    }
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerSc.attackCollider)
        {
            enemyAnim.ResetTrigger("Attack");
            enemyAnim.SetTrigger("Attack");

        }
    }
    public void AttackEnd()
    {
        enemyAnim.SetBool("IsIdle", true);
    }

    void Patrol()
    {
        Debug.Log("GO");
        if (!IsWaiting)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[0].position, speed * Time.deltaTime);
                Flip(false);
                if (Vector2.Distance(transform.position, wayPoints[0].position) < 0.5f)
                {                    
                    patrolDestination = 1;

                    StartCoroutine(WaitAndMove());   
                }    
            }
        }
        if (!IsWaiting)
        {
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[1].position, speed * Time.deltaTime);
                Flip(true);
                if (Vector2.Distance(transform.position, wayPoints[1].position) < 0.5f)
                {
                    patrolDestination = 0;

                    StartCoroutine(WaitAndMove());
                }
            }
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
    IEnumerator WaitAndMove()
    {
        IsWaiting = true;

        enemyAnim.SetBool("IsIdle", true);
        enemyAnim.SetBool("IsWalk", false);

        yield return new WaitForSeconds(3);

        IsWaiting = false;
        enemyAnim.SetBool("IsWalk", true);
        enemyAnim.SetBool("IsIdle", false);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("Player") )
        {
            touchCount++;
            enemyAnim.SetTrigger("Hit");
            if (touchCount >= 3)
            {
                body.enabled = false;
            }

            
        }
    }
    public void EndHit()
    {
        enemyAnim.ResetTrigger("Hit");
    }
}
