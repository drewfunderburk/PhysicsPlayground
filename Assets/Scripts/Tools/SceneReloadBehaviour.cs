using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadBehaviour : MonoBehaviour
{
    [SerializeField] private KeyCode _key;

    private void Update()
    {
        if (Input.GetKeyDown(_key))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
