using UnityEngine;

public class CheckPointSc : MonoBehaviour
{
    Animator checkAnim;
    void Start()
    {
        checkAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            checkAnim.SetTrigger("CheckPoint");
        }
    }
}
