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
        static readonly NetherWars.Haste hasteComponent = new NetherWars.Haste();

        public bool isHaste {
            get { return HasComponent(ComponentIds.Haste); }
            set {
                if (value != isHaste) {
                    if (value) {
                        AddComponent(ComponentIds.Haste, hasteComponent);
                    } else {
                        RemoveComponent(ComponentIds.Haste);
                    }
                }
            }
        }

        public Entity IsHaste(bool value) {
            isHaste = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherHaste;

        public static IMatcher Haste {
            get {
                if (_matcherHaste == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Haste);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherHaste = matcher;
                }

                return _matcherHaste;
            }
        }
    }
}
