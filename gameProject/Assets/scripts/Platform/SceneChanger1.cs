using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger1 : MonoBehaviour
{
    private int collectedKeys = 0;
    [SerializeField] private int requiredKeys = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("kap�") && collectedKeys >= requiredKeys)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.CompareTag("anahtar"))
        {
            collectKey();
            Destroy(collision.gameObject);
            Debug.Log("al�nd� anahtar");
        }
    }
   
    public void collectKey()
    {
        collectedKeys++;
        if (collectedKeys >= requiredKeys)
        {
            Debug.Log("kap� a��labilir");
        }
    }
}


