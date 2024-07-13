using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private float _randomExtralength;

    public void ResetChunk()
    {

    }

    public float Length => _length;
    public float RandomExtraLength => _randomExtralength;

    private void OnDrawGizmos()
    {
        GizmoDrawLine(Color.green, 0);
        GizmoDrawLine(Color.cyan, _length);
        GizmoDrawLine(Color.red, _length + _randomExtralength);
    }

    private void GizmoDrawLine(Color color, float x)
    {
        Gizmos.color = color;

        // Calculate the start and end points for the line
        Vector3 start = new Vector3(transform.position.x + x, transform.position.y - 3.2f, transform.position.z);
        Vector3 end = new Vector3(transform.position.x + x, transform.position.y + 1.8f, transform.position.z);

        // Draw the line
        Gizmos.DrawLine(start, end);
    }
}
