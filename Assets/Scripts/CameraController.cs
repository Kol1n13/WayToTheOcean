using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerWithGun;
    [SerializeField] private Transform boss;

    private void Update()
    {
        Vector3 targetPosition = GetTargetPosition();
        UpdateCameraPosition(targetPosition);
    }

    private Vector3 GetTargetPosition()
    {
        if (!BossLogic.isBossFightStart && boss != null)
            return new Vector3(boss.position.x, boss.position.y, transform.position.z);
        
        if (ItemCollecter.isGunCollected)
            return new Vector3(playerWithGun.position.x, playerWithGun.position.y, transform.position.z);
        
        return new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void UpdateCameraPosition(Vector3 targetPosition) => transform.position = targetPosition;
}
