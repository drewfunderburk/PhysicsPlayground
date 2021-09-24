using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFadeBehaviour : MonoBehaviour
{
    private enum Direction
    {
        FORWARD,
        BACKWARD
    }

    [SerializeField] private Gradient _fadeGradient;
    [SerializeField] private float _fadeTime = 1;
    [SerializeField] private bool _fadeInStart = false;
    [SerializeField] private Direction _fadeDirection = Direction.FORWARD;

    private Image _image;
    private float _timer;
    private bool _isFading;

    private Direction FadeDirection { get => _fadeDirection; set => _fadeDirection = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        if (_fadeInStart)
            Fade();
    }

    public void Fade()
    {
        if (!_isFading)
            StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        _isFading = true;
        while (_timer < _fadeTime)
        {
            _timer += Time.deltaTime;
            if (_timer < _fadeTime)
            {
                Color color = new Color();
                switch (FadeDirection)
                {
                    case Direction.FORWARD:
                        color = _fadeGradient.Evaluate(_timer / _fadeTime);
                        break;
                    case Direction.BACKWARD:
                        color = _fadeGradient.Evaluate(1 - (_timer / _fadeTime));
                        break;
                }
                
                _image.color = color;
            }

            yield return null;
        }

        _timer = 0;
        _isFading = false;
    }
}
