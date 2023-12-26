using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    public Transform pos1; // I��nlanacak pozisyon 1
    public Transform pos2; // I��nlanacak pozisyon 2
    public float teleportDistance = 2f; // I��nlanma mesafesi

    private Transform targetPos; // Hedef pozisyon
    private bool isTeleporting = false; // I��nlanma durumu

    void Update()
    {
        // Oyuncu ile d��man aras�ndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());

        // Belirli bir mesafeden daha yak�nsa ve �u anda ���nlanma i�lemi ger�ekle�miyorsa
        if (distanceToPlayer <= teleportDistance && !isTeleporting)
        {
            // I��nlanma i�lemi ba�lat
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        // I��nlanma i�lemi ba�lad���nda durumu g�ncelle
        isTeleporting = true;

        // Belirli bir s�re bekleyin
        yield return new WaitForSeconds(0.5f);

        // Hedef pozisyonu se�in
        targetPos = (targetPos == pos1) ? pos2 : pos1;

        // D��man� hedef pozisyona ���nla
        transform.position = targetPos.position;

        // I��nlanma i�lemi bitti�inde durumu g�ncelle
        isTeleporting = false;
    }

    // Oyuncu pozisyonunu d�nd�r
    private Vector2 PlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
