using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject backgroundPanel; // UI panel referans�
    public string[] lines;
    public float textSpeed;
    public bool isPersistent = false;

    private int index;
    private bool dialogueTriggered = false;

    void Start()
    {
        textComponent.text = string.Empty;
        HideBackgroundPanel(); // Oyuna ba�lad���nda arka plan� gizle
    }

    void Update()
    {
        if (dialogueTriggered && Input.GetKeyDown(KeyCode.P))
        {
            NextLine();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!dialogueTriggered && other.CompareTag("Player"))
        {
            dialogueTriggered = true;
            ShowBackgroundPanel(); // Diyalog ba�lad���nda arka plan� g�ster
            startDialogue();
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
            HideBackgroundPanel(); // Diyalog bitti�inde arka plan� gizle
           
        }
    }

    void ShowBackgroundPanel()
    {
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true);
        }
    }

    void HideBackgroundPanel()
    {
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }
}