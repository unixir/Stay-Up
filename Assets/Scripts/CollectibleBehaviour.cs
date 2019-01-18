using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public ParticleSystem particleSys;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().PlayerCollectSound();
            Instantiate(particleSys, transform, false);
            particleSys.Play();
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().IncreaseScore();
        }
    }
}
