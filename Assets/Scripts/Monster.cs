using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData _data;

    public MonsterData Data => _data;

    private void Awake()
    {
        Debug.Log($"Name {_data.Name}\nDamage {_data.Damage}");
    }
}
