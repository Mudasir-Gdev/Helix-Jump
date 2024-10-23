using UnityEngine;

public class DeathPart : MonoBehaviour
{

   private void OnEnable()
    {
       
        GetComponent<MeshRenderer>().material.color = Color.red;
    }


    public void HitDeathPart()
    {
        GameManager.Singleton.Restart();
    }
}
