using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardBehaviour : MonoBehaviour
{
    public static BlackboardBehaviour Instance;

    [SerializeField] private int _score;

    public int Score { get => _score; private set => _score = value; }

    private void Awake()
    {
        // Singleton
        if (!Instance)
            Instance = this;
        if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void IncreaseScore(int amount)
    {
        if (amount > 0)
            Score += amount;
    }
}
