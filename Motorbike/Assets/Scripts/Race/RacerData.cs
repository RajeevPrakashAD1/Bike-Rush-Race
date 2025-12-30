public enum EnemyState
{
    Normal,
    Pressured,
    Failing,
    Gone
}


[System.Serializable]
public class RacerData
{
    public string id;
    public float distance;
    public float speed;
    public int lane;
    public EnemyState state;
}