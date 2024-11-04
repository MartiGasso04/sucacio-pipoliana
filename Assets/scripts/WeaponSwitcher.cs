using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject ak47;     // Prefab o GameObject del AK-47
    public GameObject revolver; // Prefab o GameObject del revólver

    private int currentWeaponIndex = 0; // 0 = AK-47, 1 = Revolver

    void Start()
    {
        // Activar solo el arma inicial (AK-47 en este caso)
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Cambiar arma cuando se presiona la tecla "Q"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex = 1 - currentWeaponIndex; // Cambia entre 0 y 1
            SwitchWeapon(currentWeaponIndex);
        }
    }

    void SwitchWeapon(int weaponIndex)
    {
        // Activa/desactiva las armas según el índice
        if (weaponIndex == 0)
        {
            ak47.SetActive(true);
            revolver.SetActive(false);
        }
        else
        {
            ak47.SetActive(false);
            revolver.SetActive(true);
        }
    }
}
