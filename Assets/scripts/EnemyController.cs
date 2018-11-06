using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 speed;

    Vector3 velocity;

    public CameraScript camScript;

    public float maxHp;

    float currentHp;

    SpriteRenderer spriterenderer;

    GameObject ExplosionObject;

    public int playerLifeTaken;

    public int scorePoints;

    bool HasTouchedPlayer;

    Bounds bounds;

    LayerMask plyrmask;

    public virtual void Start()
    {
        HasTouchedPlayer = false;
        ExplosionObject = Resources.Load<GameObject>("Explosion");
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        currentHp = maxHp;
        spriterenderer = GetComponent<SpriteRenderer>();
        plyrmask = 512;
    }

    // Update is called once per frame
    public virtual void Update()
    {

        OutOfBounds();

        velocity = speed;

        if (!HasTouchedPlayer)
            CheckCollisionWithPlayer();

        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    public void DecreaseHp()
    {
        currentHp--;

        if (currentHp == 0)
        {
            GameObject XplosionObj = Instantiate(ExplosionObject);
            XplosionObj.transform.position = transform.position;

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameHandler>().score += scorePoints;

            Destroy(gameObject);
        }
        else
            StartCoroutine(HitColorChange());
    }

    private void OutOfBounds()
    {
        if (transform.position.x <= (camScript.transform.position.x - camScript.camSize.x) - 0.25f)
        {
            DecreasePlayerHp(playerLifeTaken);
            Destroy(gameObject);
        }
    }

    public void DecreasePlayerHp(int times)
    {
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < times; i++)
        {
            if (A != null)
                A.GetComponent<PlayerScript>().DecreaseHP();
        }
    }

    private IEnumerator HitColorChange()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.white;
    }

    void CheckCollisionWithPlayer()
    {

        bounds = GetComponent<BoxCollider2D>().bounds;

        Vector2 boundssize = new Vector2(bounds.size.x, bounds.size.y);

        RaycastHit2D hit = Physics2D.BoxCast(bounds.center, boundssize, 0, Vector2.right, 0f, plyrmask);

        if (hit)
        {
            for (int i = 0; i < playerLifeTaken; i++)
            {
                hit.collider.GetComponent<PlayerScript>().DecreaseHP();
            }

            HasTouchedPlayer = true;
        }
    }
}
