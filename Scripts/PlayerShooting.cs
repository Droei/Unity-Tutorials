using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] Camera camera;
    [SerializeField] float fireRate = 0.5f;

    Coroutine fireRoutine;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            fireRoutine = StartCoroutine(FireLoop());
        }
        else if (context.canceled)
        {
            if (fireRoutine != null)
            {
                StopCoroutine(fireRoutine);
            }
        }
    }

    private IEnumerator FireLoop()
    {
        while (true)
        {
            Instantiate(bullet, shootPos.position, Quaternion.LookRotation(camera.transform.forward));
            yield return new WaitForSeconds(fireRate);
        }
    }
}


