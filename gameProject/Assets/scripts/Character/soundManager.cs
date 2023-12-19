using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash_;
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
}
