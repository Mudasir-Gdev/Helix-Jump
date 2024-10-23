using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController Ball;
    private float Offset;

    private void Awake()
    {

        Offset = transform.position.y - Ball.transform.position.y;
    }


    private void Update()
    {

        Vector3 CurrentPos = transform.position;
        CurrentPos.y = Ball.transform.position.y + Offset;
        transform.position = CurrentPos;
    }
}
