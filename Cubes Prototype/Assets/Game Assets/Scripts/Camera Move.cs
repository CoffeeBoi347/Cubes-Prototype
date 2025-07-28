using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject objToFollow;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 targetPos = objToFollow.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, 1f);
    }
}