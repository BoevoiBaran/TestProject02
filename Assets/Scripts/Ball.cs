using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IPoolable
{
    private LevelController controller;

    private int score; // количество очков
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private float speed; // скорость
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private Transform obj; //трансформ для задания размера шара в зависимости от очков

    private Vector3 direction = Vector3.up; // направление движение шара

    private SpriteRenderer sprite;
    public Sprite[] image; // массив из спрайтов который рандомно присваивает шару внешний вид

    [SerializeField] private GameObject destroyEffect; // эффект частиц который появляется на месте уничтоженного шара

    //  реализация интерфейса для пула объектов

    [SerializeField] private string poolId = "ball-01";
    public string PoolId
    {
        get
        {
            return poolId;
        }
    }

    [SerializeField] private int objectsCount = 50;
    public int ObjectsCount
    {
        get
        {
            return objectsCount;
        }
    }

    //конец реализации интерфейся

    private void Awake()
    {
        controller = FindObjectOfType<LevelController>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        obj = GetComponent<Transform>();

    }

    private void Start()
    {
        Initialaize();
        SpriteRandomInst();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Initialaize()
    {
        obj.localScale = new Vector2(1.0f + (1.0f / (float)score), 1.0f + (1.0f / (float)score));
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }


    public void OnClick()
    {
        if (!controller.isGameOver)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            controller.Score += score;
            controller.ScoreRefresh();
            gameObject.SetActive(false);
        }
    }


    private void SpriteRandomInst()
    {
        int rand = Random.Range(0, image.Length);
        sprite.sprite = image[rand];
    }

}
