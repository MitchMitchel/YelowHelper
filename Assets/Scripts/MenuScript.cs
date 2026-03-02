using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] GameObject helper;
    //[SerializeField] Transform waitPoint;
    public float speed = 3f;
    
    Animator playAnim;
    Animator helperAnim;
    Rigidbody2D helperRb;

    private void Start()
    {
        // 1. Проверяем Кнопку
        if (play != null)
        {
            playAnim = play.GetComponentInChildren<Animator>();
            if (playAnim == null) Debug.LogWarning("Аниматор на кнопке Play не найден!");
        }

        // 2. Проверяем Хелпера
        if (helper != null)
        {
            // Ищем компоненты максимально глубоко
            helperAnim = helper.GetComponentInChildren<Animator>();
            helperRb = helper.GetComponentInChildren<Rigidbody2D>();

            if (helperAnim == null) Debug.LogWarning("Аниматор на Хелпере не найден!");
            if (helperRb == null) Debug.LogWarning("Rigidbody2D на Хелпере не найден!");
        }

        // 3. Проверяем Точку
        //if (waitPoint == null) Debug.LogError("Точка WaitPoint не назначена!");

        StartCoroutine(HelperAnimation(2f));

    }
    public void PlayGame()
    {
        StopAllCoroutines();
        playAnim.SetBool("IsPlay", true);
        StartCoroutine(PlayAnimation(1, 0.5f));
        //helperAnim.ResetTrigger("Run");
              
    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator PlayAnimation(int sceneIndex,float deelay)
    {
        yield return new WaitForSeconds(deelay);
        SceneManager.LoadScene(sceneIndex);
    }
    IEnumerator HelperAnimation(float wait)
    {
        yield return new WaitForSeconds(wait);

        //helperAnim.SetTrigger("Run");
        //Vector2 move = new Vector2(helper.transform.right.x, 0f);
        //helperRb.linearVelocity = Vector2.MoveTowards(helper.transform.position, waitPoint.position, move.x * speed);




    }
}
