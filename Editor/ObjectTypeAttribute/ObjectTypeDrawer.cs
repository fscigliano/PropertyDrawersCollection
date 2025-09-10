using System;
using System.Collections.Generic;
using NUnit.Compatibility;
using UnityEditor;
using UnityEngine;

namespace com.fscigliano.PropertyDrawersCollection.Editor
{
    /// <summary>
    /// Creation Date:   10/03/2020 10:52:58
    /// Product Name:    Property Drawers Collection
    /// Developers:      Franco Scigliano
    /// Description:
    /// ChangeLog:       13/10/2020 - Fixed multiple selection and validation inside lists when drag and drop succeeded.
    ///                  18/02/2021 - Fixed multiple edge cases. Removed object picker.
    /// </summary>
    [CustomPropertyDrawer(typeof(ObjectTypeAttribute))]
    public class ObjectTypeDrawer : CastedAttributePropertyDrawer<ObjectTypeAttribute>
    {
        protected override List<SerializedPropertyType> validTypes { get { return new List<SerializedPropertyType> (new[] { SerializedPropertyType.ObjectReference }); } }

        protected override void DoOnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.showMixedValue = property.serializedObject.isEditingMultipleObjects;

            EditorGUI.BeginProperty(position, label, property);
            ObjectTypeAttribute a = (attribute as ObjectTypeAttribute);
            
            
            Rect myRect = new Rect(position);
            myRect.x += myRect.width - 20f;
            myRect.width = 20f;
 
            if(GUI.Button(myRect, "", GUIStyle.none))
            {
                // no object picker
            }
            
            EditorGUI.BeginChangeCheck();
            var o =
                EditorGUI.ObjectField(position, label, property.objectReferenceValue, a.FilterType, true);
            if (EditorGUI.EndChangeCheck())
            {
                if (o != null)
                {
                    if (o is GameObject)
                    {
                        var c = (o as GameObject)?.GetComponent(a.FilterType);
                        if (c != null)
                        {
                            property.objectReferenceValue = c;
                        }
                        else
                        {
                            property.objectReferenceValue = null;
                        }
                    }
                    else
                    {
                        System.Type newType = o.GetType();
                        if (a.FilterType.IsCastableFrom(newType))
                        {
                            property.objectReferenceValue = o;
                        }
                        else
                        {
                            property.objectReferenceValue = null;
                        }
                    }
                }
                else
                {
                    property.objectReferenceValue = null;
                }
            }

            property.serializedObject.ApplyModifiedProperties();
            
            
            HandleDragAndDrop(position, property, a.FilterType);
            
            EditorGUI.EndProperty();
            EditorGUI.showMixedValue = false;
        }

        private void HandleDragAndDrop(Rect r, SerializedProperty property, Type t)
        {
            if (!r.Contains(Event.current.mousePosition))
            {
                return;
            }
            UnityEngine.Object firstComp = null;
            switch (Event.current.type)
            {
                case EventType.ContextClick:
                    break;
                case EventType.MouseDown:
                    DragAndDrop.PrepareStartDrag();
                    break;
                case EventType.MouseUp:
                    break;
                case EventType.MouseDrag:
                    DragAndDrop.StartDrag("drag-and-drop-objecttype");
                    break;
 
                case EventType.DragUpdated:
                    firstComp = GetComponentFromDragged(DragAndDrop.objectReferences, t);
                    if (firstComp!= null)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                    }
                    break;
 
                case EventType.Repaint:
                    firstComp = GetComponentFromDragged(DragAndDrop.objectReferences, t);
                    if (firstComp!= null)
                    {
                        if (DragAndDrop.visualMode == DragAndDropVisualMode.None ||
                            DragAndDrop.visualMode == DragAndDropVisualMode.Rejected)
                            break;

                        EditorGUI.DrawRect(r, new Color(0.5f, 0.5f, 0.7f, 0.3f));
                    }
                    break;
                case EventType.DragPerform:
                    firstComp = GetComponentFromDragged(DragAndDrop.objectReferences, t);
                    if (firstComp!= null)
                    {
                        property.objectReferenceValue = firstComp;
                        DragAndDrop.AcceptDrag();
                        DragAndDrop.activeControlID = 0;
                    }
                    break;
                case EventType.DragExited:
                    if (GUI.enabled)
                        HandleUtility.Repaint();
                    break;
            }
            property.serializedObject.ApplyModifiedProperties();
        }

        private UnityEngine.Object GetComponentFromDragged(UnityEngine.Object[] objectReferences, Type t)
        {
            for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
            {
                var o = DragAndDrop.objectReferences[i];
                if (o is GameObject go)
                {
                    var c = go.GetComponent(t);
                    if (c != null)
                    {
                        return c;
                    }
                }
            }
            return null;
        }
    }
}