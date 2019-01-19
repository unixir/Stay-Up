using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float rotateSpeed=10f;
    void Start()
    {
        StartCoroutine(RotateCoin());   
    }

    IEnumerator RotateCoin()
    {
        while (this.isActiveAndEnabled)
        {
            transform.Rotate(Vector3.forward, rotateSpeed);
            yield return null;
        }
    }
}
