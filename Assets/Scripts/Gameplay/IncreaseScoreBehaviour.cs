using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreBehaviour : MonoBehaviour
{
    // I know this is stupid, but I'm lazy and this is a quick fix for unity events not being able
    //  to call static functions or deal with singletons in general
    public void IncreaseScore(int amount)
    {
        BlackboardBehaviour.Instance.IncreaseScore(amount);
    }
}
