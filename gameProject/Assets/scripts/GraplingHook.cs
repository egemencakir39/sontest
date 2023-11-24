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
        dj.enabled = false; // dj baþlangýçta devre dýþý býrakýlýr
    }

    // Update is called once per frame
    void Update()
    {
        // q tuþuna basýldýðýnda devreye girecek
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsWithinActivationDistance())
            {
                HookOn(); // Hook'u etkinleþtir
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            HookOff(); // q tuþu býrakýldýðýnda hooku devre dýþý býrak
        }
    }

    // activationdistance içindemi
    private bool IsWithinActivationDistance()
    {
        return Vector2.Distance(transform.position, Hedefpos.position) <= activationDistance;
    }

    // Hook'u etkinleþtir
    public void HookOn()
    {
        dj.enabled = true; // dj etkinleþtir
        dj.connectedAnchor = Hedefpos.position; // Baðlantý noktasýný ayarla
        lr.positionCount = 2; // LineRenderer pozisyon sayýsýný ayarla
        lr.SetPosition(0, Hedefpos.position); // LineRenderer'ýn baþlangýç pozisyonunu ayarla
        lr.SetPosition(1, transform.position); // LineRenderer'ýn bitiþ pozisyonunu ayarla
    }

    // Hook'u devre dýþý býrak
    public void HookOff()
    {
        dj.enabled = false; // DistanceJoint2D'yi devre dýþý býrak
        lr.positionCount = 0; // LineRenderer pozisyon sayýsýný sýfýrla
    }
}