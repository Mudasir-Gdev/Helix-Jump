using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollosion;
    public Rigidbody rb;
    public float ImpulseForce;
    private Vector3 StartPos;

    public int perfectpass = 0;
    public bool IsSuperSpeedactive;
    void Start()
    {
        StartPos = transform.position;
        
    }
    private void OnCollisionEnter(Collision collision)//if once it collided, then the next collosion will be detected after 0.2 sec.
    {
        if (ignoreNextCollosion)
            return;

        if (IsSuperSpeedactive)
        {
            if (!collision.gameObject.GetComponent<Goal>()) 
                Destroy(collision.transform.parent.gameObject);
            Debug.Log("Platform will Destroy.");
        }else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                deathPart.HitDeathPart();
            }
        }

       

        // Debug.Log(" Ball is colliding.");
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse);

        ignoreNextCollosion = true;
        Invoke("AllowCollosion", .2f);          //It will not check the collosion at the same time
        perfectpass = 0;
        IsSuperSpeedactive = false;
        Debug.Log("Supeer Speed is not active.");

    }
    private void Update()
    {
       if (perfectpass>=3&& !IsSuperSpeedactive)
        {
            IsSuperSpeedactive=true;
            rb.AddForce(Vector3.down*10, ForceMode.Impulse);
            Debug.Log("Supeer Speed is Active.");
        } 
    }
    private void AllowCollosion()
    {
        ignoreNextCollosion = false;
    }
    // Update is called once per frame
    public void ResetBall()
    {
        transform.position = StartPos;
    }
}
