using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private HamtoroController player;
    private Vacuum vacuumComponent;
    
    private bool vacuumRunning;
    private bool shieldRunning;
    private bool ammoRunning;

    private int powerupLength;
    private int powerupLengthUpgrade;
    
    void Awake()
    {
        player = FindObjectOfType<HamtoroController>();
        vacuumComponent = FindObjectOfType<Vacuum>();
        if(vacuumComponent == null) Debug.LogError("Vacuum not found");
        powerupLength = 5;
        powerupLengthUpgrade = 0;
    }

    public void UpdatePowerupDuration(int d)
    {
        powerupLengthUpgrade = d;
    }

    public int GetPowerupLength()
    {
        return powerupLengthUpgrade + powerupLength;
    }
    public void SetPowerupLength(int newLength)
    {
        powerupLength = newLength;
    }
    public void ActivatePowerup(bool vacuum, bool shield, bool ammo, int duration)
    {
        powerupLength = duration + powerupLengthUpgrade;

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
        vacuumComponent.setActive(true); //turn on vacuum physics
        player.SetVacuum(true); //change animator
        yield return new WaitForSeconds(powerupLength);
        vacuumComponent.setActive(false);
        vacuumComponent.ResetAllSeeds();
        player.SetVacuum(false);
        vacuumRunning = false;
    }

    IEnumerator ShieldRoutine() 
    {
        shieldRunning = true;
        player.SetShield(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetShield(false);
        shieldRunning = false;
    }

    IEnumerator AmmoRoutine()
    {
        ammoRunning = true;
        player.SetAmmo(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetAmmo(false);
        ammoRunning = false;
    }
}
