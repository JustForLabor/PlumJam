using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static event EventHandler onGoal;    //플레이어 골인

    private void OnDestroy()
    {
        onGoal = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("골인");
            onGoal?.Invoke(this, new EventArgs());
            Time.timeScale = 0f;
            SoundManager.instance.StopSound();
            SoundManager.instance.PlaySoundOneShot(SoundType.Goal);
        }
    }
}
