using UnityEngine;

public class Looking : MonoBehaviour
{
    [SerializeField] private Player PlayerMovement;
    [SerializeField] private float _speed;
    [SerializeField] private float _positionZ;

    private void FixedUpdate()
    {
        Vector3 target = new Vector3(PlayerMovement.transform.position.x, PlayerMovement.transform.position.y, _positionZ);
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.fixedDeltaTime);
    }
}
