using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IHealthSystem
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private int currency;
    [SerializeField] private int healthpoints;
    private int maxHealthpoints;
    public Transform interactionTransform;
    private SaveLoadHighScore highScoreManager;

    private bool sucking;
    private SkeletonManager skeletonManager;

    [Header("UI")]
    [SerializeField] private Image healthbar;
    [SerializeField] private Image ammunitionbar;
    [SerializeField] private TextMeshProUGUI goldAmount;

    [Header("DebugUI")]
    [SerializeField] private TextMeshProUGUI rangeUI;
    [SerializeField] private TextMeshProUGUI ammoUI, healthUI, powerUI;

    private void Start()
    {
        this.maxHealthpoints = this.healthpoints;
        this.skeletonManager = FindObjectOfType<SkeletonManager>();
        this.UpdateGold();
        this.highScoreManager = FindObjectOfType<SaveLoadHighScore>();

        this.ammoUI.text = this.currentWeapon.MaxAmmunition.ToString();
        this.healthUI.text = this.healthpoints.ToString();
        this.powerUI.text = this.currentWeapon.Damage.ToString();
        this.rangeUI.text = this.currentWeapon.ShootingForce.ToString();
    }

    private void Update()
    {
        Vector3 pos1 = this.skeletonManager._listOfJoints[4].transform.position;
        Vector3 pos2 = this.skeletonManager._listOfJoints[8].transform.position;

        Vector3 newPos = pos1 + (pos2 - pos1).normalized * (Vector3.Distance(pos1, pos2) / 2);

        this.interactionTransform.position = newPos;

        if (this.sucking)
        {
            this.SecondaryFire();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(this.mainCamera.transform.position, (this.testTransform.position - this.mainCamera.transform.position).normalized * 100);
    }

    public int Currency 
    { 
        get { return this.currency; }
        set { this.currency = value; }
    }

    private void PrimaryFire()
    {
        this.currentWeapon.Shoot();

        this.ammunitionbar.fillAmount = (float)this.currentWeapon.Ammunition / (float)this.currentWeapon.MaxAmmunition;
    }

    private void SecondaryFire()
    {
        this.currentWeapon.Suck();
        this.ammunitionbar.fillAmount = (float)this.currentWeapon.Ammunition / (float)this.currentWeapon.MaxAmmunition;
    }

    private void Interact()
    {
        RaycastHit hit;

        Ray ray = new Ray(Camera.main.transform.position, (this.interactionTransform.position - Camera.main.transform.position).normalized * 100);
        
        if (!Physics.Raycast(ray, out hit)) { return; }
        
        IInteractible interactible = hit.collider.GetComponent<IInteractible>();
        
        if (interactible == null) { return; }

        interactible.OnInteract(hit.point);
    }

    public void OnShoot()
    {
        this.PrimaryFire();
        print("shoot");
    }

    public void OnSuckDown()
    {
        this.sucking = true;
        print("start sucking");
    }

    public void OnSuckUp()
    {
        this.sucking = false;
        this.currentWeapon.OnSuckStop();
        print("stop sucking");
    }

    public void OnInteract()
    {
        this.Interact();
        print("interact");
    }

    /// <summary>
    /// The players healthpoints will decrease by the amount of damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(int damage)
    {
        this.healthpoints -= damage;
        this.healthbar.fillAmount = (float)this.healthpoints / (float)this.maxHealthpoints;

        if (this.healthpoints <= 0)
        {
            this.Die();
        }
    }

    /// <summary>
    /// If the player receives damage and their healthpoints fall below 1 they die
    /// </summary>
    public void Die()
    {
        print("Player died!");
        this.healthpoints = this.maxHealthpoints;
        this.highScoreManager.SaveCurrentScore();
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Increases the players healthpoints by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealthpoints(int amount)
    {
        this.maxHealthpoints += amount;
        this.healthpoints = this.maxHealthpoints;
        this.healthUI.text = this.healthpoints.ToString();
    }

    /// <summary>
    /// Increases the ammunition of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponAmmunition(int amount)
    {
        this.currentWeapon.MaxAmmunition += amount;
        this.ammoUI.text = this.currentWeapon.MaxAmmunition.ToString();
    }

    /// <summary>
    /// Increases the damage of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponDamage(int amount)
    {
        this.currentWeapon.Damage += amount;
        this.powerUI.text = this.currentWeapon.Damage.ToString();
    }

    /// <summary>
    /// Increases the shooting force (range) of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponRange(float amount)
    {
        this.currentWeapon.ShootingForce += amount;
        this.rangeUI.text = this.currentWeapon.ShootingForce.ToString();
    }

    public void UpdateGold()
    {
        this.goldAmount.text = this.currency.ToString();
    }
}
