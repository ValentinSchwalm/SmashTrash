using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, IInteractible
{
    [SerializeField] private int goldAmount;
    private Player player;

    private void Start()
    {
        this.player = FindObjectOfType<Player>();
    }

    public void OnInteract(Vector3 pointOfImpact)
    {
        this.player.Currency += this.goldAmount;
        this.player.UpdateGold();
        Destroy(this.gameObject);
    }
}
