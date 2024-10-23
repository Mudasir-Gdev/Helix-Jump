using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<BallController>().perfectpass++;
        
        GameManager.Singleton.AddScore(2);
    }
}
