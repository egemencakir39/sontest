using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    [SerializeField] private Transform Hedefpos;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private DistanceJoint2D dj;
    [SerializeField] private float activationDistance = 5f;

    private bool isHookActive = false;

    void Start()
    {
        dj.enabled = false;
        lr.positionCount = 0; // Oyun baþladýðýnda çizgi çekili deðil
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsWithinActivationDistance())
        {
            ToggleHook(); //kanca aç kapa
        }

        if (isHookActive)
        {
            lr.SetPosition(1, transform.position); //kanca çizgisi
        }
    }

    private bool IsWithinActivationDistance() //aktivasyon mesafe kontrolü
    {
        return Vector2.Distance(transform.position, Hedefpos.position) <= activationDistance;
    }

    private void ToggleHook()
    {
        isHookActive = !isHookActive;

        if (isHookActive)
        {
            HookOn(); //hook açýlýyorsa bu fonksiyonu çaðýr
        }
        else
        {
            HookOff(); //hook kapancaksa bunu çaðýr
        }
    }

    public void HookOn()
    {
        dj.enabled = true;
        dj.connectedAnchor = Hedefpos.position;

        lr.positionCount = 2;
        lr.SetPosition(0, Hedefpos.position);
        lr.SetPosition(1, transform.position);
    }

    public void HookOff()
    {
        dj.enabled = false;
        lr.positionCount = 0;
    }
}