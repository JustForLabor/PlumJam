using System.Collections;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform startPoint; //사다리 시작 지점
    [SerializeField] private Transform endPoint;   //사다리 끝 지점

    private PlayerClimb playerClimb;

    public void Interact(Transform playerTransform)
    {
        Debug.Log("Ladder Interacted");

        playerClimb = playerTransform.GetComponent<PlayerClimb>();

        if (playerClimb == null) return;
        
        if (!playerClimb.isClimbing)
        {
            //사다리 타기 시작
            playerTransform.position = new Vector2(startPoint.position.x, playerTransform.position.y);
            playerClimb.EnterClimbMode();
            StartCoroutine(WaitForClimbEnd(playerTransform));
        }
        else
        {
            //사다리 타기 종료
            playerClimb.ExitClimbMode();
        }
        
    }

    private IEnumerator WaitForClimbEnd(Transform playerTransform)
    {
        if (playerClimb == null)
        {
            yield break;
        }

        yield return new WaitUntil(() => playerTransform.position.y >= endPoint.position.y);
        playerClimb.ExitClimbMode();
        Debug.Log("Climb Ended");
    }
}
