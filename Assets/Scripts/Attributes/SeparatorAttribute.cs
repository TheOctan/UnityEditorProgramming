﻿using System;
using UnityEngine;

namespace OctanGames.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute
    {
        public readonly float Height;
        public readonly float Spacing;

        public SeparatorAttribute(float height = 1, float spacing = 10)
        {
            Height = height;
            Spacing = spacing;
        }
    }
}