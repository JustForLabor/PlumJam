using UnityEngine;

public class Zipline : MonoBehaviour, IInteractable
{
    
    [SerializeField] private Transform destination;  //짚라인 도착 지점

    private PlayerZipline playerZipline;

    public void Interact(Transform playerTransform)
    {
        Debug.Log("Zipline Interacted");

        playerZipline = playerTransform.GetComponent<PlayerZipline>();

        if (!playerZipline.isZiplining)
        {
            playerZipline.MoveAlongZipline(transform, destination);
        }
    }
}
