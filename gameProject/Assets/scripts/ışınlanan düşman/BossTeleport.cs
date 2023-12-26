using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    public Transform pos1; // Iþýnlanacak pozisyon 1
    public Transform pos2; // Iþýnlanacak pozisyon 2
    public float teleportDistance = 2f; // Iþýnlanma mesafesi

    private Transform targetPos; // Hedef pozisyon
    private bool isTeleporting = false; // Iþýnlanma durumu

    void Update()
    {
        // Oyuncu ile düþman arasýndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerPosition());

        // Belirli bir mesafeden daha yakýnsa ve þu anda ýþýnlanma iþlemi gerçekleþmiyorsa
        if (distanceToPlayer <= teleportDistance && !isTeleporting)
        {
            // Iþýnlanma iþlemi baþlat
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        // Iþýnlanma iþlemi baþladýðýnda durumu güncelle
        isTeleporting = true;

        // Belirli bir süre bekleyin
        yield return new WaitForSeconds(0.5f);

        // Hedef pozisyonu seçin
        targetPos = (targetPos == pos1) ? pos2 : pos1;

        // Düþmaný hedef pozisyona ýþýnla
        transform.position = targetPos.position;

        // Iþýnlanma iþlemi bittiðinde durumu güncelle
        isTeleporting = false;
    }

    // Oyuncu pozisyonunu döndür
    private Vector2 PlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
