using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosx;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        // smoothdamp gradually changes position
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosx, transform.position.y, transform.position.z), ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosx = _newRoom.position.x;
    }
}
