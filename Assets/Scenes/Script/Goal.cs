using UnityEngine;

public class Goal : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Singleton.NextLvl();
    }

}
