using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public TMP_Text scoreStar;
    
    private int starCount = 0;
    private void Start()
    {
        
    }
    public void AddStar()
    {
        starCount++;
        if (scoreStar != null)
            scoreStar.text = starCount.ToString();

        
    }
}
