// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace chickengames.polymorphiceditor
{

   public static class SerializedPropertyExtension
   {
#if UNITY_EDITOR
      // Gets value from SerializedProperty - even if value is nested
      public static object GetValue(this UnityEditor.SerializedProperty property)
      {
         object obj = property.serializedObject.targetObject;
         bool isArray = false;
         foreach (var path in property.propertyPath.Split('.'))
         {
            if (path.Equals("Array"))
            {
               isArray = true;
               continue;
            }
            if (isArray)
            {
               isArray = false;
               int index = int.Parse(path.Replace("data[", "").Replace("]", ""));
               IEnumerator<object> enumerator = ((IEnumerable<object>)obj).GetEnumerator();
               for (int i = -1; i < index; i++) enumerator.MoveNext();
               obj = enumerator.Current;
               continue;
            }
            var type = obj.GetType();
            FieldInfo field = type.GetField(path);
            obj = field.GetValue(obj);
         }
         return obj;
      }

      // Sets value from SerializedProperty - even if value is nested
      public static void SetValue(this UnityEditor.SerializedProperty property, object val)
      {
         object obj = property.serializedObject.targetObject;
         object container = obj;

         FieldInfo field = null;
         bool isArray = false;
         bool lastWasArray = false;
         int index = 0;
         foreach (var path in property.propertyPath.Split('.'))
         {
            lastWasArray = false;
            if (path.Equals("Array"))
            {
               isArray = true;
               continue;
            }
            if (isArray)
            {
               container = obj;
               index = int.Parse(path.Replace("data[", "").Replace("]", ""));
               obj = ((IList)obj)[index];
               isArray = false;
               lastWasArray = true;
               continue;
            }
            container = obj;
            var type = obj.GetType();
            field = type.GetField(path);
            obj = field.GetValue(obj);
         }

         if (lastWasArray)
            ((IList)container)[index] = val;
         else
            field.SetValue(container, val);

      }
#endif // UNITY_EDITOR
   }
}