using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    [SerializeField] private float lifetime = 2.0f;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
