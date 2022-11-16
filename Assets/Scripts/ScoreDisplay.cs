using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    public Sprite[] sprites = new Sprite[10];
    [SerializeField] GameObject number;
    float width;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        width = number.GetComponent<BoxCollider2D>().size.y;
    }

    void GetDigits(string scoreString)
    {
        while (transform.childCount < scoreString.Length)
            Instantiate(number, transform, transform);

        for (int i = 0; i < scoreString.Length; i++)
        {
            GameObject digit = transform.GetChild(i).gameObject;
            digit.GetComponent<SpriteRenderer>().sprite = sprites[scoreString[i] - '0'];
        }
    }

    void PositionDigits()
    {
        float totalWidth = width * transform.childCount;
        float startX = -(totalWidth / 2f);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject digit = transform.GetChild(i).gameObject;
            float x = startX + i*width;
            digit.transform.position = new Vector3(x, transform.position.y, 0);
        }
    }

    void Update()
    {
        string scoreString = scoreKeeper.score.ToString();
        GetDigits(scoreString);
        PositionDigits();
    }
}
