using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Sprite[] sprites;
    PlayerScript player;
    public Image[] currentimage;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentimage.Length; i++)
        {
            if (player.currentHp >= (i+1) && currentimage[i].sprite != sprites[1])
            {
                currentimage[i].sprite = sprites[1];
            }
            else if (player.currentHp < (i + 1) && currentimage[i].sprite != sprites[0])
            {
                currentimage[i].sprite = sprites[0];
            }
        }
    }
}
