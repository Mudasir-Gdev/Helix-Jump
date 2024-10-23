using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Best;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + GameManager.Singleton.score;
        Best.text = "Best: " + GameManager.Singleton.best;
    }
}
