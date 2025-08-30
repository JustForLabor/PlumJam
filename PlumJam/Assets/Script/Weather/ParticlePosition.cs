using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 offset;


    private void Update()
    {
        transform.position = new Vector2(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y);
    }
}
