using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    bool isGoingLeft = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            if (isGoingLeft)
            {
                transform.Rotate(0, 90, 0);
                isGoingLeft = false;
            }
            else
            {
                transform.Rotate(0, -90, 0);
                isGoingLeft = true;
            }
        }
    }
}
