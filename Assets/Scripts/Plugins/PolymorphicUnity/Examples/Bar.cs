// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using chickengames.polymorphic;
using System;
using UnityEngine;


// This is an example with the Data resilience pattern
// The base class is a concrete class. You can use wathever base class type you want (concrete, abstract, interface)
namespace chickengames.polymorphicexample
{
   [Serializable]
   [Polymorphic(
      typeof(Bar),
      typeof(Bar1),
      typeof(Bar2)
   )]
   public class Bar : IPolymorphic<Bar>
   {
      [SerializeField]
      private int field1;
      public int Field1 { get => field1; }

      [SerializeField]
      private string field2;
      public string Field2 { get => field2; }

      public Bar ConvertPolymorph(Bar t)
      {
         t.field1 = field1;
         t.field2 = field2;
         return t;
      }

      public override string ToString()
      {
         return "Field1:" + field1 + ", Field2:" + field2;
      }
   }

   public class Bar1 : Bar
   {
      [SerializeField]
      private int field3;
      public int Field3 { get => field3; }

      public override string ToString()
      {
         return base.ToString() + ", Field3:" + field3;
      }
   }

   public class Bar2 : Bar
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