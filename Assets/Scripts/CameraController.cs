using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerWithGun;

    private void Update()
    {
        Vector3 targetPosition = GetTargetPosition();
        UpdateCameraPosition(targetPosition);
    }

    private Vector3 GetTargetPosition()
    {
        if (ItemCollecter.isGunCollected)
        {
            return new Vector3(playerWithGun.position.x, playerWithGun.position.y, transform.position.z);
        }
        else
        {
            return new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }

    private void UpdateCameraPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
