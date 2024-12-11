using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class InteractiveAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private GameObject[] _IKObjects;
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

    private void ActivateIK()
    {
        for(int i = 0; i < _IKObjects.Length; i++)
        {
            _IKObjects[i].GetComponent<FastIKFabric>().enabled = true;
        }
    }

    private void DeactivateIK()
    {
        for(int i = 0; i < _IKObjects.Length; i++)
        {
            _IKObjects[i].GetComponent<FastIKFabric>().enabled = false;
        }
    }

    public void PlayAnimation(string aniName)
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetActive(false);
        }
        Debug.Log($"TURNING ON {_aniCam.name}");
        _aniCam.SetActive(true);
        _originalCam.SetActive(false);
        StartCoroutine(DelayedAni(aniName));
    }

    public void PlayAnimationNoDelay(string aniName)
    {
        ani.Play(aniName);
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

    public void SpecialGuyFunctionOn()
    {
        GameObject guy = transform.Find("metarig").gameObject;
        guy.GetComponent<SplineMover>().enabled = true;
    }

    public void SpecialGuyFunctionOff()
    {
        GameObject guy = transform.Find("metarig").gameObject;
        guy.GetComponent<SplineMover>().enabled = false;
    }
}
