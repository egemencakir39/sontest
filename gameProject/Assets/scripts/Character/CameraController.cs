using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerTransform;
    Vector2 sonPos;
    [SerializeField]
    Transform backGrounds;

    private void Start()
    {
        sonPos = transform.position;
    }
    private void Update()
    {
        if (PlayerTransform == null)
        {
            return;
        }

        // playerin x ve y pozisyonlarýný kameranýn pozisyonuna kopyala
        Vector3 targetPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
        transform.position = targetPosition;


        backGround();
    }	
    void backGround()
    {
        Vector2 fark = new Vector2(transform.position.x-sonPos.x,transform.position.y-sonPos.y);
        backGrounds.position += new Vector3(fark.x, fark.y, 0f);
        sonPos=transform.position;
    }
}