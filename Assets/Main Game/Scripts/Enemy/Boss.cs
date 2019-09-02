using UnityEngine;

public class Boss : MonoBehaviour
{
    private Enemy enemy;
    private AIPunch punchScript;

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        punchScript = gameObject.GetComponent<AIPunch>();

        enemy.transform.localScale = new Vector3(0.02f, 0.02f, 1);
        enemy.Health = 500;
        enemy.IsBoss = true;
        punchScript.Force = 300;
    }
}
