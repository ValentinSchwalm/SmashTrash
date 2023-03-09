using UnityEngine;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour, IInteractible
{
    [SerializeField] private int prize;
    public UnityEvent onInteract;
    private Player player;
    private Animator animator;

    private void Start()
    {
        // Set variables
        this.player = FindObjectOfType<Player>();
        this.animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Player buys item if they have enough currency.
    /// </summary>
    public void OnInteract(Vector3 pointOfImpact)
    {
        // Check if enough currency
        if (this.player?.Currency < this.prize)
        {
            this.animator.SetTrigger("Failure");
            return;
        }

        // Buy item
        this.onInteract.Invoke();
        this.animator.SetTrigger("Success");
        this.player.Currency -= this.prize;
    }
}
