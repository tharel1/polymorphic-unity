// This code is under MIT license, you can find the complete file here : https://github.com/Mabbutnem/polymorphic-unity/blob/main/LICENSE

using UnityEngine;

namespace chickengames.polymorphicexample
{
   public class MonoBehaviorExample : MonoBehaviour
   {
      [SerializeReference]
      public Foo singleFoo = new Foo();

      [Space]
      [SerializeReference]
      public Bar singleBar = new Bar();

      [Space]
      [SerializeReference]
      public Baz singleBaz = Baz1.Instance;

      private void Start()
      {
         Debug.Log(singleFoo);
         Debug.Log(singleBar);
         singleBaz.DoSomething();
      }
   }
}