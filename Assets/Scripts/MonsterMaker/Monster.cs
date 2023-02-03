using OctanGames.Attributes;
using UnityEngine;

namespace OctanGames.MonsterMaker
{
    [SelectionBase]
    public class Monster : MonoBehaviour
    {
        [SerializeField] private MonsterData _data;
        [Separator]
        [SerializeField] private float _health;

        public MonsterData Data => _data;

        private void Awake()
        {
            Debug.Log($"Name {_data.Name}\nDamage {_data.Damage}");
        }

        private void OnDrawGizmosSelected()
        {
            if (_data == null) return;

            Transform t = transform;
            Vector3 position = t.position + Vector3.up * 0.5f;
            Vector3 direction = t.forward * _data.RangeOfAwareness;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, _data.RangeOfAwareness);

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(position, direction);

            Gizmos.color = Color.white;
        }
    }
}
