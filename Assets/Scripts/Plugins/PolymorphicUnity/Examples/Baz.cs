// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using chickengames.polymorphic;
using System;
using UnityEngine;


// This is an example with the factory pattern
// You can use the factory pattern with the Data resilience pattern
// The base class is an interface. You can use wathever base class type you want (concrete, abstract, interface)
namespace chickengames.polymorphicexample
{
   [Serializable]
   [Polymorphic(
      typeof(Baz1),
      typeof(Baz2)
   )]
   public abstract class Baz : IPolymorphic
   {
      public abstract void DoSomething();
   }



   [PolymorphFactory(typeof(Baz1Factory))]
   public class Baz1 : Baz
   {
      // SINGLETON
      private Baz1() { }
      public static Baz1 Instance { get; } = new Baz1();
      // SINGLETON

      public override void DoSomething()
      {
         Debug.Log("Baz1 !");
      }
   }

   public class Baz1Factory : IPolymorphFactory<Baz1>
   {
      public Baz1 CreatePolymorph()
      {
         return Baz1.Instance;
      }
   }



   [PolymorphFactory(typeof(Baz2Factory))]
   public class Baz2 : Baz
   {
      [SerializeField]
      private int val;
      public int Val { get => val; set => val = value; }

      public override void DoSomething()
      {
         Debug.Log("Baz2 with " + val + " !");
      }
   }

   public class Baz2Factory : IPolymorphFactory<Baz2>
   {
      public Baz2 CreatePolymorph()
      {
         return new Baz2()
         {
            Val = 100
         };
      }
   }
}