using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneBehaviour : MonoBehaviour
{
    [SerializeField] private float _delay = 1;

    public void ReloadScene()
    {
        if (_delay > 0)
            StartCoroutine(ReloadSceneCoroutine());
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ReloadSceneCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
