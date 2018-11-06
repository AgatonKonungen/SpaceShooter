using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Sprite[] sprites;

    int bild;

    SpriteRenderer spriterender;

    float TimerWaitfor = 0.08f;

    public bool IsPlayerSpawned;

    // Use this for initialization
    void Start()
    {
        bild = -1;
        spriterender = GetComponent<SpriteRenderer>();
        if (IsPlayerSpawned)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameHandler>().playerExplosion = gameObject;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameHandler>().isExplosionSet = true;
        }
        StartCoroutine(Animera());
    }

    IEnumerator Animera()
    {
        bild++;
        if (bild < sprites.Length)
            spriterender.sprite = sprites[bild];
        yield return new WaitForSeconds(TimerWaitfor);
        StartCoroutine(Animera());
        Destroy(gameObject, TimerWaitfor * sprites.Length);
    }
}
