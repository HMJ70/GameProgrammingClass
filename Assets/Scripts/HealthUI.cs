using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image HeartPrefab;
    public Sprite fullheartsprite;
    public Sprite emptyheartsprite;

    private List<Image> hearts = new List<Image>();

    public void setmaxhearts(int maxhearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxhearts; i++)
        {
            Image newheart = Instantiate(HeartPrefab,transform);
            newheart.sprite = fullheartsprite;
            newheart.color = Color.red;
            hearts.Add(newheart);
        }
    }
   
    public void updateheart(int currenthealth)
    {
        for (int i = 0;i < hearts.Count;i++)
        {
            if (i < currenthealth)
            {
                hearts[i].sprite = fullheartsprite;
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].sprite = emptyheartsprite;
                hearts[i].color = Color.black;
            }
        }
    }


}
