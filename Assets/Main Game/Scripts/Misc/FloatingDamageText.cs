using UnityEngine;
using UnityEngine.UI;

public class FloatingDamageText : MonoBehaviour
{
    private Text textComponent;

    private float moveSpeed => isBoss ? 10 / 1000f : 1/1000f;
    private Vector3 moveDirection;
    private bool canMove;
    private bool hasRotated;
    private bool isBoss;

    void Start()
    {
        var possibleDirections = new[]
        {
            transform.up, 
            transform.up + transform.right,
            transform.up - transform.right,
        };
        moveDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];
        moveDirection.z = 0;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + moveDirection, moveSpeed * Time.time);
        }
    }

    public void CreateDamage(string text, Enemy enemy, bool isCrit, bool isBoss = false)
    {
        this.textComponent = GetComponentInChildren<Text>();
        this.textComponent.text = text;
        this.textComponent.color = isCrit ? Color.yellow : Color.white;
        this.canMove = true;
        this.isBoss = isBoss;
        this.textComponent.rectTransform.sizeDelta = isCrit ? new Vector2(65, 65) : new Vector2(50, 50);
    }

    public void CreateExperience(string exp)
    {
        this.textComponent = GetComponentInChildren<Text>();
        this.textComponent.text = exp;
        this.textComponent.color = Color.magenta;
        this.canMove = true;
        this.textComponent.rectTransform.sizeDelta = new Vector2(50f, 50f);
    }
}
