using System.Collections;
using UnityEngine;

public class EnemyMonitor : MonoBehaviour
{
    Animator monitorAnim;
    SpriteRenderer animSprite;
    Rigidbody2D monitorRig;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed = 1f;
    private int patrolDestination;
    private bool IsWaiting = false;

    void Start()
    {
        monitorAnim = GetComponent<Animator>();
        monitorRig = GetComponent<Rigidbody2D>();

        monitorAnim.SetBool("IsIdle", false);
        monitorAnim.SetBool("IsWalk", true);
    }

    
    void Update()
    {
        Patrol();
        
    }
    void Patrol()
    {
        
        if (!IsWaiting)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[0].position, speed * Time.deltaTime);
                Flip(false);
                if (Vector2.Distance(transform.position, wayPoints[0].position) < 0.5f)
                {
                    patrolDestination = 1;

                    StartCoroutine(WaitAndMoving());
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

                    StartCoroutine(WaitAndMoving());
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
    IEnumerator WaitAndMoving()
    {
        IsWaiting = true;

        monitorAnim.SetBool("IsIdle", true);
        monitorAnim.SetBool("IsWalk", false);

        yield return new WaitForSeconds(3);

        IsWaiting = false;
        monitorAnim.SetBool("IsWalk", true);
        monitorAnim.SetBool("IsIdle", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monitorAnim.SetTrigger("HitMonitor");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monitorAnim.ResetTrigger("HitMonitor");
        }
    }


}
