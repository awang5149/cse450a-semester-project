using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private Vacuum vacuumComponent;
    
    private bool vacuumRunning;
    private bool shieldRunning;
    private bool ammoRunning;

    private float powerupLength;
    
    void Awake()
    {
        vacuumComponent = FindObjectOfType<Vacuum>();
        if(vacuumComponent == null) Debug.LogError("Vacuum not found");
    }
    
    
    void ActivatePowerup(bool vacuum, bool shield, bool ammo, float duration)
    {
        powerupLength = duration;

        if (vacuum && !vacuumRunning)
        {
            StartCoroutine(VacuumRoutine());
        }

        if (shield && !shieldRunning)
        {
            StartCoroutine(ShieldRoutine());
        }

        if (ammo && !ammoRunning)
        {
            StartCoroutine(AmmoRoutine());
        }
            
    }
    IEnumerator VacuumRoutine()
    {
        vacuumRunning = true;
        vacuumComponent.setActive(true);
        yield return new WaitForSeconds(powerupLength);
        vacuumComponent.setActive(false);
        vacuumRunning = false;
    }

    IEnumerator ShieldRoutine()
    {
        shieldRunning = true;
        var player = FindObjectOfType<HamtoroController>();
        player.SetShield(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetShield(false);
        shieldRunning = false;
    }

    IEnumerator AmmoRoutine()
    {
        ammoRunning = true;
        var player = FindObjectOfType<HamtoroController>();
        player.SetAmmo(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetAmmo(false);
        ammoRunning = false;
    }
}
