using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyController
{
    float Timerr;

    public GameObject Projectile;

    public override void Update()
    {
        base.Update();

        if (Timerr == 0)
            Shoot();

        Cooldown();
    }

    void Shoot()
    {
        GameObject A = Instantiate(Projectile, transform.position, transform.rotation);
        A.GetComponent<ProjectileScript>().fromEnemy = true;
        A.GetComponent<ProjectileScript>().speed *= -1;
        A.GetComponent<SpriteRenderer>().flipY = true;
        Timerr += Time.deltaTime;
    }

    void Cooldown()
    {
        if (Timerr > 0 && Timerr < 2)
            Timerr += Time.deltaTime;
        else if (Timerr >= 2)
            Timerr = 0;
    }
}
