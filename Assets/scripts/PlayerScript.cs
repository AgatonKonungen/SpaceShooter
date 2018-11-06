using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector2 velocity;

    CameraScript camscript;

    public float moveSpeed;

    float inputY;

    Bounds bounds;

    Transform FirePoint;

    public GameObject Projectile;

    public float maxHp;

    GameObject Explosion;

    public float currentHp;

    SpriteRenderer spriterenderer;

    // Use this for initialization
    void Start()
    {
        camscript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        bounds = GetComponent<BoxCollider2D>().bounds;
        FirePoint = GetComponentInChildren<Transform>();
        currentHp = maxHp;
        spriterenderer = GetComponent<SpriteRenderer>();
        Explosion = Resources.Load<GameObject>("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxisRaw("Vertical");

        Movement();

        Firing();
    }

    void Movement()
    {
        velocity.y = (inputY * moveSpeed);

        if (BoundsCheck())
            transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    bool BoundsCheck()
    {
        float editedVelocity = velocity.y * Time.deltaTime;


        if ((transform.position.y + editedVelocity) >= -camscript.camSize.y + (bounds.size.y / 4)
            && (transform.position.y + editedVelocity) <= camscript.camSize.y - (bounds.size.y / 4))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Firing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject A = Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
        }
    }

    public void DecreaseHP()
    {
        currentHp--;

        if (currentHp == 0)
        {
            GameObject A = Instantiate(Explosion);
            A.transform.position = transform.position;
            A.GetComponent<AnimationScript>().IsPlayerSpawned = true;
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(HitColorChange());
        }
    }

    private IEnumerator HitColorChange()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.white;
    }
}
