using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash_;
    [SerializeField] private AudioClip atack1;
    [SerializeField] private AudioClip bow_;
    [SerializeField] private AudioClip CheckPoint;
    [SerializeField] private AudioClip Point;
    [SerializeField] private AudioClip atack2;
    [SerializeField] private AudioClip takingDamage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Jump_()
    {
        audioSource.PlayOneShot(jump);
    }
    public void Dash_()
    {
        audioSource.PlayOneShot(dash_);
    }
    public void Attack1_()
    {
        audioSource.PlayOneShot(atack1);
    }
    public void Bow_()
    {
        audioSource.PlayOneShot(bow_);
    }
    public void CheckPoint_()
    {
        audioSource.PlayOneShot(CheckPoint);
    }
    public void Point_()
    {
        audioSource.PlayOneShot(Point);
    }
    public void Attack2_()
    {
        audioSource.PlayOneShot(atack2);
    }
    public void takingDamage_()
    {
        audioSource.PlayOneShot(takingDamage);
    }
}
