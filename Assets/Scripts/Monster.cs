using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("General stats")]
    [SerializeField] private string _name;
    [SerializeField] private MonsterType _monsterType;
    [SerializeField, Range(0, 100)] private float _chanceToDropItem = 50f;
    [Tooltip("Radius size where monster will see the player")]
    [SerializeField] private float _rangeOfAwareness;

    [Header("Combat stats")]
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    [Header("Dialogue")]
    [SerializeField, TextArea] private string _battleCry;
}
