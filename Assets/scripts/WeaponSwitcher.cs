using System.Collections;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject ak47;     // Prefab o GameObject del AK-47
    public GameObject revolver; // Prefab o GameObject del revólver

    private int currentWeaponIndex = 0; // 0 = AK-47, 1 = Revolver
    private bool isSwitching = false; // Estado de cambio de arma

    // Posiciones y rotaciones de "listo para disparar" guardadas al inicio
    private Vector3 ak47ReadyPosition;
    private Vector3 ak47ReadyRotation;
    private Vector3 revolverReadyPosition;
    private Vector3 revolverReadyRotation;

    // Configuración de posición y rotación para el efecto de enfundado
    public Vector3 holsterOffset = new Vector3(-0.5f, -0.5f, 0);
    public Vector3 holsterRotation = new Vector3(0, 0, 90);
    public float switchSpeed = 3f; // Ajusta esta velocidad para mejorar la suavidad

    public AudioSource soCanvi;

    void Start()
    {
        // Guardar las posiciones y rotaciones originales para "listo para disparar"
        ak47ReadyPosition = ak47.transform.localPosition;
        ak47ReadyRotation = ak47.transform.localEulerAngles;
        revolverReadyPosition = revolver.transform.localPosition;
        revolverReadyRotation = revolver.transform.localEulerAngles;

        // Colocar el revolver en la posición de enfundado y desactivarlo
        revolver.transform.localPosition = ak47ReadyPosition + holsterOffset;
        revolver.transform.localEulerAngles = holsterRotation;
        ak47.SetActive(true);
        revolver.SetActive(false);
    }

    void Update()
    {
        // Cambiar arma cuando se presiona la tecla "Q"
        if (Input.GetKeyDown(KeyCode.Q) && !isSwitching)
        {
            currentWeaponIndex = 1 - currentWeaponIndex; // Cambia entre 0 y 1
            StartCoroutine(SwitchWeaponWithAnimation(currentWeaponIndex));
        }
    }

    IEnumerator SwitchWeaponWithAnimation(int weaponIndex)
    {
        isSwitching = true;

        soCanvi.Play();

        GameObject weaponToDraw = weaponIndex == 0 ? ak47 : revolver;
        GameObject weaponToHolster = weaponIndex == 0 ? revolver : ak47;

        Vector3 drawPosition = weaponIndex == 0 ? ak47ReadyPosition : revolverReadyPosition;
        Vector3 drawRotation = weaponIndex == 0 ? ak47ReadyRotation : revolverReadyRotation;

        // Activar el arma que desenfundamos
        weaponToDraw.SetActive(true);

        // Enfundar el arma actual (mover a posición "holster")
        yield return StartCoroutine(MoveWeapon(weaponToHolster, weaponToHolster.transform.localPosition + holsterOffset, holsterRotation, switchSpeed));

        // Desactivar el arma que acabamos de enfundar
        weaponToHolster.SetActive(false);

        // Desenfundar la nueva arma (mover a posición "ready")
        yield return StartCoroutine(MoveWeapon(weaponToDraw, drawPosition, drawRotation, switchSpeed));

        isSwitching = false;
    }

    IEnumerator MoveWeapon(GameObject weapon, Vector3 targetPosition, Vector3 targetRotation, float speed)
    {
        Vector3 startPosition = weapon.transform.localPosition;
        Vector3 startRotation = weapon.transform.localEulerAngles;
        float progress = 0f;

        while (progress < 1f)
        {
            // Incrementar el progreso en función del tiempo y la velocidad
            progress += Time.deltaTime * speed;
            // Interpolación lineal para mantener la animación controlada y limpia
            weapon.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0f, 1f, progress));
            weapon.transform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, Mathf.SmoothStep(0f, 1f, progress));
            yield return null;
        }

        // Asegurarse de que la posición y rotación final sean exactas
        weapon.transform.localPosition = targetPosition;
        weapon.transform.localEulerAngles = targetRotation;
    }
}
