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

    private float powerupLength;
    
    void Awake()
    {
        player = FindObjectOfType<HamtoroController>();
        vacuumComponent = FindObjectOfType<Vacuum>();
        if(vacuumComponent == null) Debug.LogError("Vacuum not found");
    }
    
    
    public void ActivatePowerup(bool vacuum, bool shield, bool ammo, float duration)
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
        Debug.Log("VacuumRoutine started. Setting isVacuuming TRUE at " + Time.time + ", frame " + Time.frameCount);
        vacuumRunning = true;
        vacuumComponent.setActive(true); //turn on vacuum physics
        player.SetVacuum(true); //change animator
        yield return new WaitForSeconds(powerupLength);
        Debug.Log("VacuumRoutine ending. Setting isVacuuming FALSE at " + Time.time + ", frame " + Time.frameCount);
        vacuumComponent.setActive(false);
        player.SetVacuum(false);
        vacuumRunning = false;
    }

    IEnumerator ShieldRoutine() 
    {
        Debug.Log("Shield running");
        shieldRunning = true;
        player.SetShield(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetShield(false);
        shieldRunning = false;
    }

    IEnumerator AmmoRoutine()
    {
        Debug.Log("Ammo running"); 
        ammoRunning = true;
        player.SetAmmo(true);
        yield return new WaitForSeconds(powerupLength);
        player.SetAmmo(false);
        ammoRunning = false;
    }
}
