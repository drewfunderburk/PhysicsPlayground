using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneBehaviour : MonoBehaviour
{
    [SerializeField] private float _delay = 0;

    public void GoToNextScene()
    {
        if (_delay > 0)
            StartCoroutine(GoToNextSceneCoroutine());
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator GoToNextSceneCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
