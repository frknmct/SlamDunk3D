using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource ballVoice;
    private void OnTriggerEnter(Collider other)
    {
        ballVoice.Play();
        if (other.CompareTag("Basket"))
        {
            gameManager.Basket(transform.position);
        }
        else if (other.CompareTag("BottomObject"))
        {
            gameManager.Lose();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballVoice.Play();
    }
}
