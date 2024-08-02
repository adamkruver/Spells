using System;

using Utils.DataTypes;

namespace Utils.DataStructure
{
    public enum Attribute : int
    {
        Atk,
        Spellpower,
        Haste,
        Lethality,
        Versality,
        LIFESTEAL,
        Speed,
        AOERESIST,
        Endurance,
        OutcomeDamage,
        IncomeDamage,
        OutcomeHealing,
        IncomeHealing,
        BLOCK,
        EVADE,
        PARRY,
    }

    [Serializable]
    public class StatsTable
    {
        public const int STATS_COUNT = (int) Attribute.PARRY + 1;

        public static readonly StatsTable UnitDefault = new(new PercentModifiedValue[]
        {
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
            new PercentModifiedValue(0, 100),
        });

        private PercentModifiedValue[] _values = new PercentModifiedValue[STATS_COUNT];

        public StatsTable()
        {
            for (int i = 0; i < STATS_COUNT; i++)
            {
                _values[i] = new PercentModifiedValue();
            }
        }

        public StatsTable(PercentModifiedValue[] values)
        {
            for (int i = 0; i < STATS_COUNT; i++)
            {
                _values[i] = values[i];
            }
        }

        public StatsTable(StatsTable value) : this(value._values)
        {
        }

        public void Add(StatsTable table)
        {
            for (int i = 0; i < STATS_COUNT; i++)
            {
                _values[i] += table._values[i];
            }
        }

        public void Subtract(StatsTable table)
        {
            for (int i = 0; i < STATS_COUNT; i++)
            {
                _values[i] -= table._values[i];
            }
        }

        public void CopyTo(StatsTable target)
        {
            _values.AsSpan().CopyTo(target._values);
        }

        public void Clear()
        {
            _values.AsSpan().Clear();
        }

        public PercentModifiedValue this[int stat]
        {
            get => _values[stat];
            set => _values[stat] = value;
        }

        public PercentModifiedValue this[Attribute stat]
        {
            get => _values[(int) stat];
            set => _values[(int) stat] = value;
        }

        public static StatsTable operator +(StatsTable value1, StatsTable value2)
        {
            PercentModifiedValue[] values = new PercentModifiedValue[STATS_COUNT];

            for (int i = 0; i < STATS_COUNT; i++)
            {
                values[i] = value1[i] + value2[i];
            }

            return new StatsTable(values);
        }
    }
}
