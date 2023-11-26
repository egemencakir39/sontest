using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPointSystem : MonoBehaviour
{
    private bool isChecked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChecked = true;
        }
    }

    public bool IsChecked()
    {
        return isChecked;
    }
}
