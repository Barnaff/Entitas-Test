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
        public NetherWars.TurnPhase turnPhase { get { return (NetherWars.TurnPhase)GetComponent(ComponentIds.TurnPhase); } }

        public bool hasTurnPhase { get { return HasComponent(ComponentIds.TurnPhase); } }

        public Entity AddTurnPhase(NetherWars.TurnPhase.eTurnPhase newPhase) {
            var component = CreateComponent<NetherWars.TurnPhase>(ComponentIds.TurnPhase);
            component.Phase = newPhase;
            return AddComponent(ComponentIds.TurnPhase, component);
        }

        public Entity ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase newPhase) {
            var component = CreateComponent<NetherWars.TurnPhase>(ComponentIds.TurnPhase);
            component.Phase = newPhase;
            ReplaceComponent(ComponentIds.TurnPhase, component);
            return this;
        }

        public Entity RemoveTurnPhase() {
            return RemoveComponent(ComponentIds.TurnPhase);
        }
    }

    public partial class Pool {
        public Entity turnPhaseEntity { get { return GetGroup(Matcher.TurnPhase).GetSingleEntity(); } }

        public NetherWars.TurnPhase turnPhase { get { return turnPhaseEntity.turnPhase; } }

        public bool hasTurnPhase { get { return turnPhaseEntity != null; } }

        public Entity SetTurnPhase(NetherWars.TurnPhase.eTurnPhase newPhase) {
            if (hasTurnPhase) {
                throw new EntitasException("Could not set turnPhase!\n" + this + " already has an entity with NetherWars.TurnPhase!",
                    "You should check if the pool already has a turnPhaseEntity before setting it or use pool.ReplaceTurnPhase().");
            }
            var entity = CreateEntity();
            entity.AddTurnPhase(newPhase);
            return entity;
        }

        public Entity ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase newPhase) {
            var entity = turnPhaseEntity;
            if (entity == null) {
                entity = SetTurnPhase(newPhase);
            } else {
                entity.ReplaceTurnPhase(newPhase);
            }

            return entity;
        }

        public void RemoveTurnPhase() {
            DestroyEntity(turnPhaseEntity);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTurnPhase;

        public static IMatcher TurnPhase {
            get {
                if (_matcherTurnPhase == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TurnPhase);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTurnPhase = matcher;
                }

                return _matcherTurnPhase;
            }
        }
    }
}
