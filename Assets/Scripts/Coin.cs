using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public void collect()
    {
        Destroy(gameObject);
    }
}
