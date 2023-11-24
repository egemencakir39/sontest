using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    [SerializeField] private Transform Hedefpos; 
    [SerializeField] private LineRenderer lr; 
    [SerializeField] private DistanceJoint2D dj; 
    [SerializeField] private float activationDistance = 5f;
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        dj.enabled = false; // dj ba�lang��ta devre d��� b�rak�l�r
    }

    // Update is called once per frame
    void Update()
    {
        // q tu�una bas�ld���nda devreye girecek
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsWithinActivationDistance())
            {
                HookOn(); // Hook'u etkinle�tir
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            HookOff(); // q tu�u b�rak�ld���nda hooku devre d��� b�rak
        }
    }

    // activationdistance i�indemi
    private bool IsWithinActivationDistance()
    {
        return Vector2.Distance(transform.position, Hedefpos.position) <= activationDistance;
    }

    // Hook'u etkinle�tir
    public void HookOn()
    {
        dj.enabled = true; // dj etkinle�tir
        dj.connectedAnchor = Hedefpos.position; // Ba�lant� noktas�n� ayarla
        lr.positionCount = 2; // LineRenderer pozisyon say�s�n� ayarla
        lr.SetPosition(0, Hedefpos.position); // LineRenderer'�n ba�lang�� pozisyonunu ayarla
        lr.SetPosition(1, transform.position); // LineRenderer'�n biti� pozisyonunu ayarla
    }

    // Hook'u devre d��� b�rak
    public void HookOff()
    {
        dj.enabled = false; // DistanceJoint2D'yi devre d��� b�rak
        lr.positionCount = 0; // LineRenderer pozisyon say�s�n� s�f�rla
    }
}