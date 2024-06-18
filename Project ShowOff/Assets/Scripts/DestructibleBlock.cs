using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DestructibleBlock : MonoBehaviour
{

    [SerializeField] ParticleSystem ParticleSystem;
    [FormerlySerializedAs("collider")] [SerializeField] Collider selfCollider;
    [FormerlySerializedAs("collider")][SerializeField] Collider selfCollider2;
    [SerializeField] MeshRenderer mR;

    Vector3 startScale;

    float deathTimer;
    bool waiting = false;

    float animationTimer;
    float animationResetTimer;
    bool animating = false;

    [SerializeField]
    float wobbleIntensity;
    [SerializeField]
    float wobbleSpeed;

    private void Start()
    {
        if (ParticleSystem == null) 
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        if (selfCollider == null)
        {
            selfCollider = GetComponent<Collider>();
        }

        if (mR == null)
        {
            mR = GetComponent<MeshRenderer>();
        }

        startScale = transform.localScale;
    }

    public void DestroyThis()
    {
        ParticleSystem.Play();
        waiting = true;

        deathTimer = Time.time + ParticleSystem.main.duration;

        Destroy(selfCollider);
        Destroy(selfCollider2);
        Destroy(mR);
    }

    public void StartAnimating()
    {
        animating = true;
        animationResetTimer = 0.1f;
    }

    private void Update()
    {
        if (waiting)
        {
            if (deathTimer < Time.time)
            {
                Destroy(gameObject);
            }
        }

        if (animating)
        {
            if (animationResetTimer < 0)
            {
                animating = false;
                animationTimer = 0;
                transform.localScale = startScale;

                return;
            }

            animationTimer += Time.deltaTime;
            animationResetTimer -= Time.deltaTime;

            transform.localScale = startScale * (1f + wobbleIntensity * Mathf.Sin(Time.time * wobbleSpeed));
        }
    }
}
