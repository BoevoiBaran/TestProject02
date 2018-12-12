using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private string ballId = "ball-01";

    [SerializeField] private float interval = 1.0f; //интервал с которым создаются шары
    [SerializeField] private float minX; // координаты респауна шаров  по Х
    [SerializeField] private float maxX;

    [SerializeField] private int minBallScore; // количество очков которое присваивается шару при создании
    [SerializeField] private int maxBallScore;

    [SerializeField] private float ballSpeedIncrease; // переменная которая увеличивает скорость шаров со временем
    [SerializeField] private float intervalBallSpeedIncrease; // интервал с которым увеличивается скорость


    private void Start()
    {
        InvokeRepeating("Spawn", 1.0f, interval);
        InvokeRepeating("BallSpeedIncrease", 10.0f, intervalBallSpeedIncrease);
    }

    private void BallSpeedIncrease()
    {
        ballSpeedIncrease++;
    }

    private void Spawn()
    {
        Vector2 pos = new Vector2(Random.Range(minX, maxX), transform.position.y);


        Ball b = (Ball)ObjectsPool.Instanse.GetObject(ballId);
        if (!b)
        {
            return;
        }

        b.transform.position = pos;
        b.transform.rotation = Quaternion.identity;
        int score = Random.Range(minBallScore, maxBallScore);
        b.Score = score;
        b.Speed = score + ballSpeedIncrease;

        b.gameObject.SetActive(true);
    }

}
