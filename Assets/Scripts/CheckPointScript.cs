using UnityEngine;
using TMPro;

public class CheckPointSc : MonoBehaviour
{
    Animator checkAnim;
    public TMP_Text stageClearTitle;
    Animator stageAnim;
    
    void Start()
    {
        checkAnim = GetComponent<Animator>();
        stageAnim = GetComponent<Animator>();
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
        stageAnim.SetTrigger("StageClear");
    }
}
