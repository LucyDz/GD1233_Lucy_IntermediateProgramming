using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShootHandler : MonoBehaviour
{
    [SerializeField] ProjectileWeapon _weapon;
    public Vector3 screenPos;
    public Vector3 mousePos;

    [SerializeField] private PotionBomb _bombPrefab;
    [SerializeField] private float _bombThrowForce = 12f;
    [SerializeField] private float _bombArcAngle = 30f;


    public void FireWand()
    {
        Debug.Log("WandFire");
        _weapon.Fire(mousePos);
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, 36.2f ));
        mousePos = new Vector3(mousePos.x, _weapon.Muzzle.position.y, mousePos.z);
       
    }

    // calculation from google
    Vector3 CalculateTrajectory(Vector3 start, Vector3 end, float height)
    {
        Debug.Log($"CalculateTrajectory {start}->{end} h:{height}");
        float gravity = Physics.gravity.y;
        float displacementY = end.y - start.y;
        Vector3 displacementXZ = new Vector3(end.x - start.x, 0, end.z - start.z);

        // Time to reach peak + time to fall from peak
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);

        // Velocity components
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        // Total velocity = horizontal velocity + vertical velocity
        // We use mass = 1 for ForceMode.Impulse (f=m*v), if mass > 1, multiply by rb.mass
        return velocityXZ + velocityY;
    }

    private void ThrowBomb()
    {
        if (_bombPrefab == null) return;

        Vector3 forceVector = CalculateTrajectory(_weapon.Muzzle.position, mousePos, _bombArcAngle);
        //Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        //Vector3 throwDir = Quaternion.AngleAxis(-_bombArcAngle, right) * forward;
        Debug.Log($"ThrowBomb {forceVector}");
        var bomb = Instantiate(_bombPrefab, _weapon.Muzzle.position, Quaternion.Euler(-90,0,0));
        bomb.Launch(forceVector, gameObject);
    }
}
