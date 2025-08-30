using UnityEngine;

public class ZiplineLineRenderer : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, point1.position);
        lineRenderer.SetPosition(1, point2.position);
    }

    private void OnDestroy()
    {
        lineRenderer = null;
    }
}
