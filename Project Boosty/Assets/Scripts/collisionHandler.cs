using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionHandler : MonoBehaviour
{

    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem successParticles;

    private AudioSource audioSource;

    private bool isTransitioning = false;
    private bool collisionOff = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            nextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionOff = !collisionOff;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionOff)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            
            case "Finish":
                levelFinish();
                break;
            
            default:
                playerCrash();
                break;
        }
        
    }

    private void levelFinish()
    {
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel", levelLoadDelay);
    }

    private void playerCrash()
    {
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", levelLoadDelay);
    }
    
    
    private void reloadLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }

    private void nextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}


