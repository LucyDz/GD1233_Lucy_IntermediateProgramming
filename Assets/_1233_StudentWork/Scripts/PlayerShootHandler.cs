using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShootHandler : MonoBehaviour
{
    [SerializeField] ProjectileWeapon _weapon;
    public Vector3 screenPos;
    public Vector3 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
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
}
