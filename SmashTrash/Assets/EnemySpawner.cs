using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour, IInteractible
{
    [SerializeField] private TMP_Dropdown enemyDropdown;
    [SerializeField] private Enemy[] enemies;

    public void OnInteract(Vector3 pointOfImpact)
    {
        if (this.enemyDropdown.value == 0)
        {
            return;
        }

        Instantiate(this.enemies[this.enemyDropdown.value - 1], pointOfImpact, Quaternion.identity);
    }
}
