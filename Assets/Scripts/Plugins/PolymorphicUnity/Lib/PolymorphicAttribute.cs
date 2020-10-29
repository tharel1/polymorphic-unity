// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using System;

namespace chickengames.polymorphic
{

   [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
   public class PolymorphicAttribute : Attribute
   {
      public Type[] Types { get; private set; }

      public PolymorphicAttribute(params Type[] types)
      {
         Types = types;
      }
   }
}