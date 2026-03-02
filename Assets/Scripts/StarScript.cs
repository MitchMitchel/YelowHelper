using UnityEngine;
using UnityEngine.SceneManagement;

public class StarScript : MonoBehaviour
{
    public ScoreScript scoreScript;
    [SerializeField] AudioClip soundStar;
    AudioSource starSound;

    private void Start()
    {
        //starSound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scoreScript?.AddStar();
            AudioSource.PlayClipAtPoint(soundStar, transform.position);
            Destroy(gameObject); 
        }
    }
}
