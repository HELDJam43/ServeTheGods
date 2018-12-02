using UnityEngine;

public class Level: MonoBehaviour
{
    public int _index;

    public int _customerInitialCount = 1;
    public int _customerTotalCount = 2;
    public float _customerSpawnRate = 3;

    public int _godInitialCount = 0;
    public int _godTotalCount = 0;
    public float _godSpawnRate = 4;

    public int _foodInitialCount = 1;
    public int _foodTotalCount = 6;
    public float _foodSpawnRate = 2;

    public int _scoreAdditive = 300;
    public LevelManager.StarRank _startingStarLevel;
    public int _startingGodRageLevel = 0;
}
