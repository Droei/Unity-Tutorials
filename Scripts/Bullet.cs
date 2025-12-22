using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = transform.forward;
        float distance = speed * Time.deltaTime;

        if (Physics.Raycast(currentPosition, direction, out RaycastHit hit, distance))
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * distance);
        }
    }
}
