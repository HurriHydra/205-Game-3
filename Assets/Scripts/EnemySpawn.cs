using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject RobotPin;
    public GameObject Player;
    public float speed;
    public int EnemyAmount = 25;
    public int PositionX;
    public int PositionZ;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(RobotPin, new Vector3(PositionX, 0.75f, 30.0f), Quaternion.identity); This lags so bad lol, still trying to figure out a enemy spawner
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Spawner());
        RobotPin.transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

}

// This game needs more objectives to win so I plan on adding unique stuff to it. I kinda wanna make it similar to FNAF
// but where you have to shoot 5 special things to be able to survive the night
