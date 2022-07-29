using Zenject;

public struct AddCoin { }
public struct LoseHeart { }

public class Stats
{
    public int Health { get; set; }
    public int Coints { get; set; }
}

public class EntityStats
{
    private readonly SignalBus signalBus;

    public Stats data;

    public EntityStats(EntityData playerData, SignalBus singalBus)
    {
        data = new Stats()
        {
            Health = playerData.health,
            Coints = playerData.coints
        };

        this.signalBus = singalBus;
    }

    public int Damaged(int id, int value)
    {
        data.Health -= value;

        for (int i = 0; i < value; i++)
        {
            signalBus.TryFireId<LoseHeart>(id);
        }

        return data.Health;

        //if (data.Health <= 0)
        //    signalBus.TryFireId<EntityDied>(id);


    }

    public void AddCoint()
    {
        data.Coints++;
    }
}
