// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using chickengames.polymorphic;

namespace chickengames.polymorphiceditor
{

   [CustomPropertyDrawer(typeof(IPolymorphic), true)]
   public class PolymorphicPropertyDrawer : PropertyDrawer
   {
#if UNITY_EDITOR
      private const float POPUP_HEIGHT = 20;

      private int selectedType = 0;
      private string[] options = new string[0];
      private GUIContent[] optionsContent = new GUIContent[0];

      private Type baseType;
      private List<Type> types = new List<Type>();
      private bool isList = false;

      private bool initialized = false;

      private void Init()
      {
         initialized = true;

         baseType = this.fieldInfo.FieldType;

         isList = !typeof(IPolymorphic).IsAssignableFrom(baseType);

         PolymorphicAttribute polymorphicAttribute = isList ?
            (PolymorphicAttribute)baseType.GenericTypeArguments[0].GetCustomAttribute(typeof(PolymorphicAttribute), true) :
            (PolymorphicAttribute)baseType.GetCustomAttribute(typeof(PolymorphicAttribute), true);
         if (polymorphicAttribute == null) return;

         types = polymorphicAttribute.Types.ToList();
         options = types.Select(t => t.Name).ToArray();
         optionsContent = options.Select(str => new GUIContent(str)).ToArray();
      }

      public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
      {
         if (!initialized)
            Init();

         SetSelectedClass(property);
         EditorGUI.BeginChangeCheck();
         selectedType = EditorGUI.Popup(PopupRect(position), label, selectedType, optionsContent);
         if (EditorGUI.EndChangeCheck())
         {
            SetPolymorphicObject(property);
         }
         EditorGUI.PropertyField(position, property, new GUIContent(), true);
      }

      public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
      {
         return EditorGUI.GetPropertyHeight(property);
      }

      private Rect PopupRect(Rect position)
      {
         return new Rect(position.x, position.y, position.width, POPUP_HEIGHT);
      }

      private void SetSelectedClass(SerializedProperty property)
      {
         object obj = property.GetValue();
         if (obj == null) return;
         Type type = obj.GetType();
         if (isList)
         {
            if (!baseType.GenericTypeArguments[0].IsAssignableFrom(type)) return;
         }
         else
         {
            if (!baseType.IsAssignableFrom(type)) return;
         }
         selectedType = types.IndexOf(type);
      }

      private void SetPolymorphicObject(SerializedProperty property)
      {
         Type type = types[selectedType];
         PolymorphFactoryAttribute factoryAttribute = (PolymorphFactoryAttribute)type.GetCustomAttribute(typeof(PolymorphFactoryAttribute), true);

         object created;
         if (factoryAttribute == null)
         {
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            created = constructor.Invoke(new object[0]);
         }
         else
         {
            ConstructorInfo factoryConstructor = factoryAttribute.FactoryType.GetConstructor(Type.EmptyTypes);
            object factory = factoryConstructor.Invoke(new object[0]);
            MethodInfo factoryMethod = factoryAttribute.FactoryType.GetMethod("CreatePolymorph");
            created = factoryMethod.Invoke(factory, new object[0]);
         }


         MethodInfo convertPolymorphMethod = type.GetMethod("ConvertPolymorph", new Type[] { type });
         if (convertPolymorphMethod != null)
         {
            object obj = property.GetValue();
            if (baseType.IsInstanceOfType(obj))
               created = convertPolymorphMethod.Invoke(obj, new object[] { created });
         }
         property.SetValue(created);
      }
#endif // UNITY_EDITOR
   }
}