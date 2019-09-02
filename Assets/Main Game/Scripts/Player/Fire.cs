using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Projectile;
    public Transform FirePoint;

    [Range(0,100)]
    public int ManaCost;

    public float FireRate = 500;

    private float fireDelay;

    private GameObject playerObj;
    private Player playerComponent;

    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerComponent = playerObj.GetComponent<Player>();
    }
    
    // Start is called before the first frame update
    public bool FireShot()
    {
        if (Time.time <= fireDelay || playerComponent.Mana <= 0)
            return false;

        fireDelay = Time.time + FireRate / 1000;

        playerComponent.RemoveMana(ManaCost);

        Instantiate(Projectile, FirePoint.position, Quaternion.identity);
        return true;
    }
}