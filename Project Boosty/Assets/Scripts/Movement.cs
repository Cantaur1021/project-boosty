using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 5f; 
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private AudioClip mainRocketThrusters;
    [SerializeField] private ParticleSystem mainRocketThrusterParticles;
    [SerializeField] private ParticleSystem leftRocketThrusterParticles;
    [SerializeField] private ParticleSystem rightRocketThrusterParticles;
    
    private Rigidbody rb;
    private AudioSource myAudioSource;
    

    private bool playAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
       
    }

    void ProcessThrust()
    {
        if (Input.GetKey((KeyCode.Space)))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.PlayOneShot(mainRocketThrusters);
            }

            if (!mainRocketThrusterParticles.isPlaying)
            {
                mainRocketThrusterParticles.Play();
            }
        }
        else
        {
            myAudioSource.Stop();
            mainRocketThrusterParticles.Stop();
        }

    }
    

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if (!rightRocketThrusterParticles.isPlaying)
            {
                rightRocketThrusterParticles.Play();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if (!leftRocketThrusterParticles.isPlaying)
            {
                leftRocketThrusterParticles.Play();
            }
        }
        else
        {
            rightRocketThrusterParticles.Stop();
            leftRocketThrusterParticles.Stop();
        }
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
