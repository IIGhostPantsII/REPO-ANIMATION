using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject[] _turnOnButtons;

    private void TurnOnButtons()
    {
        for(int i = 0; i < _turnOnButtons.Length; i++)
        {
            _turnOnButtons[i].SetActive(true);
        }
    }

    public IEnumerator WaitForFade(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TurnOnButtons();
    }

    public void Transition(float seconds)
    {
        StartCoroutine(WaitForFade(seconds));
    }
}
