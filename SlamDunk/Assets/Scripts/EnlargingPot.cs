using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnlargingPot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI duration;
    [SerializeField] private int firstDuration;
    [SerializeField] private GameManager gameManager;
    
    IEnumerator Start()
    {
        duration.text = firstDuration.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            firstDuration--;
            duration.text = firstDuration.ToString();

            if (firstDuration == 0)
            {
                gameObject.SetActive(false);
                break; 
            }
                
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        gameManager.EnlargePot(transform.position);
    }
}
