using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPointSystem : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bile�eni
    public Transform checkpoint; // Checkpoint'in Transform bile�eni

    private void OnTriggerEnter(Collider other)
    {
        // E�er oyuncu checkpoint b�lgesine girdiyse
        if (other.CompareTag("Player"))
        {
            SaveCheckpoint(); // Checkpoint'i kaydet
        }
    }

    void SaveCheckpoint()
    {
        // Oyuncunun pozisyonunu ve rotasyonunu kaydet
        PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);

        PlayerPrefs.SetFloat("PlayerRotX", player.rotation.x);
        PlayerPrefs.SetFloat("PlayerRotY", player.rotation.y);
        PlayerPrefs.SetFloat("PlayerRotZ", player.rotation.z);

        // Di�er checkpoint bilgilerini de kaydetmek istiyorsan�z ekleyebilirsiniz
        PlayerPrefs.Save();

        Debug.Log("Checkpoint Saved");
    }

    public void LoadCheckpoint()
    {
        // Kaydedilmi� pozisyon ve rotasyon bilgilerini al
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ");

        float rotX = PlayerPrefs.GetFloat("PlayerRotX");
        float rotY = PlayerPrefs.GetFloat("PlayerRotY");
        float rotZ = PlayerPrefs.GetFloat("PlayerRotZ");

        // Oyuncunun pozisyon ve rotasyonunu checkpoint'teki de�erlere ayarla
        player.position = new Vector3(posX, posY, posZ);
        player.rotation = Quaternion.Euler(rotX, rotY, rotZ);

        Debug.Log("Checkpoint Loaded");
    }
}