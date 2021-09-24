using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBehaviour : MonoBehaviour
{
    [SerializeField] private float _delay = 1;

    public void Quit()
    {
        if (_delay > 0)
            StartCoroutine(QuitCoroutine());
        else
            Application.Quit();
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        Application.Quit();
    }
}
