using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthToAdd = 10;
    soundManager SoundManagerScript;


    private void Start()
    {
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // saðlýk+
            other.GetComponent<PlayerHealth>().UpdateHealth(healthToAdd);

            SoundManagerScript.Point_();
            Destroy(gameObject);
        }
    }
}