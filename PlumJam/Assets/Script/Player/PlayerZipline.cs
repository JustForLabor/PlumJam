using System.Collections;
using UnityEngine;

public class PlayerZipline : MonoBehaviour
{
    [Header("Zipline Settings")]
    [SerializeField] private float speed;          //짚라인 속도

    public bool isZiplining { get; private set; }  //짚라인 타는 중인지 여부

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //짚라인 타기 시작
    //나중에 점프키를 통해서 짚라인 타기 종료 기능 추가 고려
    public void MoveAlongZipline(Transform start, Transform destination)
    {
        Vector2 direction = (destination.position - start.position).normalized; //짚라인 방향

        isZiplining = true;
        transform.position = start.position;                //짚라인 시작점으로 이동
        rigidbody2D.gravityScale = 0f;                      //중력 비활성화
        rigidbody2D.linearVelocity = direction * speed;     //짚라인 방향으로 이동

        StartCoroutine(Stopzipline(start, destination));    //도착 지점에 도달했는지 확인하는 코루틴 시작
    }

    private IEnumerator Stopzipline(Transform start, Transform destination)
    {
        while (true)
        {
            Vector2 playerToDestinationDir = (destination.position - transform.position).normalized;
            Vector2 velocityDir = rigidbody2D.linearVelocity.normalized;
            float dot = Vector2.Dot(playerToDestinationDir, velocityDir);

            if (dot < 0f) break;                    //플레이어가 도착 지점을 지나쳤다면 루프 탈출
            yield return null;
        }

        isZiplining = false;
        rigidbody2D.gravityScale = 1f;              //중력 활성화
        rigidbody2D.linearVelocity = Vector2.zero;  //속도 초기화
    }
}
