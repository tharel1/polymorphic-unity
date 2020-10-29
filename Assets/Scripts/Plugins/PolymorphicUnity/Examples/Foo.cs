// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using chickengames.polymorphic;
using System;
using UnityEngine;


// This is a basic exemple
// The base class is a concrete class. You can use wathever base class type you want (concrete, abstract, interface)
namespace chickengames.polymorphicexample
{
   [Serializable]
   [Polymorphic(
      typeof(Foo),
      typeof(Foo1),
      typeof(Foo2)
   )]
   public class Foo : IPolymorphic
   {
      [SerializeField]
      private int field1;
      public int Field1 { get => field1; }

      [SerializeField]
      private string field2;
      public string Field2 { get => field2; }

      public override string ToString()
      {
         return "Field1:" + field1 + ", Field2:" + field2;
      }
   }

   public class Foo1 : Foo
   {
      [SerializeField]
      private int field3;
      public int Field3 { get => field3; }

      public override string ToString()
      {
         return base.ToString() + ", Field3:" + field3;
      }
   }

   public class Foo2 : Foo
   {
      [SerializeField]
      private int field4;
      public int Field4 { get => field4; }

      public override string ToString()
      {
         return base.ToString() + ", Field4:" + field4;
      }
   }
}