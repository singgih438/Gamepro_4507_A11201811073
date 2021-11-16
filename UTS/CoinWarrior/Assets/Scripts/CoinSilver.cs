using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSilver : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            CoinScore.hitungCoin += 20;
        }
    }
}
