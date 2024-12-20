using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger1 : MonoBehaviour
{
    soundManager SoundManagerScript;
    private int collectedKeys = 0;
    [SerializeField] private int requiredKeys = 5;
    [SerializeField] private TMP_Text keyCountText;
    private AudioSource audioSource;
    private float keyCollectCooldown = 3f;
    private bool canCollectKey = true;

    private void Update()
    {
        keyCountText.text = collectedKeys.ToString();
    }
    private void Start()
    {
        SoundManagerScript = GameObject.Find("SoundManager").GetComponent<soundManager>();
        audioSource = GetComponent<AudioSource>();
        collectedKeys = 0;
        keyCountText.text = collectedKeys.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("kap�") && collectedKeys >= requiredKeys)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (canCollectKey && collision.gameObject.CompareTag("anahtar"))
        {
            StartCoroutine(KeyCollectCooldown());
            collectKey();
            Destroy(collision.gameObject);
            Debug.Log("Anahtar al�nd�");
            SoundManagerScript.Point_();
            Debug.Log("Key Count: " + collectedKeys);
        }
    }
    private IEnumerator KeyCollectCooldown()
    {
        canCollectKey = false;
        yield return new WaitForSeconds(keyCollectCooldown);
        canCollectKey = true;
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


