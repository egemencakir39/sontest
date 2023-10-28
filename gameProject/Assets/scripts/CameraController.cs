using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerTransform;

    private void Update()
    {
        if (PlayerTransform == null)
        {
            return;
        }

        // Oyuncunun x ve y pozisyonlarýný kameranýn pozisyonuna kopyala
        Vector3 targetPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
        transform.position = targetPosition;
    }	
}