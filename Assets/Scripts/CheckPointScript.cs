using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CheckPointSc : MonoBehaviour
{
    
    public Animator checkAnim;
    public TMP_Text stageClearTitle;
    public Animator stageAnim;
    
    void Start()
    {
        
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
            stageAnim.SetTrigger("StageClear");

            StartCoroutine(SceneController());
        }


    }
    IEnumerator SceneController()
    {
        yield return new WaitForSeconds(3f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
