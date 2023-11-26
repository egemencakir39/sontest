using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPointSystem : MonoBehaviour
{
    private Transform currentCheckPoint;
    public void CheckPointChanger (Transform CheckPoint)
    {
        currentCheckPoint = CheckPoint;
    }
    public Transform GetCurrentCheckpoint()
    {
        return currentCheckPoint;
    }
}
