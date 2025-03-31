using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageManager : DamageManager
{
    [SerializeField] List<GameObject> gameUIs;
    [SerializeField] GameObject EndUI;
    [SerializeField] HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(currentHealth);

    }
    public override void OnDestroy()
    {
        foreach(GameObject gameUI in gameUIs) {
            if(gameUI != null)
            gameUI.SetActive(false);
        }
        DisableAllObjects();
        EndUI.SetActive(true);
        base.OnDestroy();
    }
    void DisableAllObjects()
{
    GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("HighThreat");
    foreach (GameObject obj in objectsToDisable)
    {
        obj.SetActive(false);
    }
}
}
