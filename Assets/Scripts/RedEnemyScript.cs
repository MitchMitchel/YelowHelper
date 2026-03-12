using UnityEngine;
using System.Collections;

public class RedEnemyScript : MonoBehaviour
{
    Animator redAnim;
    Rigidbody2D redRig;
    [SerializeField] Transform[] wayPoints;

    [SerializeField] float speed = 1f;

    private int patrolDestination;
    private bool IsWaiting = false;
    void Start()
    {
        redAnim = GetComponent<Animator>();
        redRig = GetComponent<Rigidbody2D>();

        redAnim.SetBool("IsIdle", false);
        redAnim.SetBool("IFly", true);
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
    void Patrol()
    {
        
        if (!IsWaiting)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[0].position, speed * Time.deltaTime);
                Flip(false);
                if (Vector2.Distance(transform.position, wayPoints[0].position) < 0.1f)
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
                if (Vector2.Distance(transform.position, wayPoints[1].position) < 0.1f)
                {
                    patrolDestination = 0;

                    StartCoroutine(WaitAndMoving());
                }
            }
        }

    }
    IEnumerator WaitAndMoving()
    {
        IsWaiting = true;

        redAnim.SetBool("IsIdle", true);
        redAnim.SetBool("IsFly", false);

        yield return new WaitForSeconds(3);

        IsWaiting = false;
        redAnim.SetBool("IsFly", true);
        redAnim.SetBool("IsIdle", false);
    }
    void Update()
    {
        Patrol();
    }
}
