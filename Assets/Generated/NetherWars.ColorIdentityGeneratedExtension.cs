//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public NetherWars.ColorIdentity colorIdentity { get { return (NetherWars.ColorIdentity)GetComponent(ComponentIds.ColorIdentity); } }

        public bool hasColorIdentity { get { return HasComponent(ComponentIds.ColorIdentity); } }

        public Entity AddColorIdentity(NetherWars.eColorType newColors) {
            var component = CreateComponent<NetherWars.ColorIdentity>(ComponentIds.ColorIdentity);
            component.Colors = newColors;
            return AddComponent(ComponentIds.ColorIdentity, component);
        }

        public Entity ReplaceColorIdentity(NetherWars.eColorType newColors) {
            var component = CreateComponent<NetherWars.ColorIdentity>(ComponentIds.ColorIdentity);
            component.Colors = newColors;
            ReplaceComponent(ComponentIds.ColorIdentity, component);
            return this;
        }

        public Entity RemoveColorIdentity() {
            return RemoveComponent(ComponentIds.ColorIdentity);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherColorIdentity;

        public static IMatcher ColorIdentity {
            get {
                if (_matcherColorIdentity == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ColorIdentity);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherColorIdentity = matcher;
                }

                return _matcherColorIdentity;
            }
        }
    }
}
