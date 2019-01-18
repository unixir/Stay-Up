using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFloorBehaviour : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().PlayerDeathSound();
            gameManager.GameOver();
        }
    }
}
