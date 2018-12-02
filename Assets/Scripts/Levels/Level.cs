using UnityEngine;

public class Level: MonoBehaviour
{
    public int _index;

    public int _customerInitialCount;
    public int _customerTotalCount;
    public float _customerSpawnRate = 3;

    public int _godInitialCount;
    public int _godTotalCount;
    public float _godSpawnRate = 4;

    public int _foodInitialCount;
    public int _foodTotalCount;
    public float _foodSpawnRate = 2;

    public int _scoreMultiplier = 2;
    public LevelManager.StarRank _startingStarLevel;
    public int _startingGodRageLevel = 0;
}
