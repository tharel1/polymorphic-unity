// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using System.Collections.Generic;
using UnityEngine;

namespace chickengames.polymorphicexample
{
   [CreateAssetMenu(fileName = "PolymorphicScriptableObjectExample", menuName = "PolymorphicExample/ScriptableObject")]
   public class ScriptableObjectExample : ScriptableObject
   {
      [SerializeReference]
      public Foo single = new Foo();

      [Space]
      [SerializeReference]
      public List<Foo> many = new List<Foo>();
   }
}