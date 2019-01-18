using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    bool isGoingLeft = false;
    AudioSource audioSource;
    public AudioClip collectSound, playerDeath;
    public float difficultyTime=20f,speedIncrease=0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("IncreaseDifficulty", difficultyTime, difficultyTime);
    }

    void IncreaseDifficulty()
    {
        moveSpeed += speedIncrease;
    }

    public void PlayerCollectSound()
    {
        audioSource.PlayOneShot(collectSound);
    }

    public void PlayerDeathSound()
    {
        if(audioSource.clip!=playerDeath)
        audioSource.PlayOneShot(playerDeath);
    }

    void Update()
    {
        transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
        if (Input.GetButtonDown("Jump"))
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
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
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
}
