using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject _originalCam;
    private GameObject _aniCam;

    private Animator ani;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void BringButtonsBack()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(true);
        }
    }

    private void BackToOriginalCamera()
    {
        _originalCam.SetActive(true);
        _aniCam.SetActive(false);
    }

    private IEnumerator DelayedAni(string aniName)
    {
        yield return new WaitForSeconds(2f);

        ani.Play(aniName);
    }

    public void PlayAnimation(string aniName)
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
        _aniCam.SetActive(true);
        _originalCam.SetActive(false);
        StartCoroutine(DelayedAni(aniName));
    }

    public void SetAniCam(string name)
    {
        _aniCam = transform.Find(name).gameObject;
    }

    public void Reset()
    {
        BackToOriginalCamera();
        BringButtonsBack();
    }
}
