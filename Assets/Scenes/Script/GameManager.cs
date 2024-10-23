using UnityEditor.Advertisements;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public int score;
    public int best;
    public int CurrentStage = 0;
    public static GameManager Singleton;
    void Awake()
    {
        Advertisement.Initialize("5718469");
        
        if (Singleton == null) Singleton = this;
        else if (Singleton != this)
        {
            Destroy(gameObject);
        }
        best = PlayerPrefs.GetInt("HighScore");
    }


    
    public void AddScore(int AddtoScore)
    {
        score += AddtoScore;
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("HighScore", score);
            
        }
    }
    public void Restart()
    {
        Debug.Log("Restart Level.");
        //See ads
        Advertisement.Show("Interstitial_Android");
        score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(CurrentStage);

    }
    public void NextLvl()
    {
        CurrentStage++;
        
        Debug.Log("Going to Next Level");
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(CurrentStage);
    }

}
