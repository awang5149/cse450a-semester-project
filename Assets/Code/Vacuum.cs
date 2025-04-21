using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    
    private bool isActive = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActive) return;
        
        if (collision.gameObject.TryGetComponent<SeedBehavior>(out SeedBehavior seed))
        {
            seed.setTarget(transform.parent.position);
        }
    }

    public void setActive(bool flag)
    {
        isActive = flag;
    }
    public void ResetAllSeeds()
    {
        SeedBehavior[] seeds = FindObjectsOfType<SeedBehavior>();
        foreach (var seed in seeds)
        {
            seed.ClearTarget();
        }
    }
    
}
