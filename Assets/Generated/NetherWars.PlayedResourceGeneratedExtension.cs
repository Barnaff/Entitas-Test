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
        static readonly NetherWars.PlayedResource playedResourceComponent = new NetherWars.PlayedResource();

        public bool isPlayedResource {
            get { return HasComponent(ComponentIds.PlayedResource); }
            set {
                if (value != isPlayedResource) {
                    if (value) {
                        AddComponent(ComponentIds.PlayedResource, playedResourceComponent);
                    } else {
                        RemoveComponent(ComponentIds.PlayedResource);
                    }
                }
            }
        }

        public Entity IsPlayedResource(bool value) {
            isPlayedResource = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayedResource;

        public static IMatcher PlayedResource {
            get {
                if (_matcherPlayedResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PlayedResource);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPlayedResource = matcher;
                }

                return _matcherPlayedResource;
            }
        }
    }
}
