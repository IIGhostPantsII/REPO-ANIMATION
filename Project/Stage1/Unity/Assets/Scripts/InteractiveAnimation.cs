using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAnimation : MonoBehaviour
{   
    Animator ani;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void PlayAnimation(string aniName)
    {
        ani.Play(aniName);
    }
}
