using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;

    Vector3 velocity;

    Bounds bounds;

    CameraScript camScript;

    public LayerMask enemyMask;

    public LayerMask playerMask;

    public bool fromEnemy;

    private void Start()
    {
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {

        bounds = GetComponent<BoxCollider2D>().bounds;

        velocity.x = speed;

        CollideCheck();

        transform.Translate(velocity * Time.deltaTime, Space.World);

        OutOfBounds();
    }

    void OutOfBounds()
    {
        if ((transform.position.x + (velocity.x * Time.deltaTime)) > (camScript.transform.position.x + camScript.camSize.x + 3f) ||
            (transform.position.x + (velocity.x * Time.deltaTime)) < (camScript.transform.position.x - camScript.camSize.x - 3f))
            Destroy(gameObject);

    }

    void CollideCheck()
    {
        Vector2 boundssize = new Vector2(bounds.size.x, bounds.size.y);

        if (!fromEnemy)
        {
            RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boundssize, 0, Vector2.right, 0f, enemyMask);

            if (hit)
            {
                hit.collider.GetComponent<EnemyController>().DecreaseHp();
                Destroy(gameObject);
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boundssize, 0, Vector2.right, 0f, playerMask);

            if (hit)
            {
                hit.collider.GetComponent<PlayerScript>().DecreaseHP();
                Destroy(gameObject);
            }
        }

    }
}