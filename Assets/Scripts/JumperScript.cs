using System.Collections;
using UnityEngine;

public class JumperScript : MonoBehaviour
{
    public PlayerScript player;
    
    
    private bool isUsed = false;

    void Start()
    {
       
    }
    
    
    void Update()
    {
       
    }
    private IEnumerator TemporaryJumpBoost(PlayerScript player,int ammount,float duration) 
    {
        player.jumpPower += ammount;
        yield return new WaitForSeconds(duration);
        player.jumpPower -= ammount;
        isUsed = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isUsed && collision.gameObject.CompareTag("Player"))
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                
                isUsed = true;
                StartCoroutine(TemporaryJumpBoost(player, 3, 3f));
            }
        }
    }
   
}
