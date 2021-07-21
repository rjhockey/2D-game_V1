using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // camera follows player to the right
            if (collision.transform.position.x < transform.position.x)
                    cam.MoveToNewRoom(nextRoom);
            // camera follows player to the left
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }
}
