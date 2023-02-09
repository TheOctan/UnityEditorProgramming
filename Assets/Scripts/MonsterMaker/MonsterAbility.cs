using System;
using UnityEngine;

namespace OctanGames.MonsterMaker
{
    [Serializable]
    public class MonsterAbility
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damage = 1;
        [SerializeField] private ElementType _element;
    }
}