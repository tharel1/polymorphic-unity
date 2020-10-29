// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using System;

namespace chickengames.polymorphic
{

   [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
   public class PolymorphFactoryAttribute : Attribute
   {
      public Type FactoryType { get; private set; }

      public PolymorphFactoryAttribute(Type factoryType)
      {
         FactoryType = factoryType;
      }
   }
}