using OctanGames.Attributes;
using UnityEngine;

namespace OctanGames.MonsterMaker
{
    [CreateAssetMenu(fileName = "MonsterData_", menuName = "UnitData/Monster")]
    public class MonsterData : ScriptableObject
    {
        [Header("General stats")]
        [SerializeField] private string _name;
        [SerializeField] private MonsterType _monsterType;
        [SerializeField, Range(0, 100)] private float _chanceToDropItem = 50f;
        [Tooltip("Radius size where monster will see the player")]
        [SerializeField, Min(0)] private float _rangeOfAwareness = 10f;

        [Separator]
        [SerializeField] private bool _canEnterCombat = true;

        [Header("Combat stats")]
        [SerializeField] private int _health = 1;
        [SerializeField] private int _speed = 1;
        [SerializeField] private int _damage = 1;

        [Separator, Header("Dialogue")]
        [SerializeField, TextArea] private string _battleCry;

        public string Name => _name;
        public MonsterType Type => _monsterType;
        public float ChanceToDropItem => _chanceToDropItem;
        public float RangeOfAwareness => _rangeOfAwareness;
        public bool CanEnterCombat => _canEnterCombat;
        public int Damage => _damage;
        public int Health => _health;
        public int Speed => _speed;
    }
}