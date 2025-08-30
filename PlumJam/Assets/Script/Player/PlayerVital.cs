using System;
using UnityEngine;

public class PlayerVital : MonoBehaviour
{
    public static event EventHandler OnPlayerDie;

    private void OnDestroy()
    {
        OnPlayerDie = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flood"))
        {
            Die();
        }
    }

    private void Die()
    {
        //플레이어 사망
        Debug.Log("Player died!");
        OnPlayerDie?.Invoke(this, EventArgs.Empty);
    }
}
