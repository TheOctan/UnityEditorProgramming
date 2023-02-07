using OctanGames.Attributes;
using UnityEditor;
using UnityEngine;

namespace OctanGames.Drawers
{
    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            var separatorAttribute = (SeparatorAttribute)attribute;

            var separatorRect = new Rect(position.xMin,
                position.yMin + separatorAttribute.Spacing,
                position.width,
                separatorAttribute.Height);

            EditorGUI.DrawRect(separatorRect, Color.gray);
        }

        public override float GetHeight()
        {
            var separatorAttribute = (SeparatorAttribute)attribute;
            return separatorAttribute.Spacing * 2 + separatorAttribute.Height;
        }
    }
}