using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProFadeBehaviour : MonoBehaviour
{
    [SerializeField] private Gradient _fadeGradient;
    [SerializeField] private float _fadeTime = 1;

    private TextMeshProUGUI _text;
    private float _timer;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
         if (_timer < _fadeTime)
        {
            Color color = _fadeGradient.Evaluate(_timer / _fadeTime);
            _text.color = color;
        }
    }

    public void ResetTimer()
    {
        _timer = 0;
    }
}
