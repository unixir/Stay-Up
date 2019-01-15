using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public ParticleSystem particleSys;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(particleSys, transform, false);
            particleSys.Play();
            Destroy(gameObject);
            GameManager.instance.IncreaseScore();
        }
    }
}
