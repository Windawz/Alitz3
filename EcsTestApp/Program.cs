using Alitz3.Ecs;

namespace Alitz3.EcsTestApp;

record struct Health(float Value);
record struct Armor(float Value);
record struct Attacked(Entity Attacker, float Damage);
record struct Buffed(float Multiplier);

class BuffSystem : Ecs.System {
    public BuffSystem() : base(0) { }

    public override void Run(Querier querier) {
        querier.Get<Attacked>().ForEach((ref Attacked attacked) => {
            var buffs = querier.Get<Buffed>();
            if (buffs.ContainsEntity(attacked.Attacker)) {
                var multiplier = buffs[attacked.Attacker].Multiplier;
                attacked.Damage *= multiplier;
            }
        });
    }
}

class ArmorDamageSystem : Ecs.System {
    public ArmorDamageSystem() : base(0, typeof(BuffSystem)) { }

    public override void Run(Querier querier) {
        querier.Get<Attacked, Armor>().ForEach((ref Attacked attacked, ref Armor armor) => {
            float newArmor = Math.Max(0, armor.Value - attacked.Damage);
            float newDamage = Math.Max(0, attacked.Damage - armor.Value);
            attacked.Damage = newDamage;
            armor.Value = newArmor;
        });
    }
}

class HealthDamageSystem : Ecs.System {
    public HealthDamageSystem() : base(0, typeof(ArmorDamageSystem)) { }

    public override void Run(Querier querier) {
        querier.Get<Attacked, Health>().ForEach((Entity entity, ref Attacked attacked, ref Health health) => {
            float newHealth = Math.Max(0, health.Value - attacked.Damage);
            float newDamage = Math.Max(0, attacked.Damage - health.Value);
            attacked.Damage = newDamage;
            health.Value = newHealth;
            querier.Get<Attacked>().Remove(entity);
        });
    }
}

internal class Program {
    static void Main(string[] args) {
        var schedule = new SystemScheduleBuilder()
            .Add(new HealthDamageSystem())
            .Add(new BuffSystem())
            .Add(new ArmorDamageSystem())
            .Build();

        var ecs = new Ecs.Ecs(schedule);
        for (int i = 0; i < 3; i++) {
            ecs.EntityPool.Add();
        }
        foreach (var entity in ecs.EntityPool) {
            ecs.Components.Column<Health>().Add(entity, new(100.0f));
        }
    }
}
