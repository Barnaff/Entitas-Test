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
        static readonly NetherWars.SummoningSickness summoningSicknessComponent = new NetherWars.SummoningSickness();

        public bool isSummoningSickness {
            get { return HasComponent(ComponentIds.SummoningSickness); }
            set {
                if (value != isSummoningSickness) {
                    if (value) {
                        AddComponent(ComponentIds.SummoningSickness, summoningSicknessComponent);
                    } else {
                        RemoveComponent(ComponentIds.SummoningSickness);
                    }
                }
            }
        }

        public Entity IsSummoningSickness(bool value) {
            isSummoningSickness = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSummoningSickness;

        public static IMatcher SummoningSickness {
            get {
                if (_matcherSummoningSickness == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SummoningSickness);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSummoningSickness = matcher;
                }

                return _matcherSummoningSickness;
            }
        }
    }
}
