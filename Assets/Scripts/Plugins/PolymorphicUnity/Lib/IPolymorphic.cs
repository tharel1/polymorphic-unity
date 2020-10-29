// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

namespace chickengames.polymorphic
{

   public interface IPolymorphic { }

   public interface IPolymorphic<T> : IPolymorphic
   {
      T ConvertPolymorph(T t);
   }
}