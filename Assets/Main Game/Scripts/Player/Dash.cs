using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float SideForce = 400f;
    public float DownForce = 275f;

    private Rigidbody2D rigidbody;
    private bool hasDashed;

    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasDashed || !Input.GetKey(KeyCode.LeftShift))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DashLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DashRight();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DashDown();
        }
    }

    void DashLeft()
    {
        rigidbody.AddForce(new Vector2(-SideForce, 0), ForceMode2D.Impulse);
        StartCoroutine(ResetDash());
    }

    void DashRight()
    {
        rigidbody.AddForce(new Vector2(SideForce, 0), ForceMode2D.Impulse);
        StartCoroutine(ResetDash());
    }

    void DashDown()
    {
        rigidbody.AddForce(new Vector2(0, -DownForce), ForceMode2D.Impulse);
        StartCoroutine(ResetDash());
    }

    IEnumerator ResetDash()
    {
        hasDashed = true;
        yield return new WaitForSeconds(0.5f);
        hasDashed = false;
    }
}
