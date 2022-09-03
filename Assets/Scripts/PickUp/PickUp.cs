using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(AudioSource))]
public abstract class PickUp : MonoBehaviour
{
    // this is our template method. Subclasses must implement
    protected abstract void OnPickUp(PlayerMovement player);

    [Header("Feedback")]
    //[SerializeField] AudioClip _pickupSFX = null;
    [SerializeField] ParticleSystem _particlePrefab = null;
    [SerializeField] float rotationSpeed;
    Collider _collider = null;
    AudioSource _audioSource = null;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    // Reset gets called whenever you add a component to an object
    private void Reset()
    {
        // set isTrigger in the Inspector, just in case Designer forgot
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // guard clause
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null)
            return;

        // found the player! call our abstract method and supporting feedback
        OnPickUp(player);

        //if (_pickupSFX != null)
        //{
        //    SpawnAudio(_pickupSFX);
        //}

        if (_particlePrefab != null)
        {
            SpawnParticle(_particlePrefab);
        }

        //Disable();
    }

    void SpawnAudio(AudioClip pickupSFX)
    {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
    }

    void SpawnParticle(ParticleSystem pickupParticles)
    {
        ParticleSystem newParticles =
            Instantiate(pickupParticles, transform.position, Quaternion.identity);
        newParticles.Play();
    }

    // allow override in case subclass wants to object pool, etc.
    //protected virtual void Disable()
    //{
    //    gameObject.SetActive(false);
    //}

}

