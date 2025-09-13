using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    public abstract class HidingAttributeDrawer : PropertyDrawer
    {
        private static bool CheckShouldHide(SerializedProperty property)
        {
            try
            {
                bool shouldHide = false;

                var targetObject = Utilities.GetTargetObjectOfProperty(property);
                var type = targetObject.GetType();

                FieldInfo field;
                do
                {
                    field = type.GetField(property.name,
                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    type = type.BaseType;
                } while (field == null && type != null);

                var customAttributes = field.GetCustomAttributes(typeof(HidingAttribute), false);

                // 'property' may be a property of a serialized class or collection inside the property.serializedObject.
                // In that case we get the serialized property just above 'property' so the ShouldDraw method can test the
                // HideIf attribute using FindPropertyRelative on the 'propertyParent'.
                SerializedProperty propertyParent = null;
                var propertyPath = property.propertyPath;
                var lastDot = propertyPath.LastIndexOf('.');
                if (lastDot > 0)
                {
                    var parentPath = propertyPath.Substring(0, lastDot);
                    propertyParent = property.serializedObject.FindProperty(parentPath);
                }

                HidingAttribute[] attachedAttributes = (HidingAttribute[])customAttributes;
                foreach (var hider in attachedAttributes)
                {
                    if (!ShouldDraw(property.serializedObject, propertyParent, hider))
                    {
                        shouldHide = true;
                    }
                }

                return shouldHide;
            }
            catch
            {
                return false;
            }
        }


        private static Dictionary<Type, Type> _typeToDrawerType;

        private static Dictionary<Type, PropertyDrawer> _drawerTypeToDrawerInstance;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!CheckShouldHide(property))
            {
                if (_typeToDrawerType == null)
                    PopulateTypeToDrawer();

                Type drawerType;
                var typeOfProp = Utilities.GetTargetObjectOfProperty(property).GetType();
                if (_typeToDrawerType.TryGetValue(typeOfProp, out drawerType))
                {
                    var drawer =
                        _drawerTypeToDrawerInstance.GetOrAdd(drawerType, () => CreateDrawerInstance(drawerType));
                    drawer.OnGUI(position, property, label);
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (CheckShouldHide(property))
                return -2;

            if (_typeToDrawerType == null)
                PopulateTypeToDrawer();

            Type drawerType;
            var typeOfProp = Utilities.GetTargetObjectOfProperty(property).GetType();
            if (_typeToDrawerType.TryGetValue(typeOfProp, out drawerType))
            {
                var drawer = _drawerTypeToDrawerInstance.GetOrAdd(drawerType, () => CreateDrawerInstance(drawerType));
                return drawer.GetPropertyHeight(property, label);
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private PropertyDrawer CreateDrawerInstance(Type drawerType)
        {
            return (PropertyDrawer)Activator.CreateInstance(drawerType);
        }

        private void PopulateTypeToDrawer()
        {
            _typeToDrawerType = new Dictionary<Type, Type>();
            _drawerTypeToDrawerInstance = new Dictionary<Type, PropertyDrawer>();
            var propertyDrawerType = typeof(PropertyDrawer);
            var targetType =
                typeof(CustomPropertyDrawer).GetField("m_Type", BindingFlags.Instance | BindingFlags.NonPublic);
            var useForChildren =
                typeof(CustomPropertyDrawer).GetField("m_UseForChildren",
                    BindingFlags.Instance | BindingFlags.NonPublic);

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            foreach (Type type in types)
            {
                if (propertyDrawerType.IsAssignableFrom(type))
                {
                    var customPropertyDrawers = type.GetCustomAttributes(true).OfType<CustomPropertyDrawer>().ToList();
                    foreach (var propertyDrawer in customPropertyDrawers)
                    {
                        var targetedType = (Type)targetType.GetValue(propertyDrawer);
                        _typeToDrawerType[targetedType] = type;

                        var usingForChildren = (bool)useForChildren.GetValue(propertyDrawer);
                        if (usingForChildren)
                        {
                            var childTypes = types.Where(t => targetedType.IsAssignableFrom(t) && t != targetedType);
                            foreach (var childType in childTypes)
                            {
                                _typeToDrawerType[childType] = type;
                            }
                        }
                    }
                }
            }
        }

        private static bool ShouldDraw(SerializedObject hidingobject, SerializedProperty serializedProperty,
            HidingAttribute hider)
        {
            if (hider is HideIfAttribute hideIf)
            {
                return HideIfAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, hideIf);
            }

            if (hider is HideIfNullAttribute hideIfNull)
            {
                return HideIfNullAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, hideIfNull);
            }

            if (hider is HideIfNotNullAttribute hideIfNotNull)
            {
                return HideIfNotNullAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, hideIfNotNull);
            }

            if (hider is HideIfEnumValueAttribute hideIfEnum)
            {
                return HideIfEnumValueAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, hideIfEnum);
            }

            if (hider is HideIfCompareValueAttribute hideIfCompare)
            {
                return HideIfCompareValueAttributeDrawer.ShouldDraw(hidingobject, serializedProperty, hideIfCompare);
            }

            Debug.LogWarning("Trying to check unknown hider loadingType: " + hider.GetType().Name);
            return false;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfAttributeDrawer : HidingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject hidingObject, SerializedProperty serializedProperty,
            HideIfAttribute attribute)
        {
            var prop = serializedProperty == null
                ? hidingObject.FindProperty(attribute.variable)
                : serializedProperty.FindPropertyRelative(attribute.variable);
            if (prop == null)
            {
                if (serializedProperty == null)
                {
                    var o = hidingObject.targetObject.GetType();
                    PropertyInfo propertyInfo = o.GetProperty(attribute.variable,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (propertyInfo != null)
                    {
                        bool value = (bool)propertyInfo.GetValue(hidingObject.targetObject);
                        return value == attribute.state;
                    }

                    return true;
                }
                else
                {
                    object targetObject = GetTargetObjectOfProperty(serializedProperty);
                    if (targetObject != null)
                    {
                        // Use reflection to access the protected property
                        PropertyInfo propertyInfo = targetObject.GetType().GetProperty(attribute.variable,
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        if (propertyInfo != null)
                        {
                            object valueObj = propertyInfo.GetValue(targetObject, null);
                            bool value = (bool)valueObj;
                            return value == attribute.state;
                        }
                    }
                }
            }

            if (prop == null)
            {
                return true;
            }

            return prop.boolValue != attribute.state;
        }

        // Utility function to get the target object from a SerializedProperty
        private static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null)
            {
                return null;
            }

            string path = prop.propertyPath.Replace(".Array.data[", "["); // Adjust for arrays
            object obj = prop.serializedObject.targetObject;
            string[] elements = path.Split('.');

            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    // Handle array/list index
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "")
                        .Replace("]", ""));
                    obj = GetValue(obj, elementName, index);
                }
                else
                {
                    obj = GetValue(obj, element);
                }
            }

            return obj;
        }

        // Helper function to get the value of a field/property by name
        private static object GetValue(object source, string name, int index = -1)
        {
            if (source == null)
            {
                return null;
            }

            var type = source.GetType();
            FieldInfo field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field == null)
            {
                return null;
            }

            var value = field.GetValue(source);
            if (index >= 0 && value is System.Collections.IEnumerable enumerable)
            {
                var enm = enumerable.GetEnumerator();
                for (int i = 0; i <= index; i++)
                {
                    enm.MoveNext();
                }

                return enm.Current;
            }

            return value;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfNullAttribute))]
    public class HideIfNullAttributeDrawer : HidingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject hidingObject, SerializedProperty serializedProperty,
            HideIfNullAttribute attribute)
        {
            var prop = serializedProperty == null
                ? hidingObject.FindProperty(attribute.variable)
                : serializedProperty.FindPropertyRelative(attribute.variable);
            if (prop == null)
            {
                return true;
            }

            return prop.objectReferenceValue != null;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfNotNullAttribute))]
    public class HideIfNotNullAttributeDrawer : HidingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject hidingObject, SerializedProperty serializedProperty,
            HideIfNotNullAttribute attribute)
        {
            var prop = serializedProperty == null
                ? hidingObject.FindProperty(attribute.variable)
                : serializedProperty.FindPropertyRelative(attribute.variable);
            if (prop == null)
            {
                return true;
            }

            return prop.objectReferenceValue == null;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfEnumValueAttribute))]
    public class HideIfEnumValueAttributeDrawer : HidingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject hidingObject, SerializedProperty serializedProperty,
            HideIfEnumValueAttribute hideIfEnumValueAttribute)
        {
            var enumProp = serializedProperty == null
                ? hidingObject.FindProperty(hideIfEnumValueAttribute.variable)
                : serializedProperty.FindPropertyRelative(hideIfEnumValueAttribute.variable);
            var states = hideIfEnumValueAttribute.states;

            //enumProp.enumValueIndex gives the order in the enum list, not the actual enum value
            bool equal = states.Contains(enumProp.intValue);

            return equal != hideIfEnumValueAttribute.hideIfEqual;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfCompareValueAttribute))]
    public class HideIfCompareValueAttributeDrawer : HidingAttributeDrawer
    {
        public static bool ShouldDraw(SerializedObject hidingObject, SerializedProperty serializedProperty,
            HideIfCompareValueAttribute hideIfCompareValueAttribute)
        {
            var variable = serializedProperty == null
                ? hidingObject.FindProperty(hideIfCompareValueAttribute.variable)
                : serializedProperty.FindPropertyRelative(hideIfCompareValueAttribute.variable);
            var compareValue = hideIfCompareValueAttribute.value;

            switch (hideIfCompareValueAttribute.hideIf)
            {
                case HideIf.Equal: return variable.intValue != compareValue;
                case HideIf.NotEqual: return variable.intValue == compareValue;
                case HideIf.Greater: return variable.intValue <= compareValue;
                default: /*case HideIf.Lower:*/ return variable.intValue >= compareValue;
            }
        }
    }
}