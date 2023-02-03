using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using Underlying = System.UInt32;

namespace Alitz3.Ecs;
public readonly struct Entity : IEquatable<Entity> {
    // Version on the left, Id on the right
    public static readonly UnderlyingType MaxIdValue = NullId - 1;
    public static readonly UnderlyingType MaxVersionValue = (UnderlyingType.MaxValue - MaxIdValue) >> IdBitCount;
    public static readonly UnderlyingType NullId = 0xFFFF_F;
    private static readonly int IdBitCount = BitOperations.PopCount(MaxIdValue);
    private static readonly int VersionBitCount = BitOperations.PopCount(MaxVersionValue);
    private static readonly UnderlyingType IdMask = MaxIdValue;
    private static readonly UnderlyingType VersionMask = MaxVersionValue << IdBitCount;

    public Entity() {
        _value = CombineIdVersionBits(NullId, new(0));
    }

    public Entity(UnderlyingType id, UnderlyingType version) {
        if (id > MaxIdValue) {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        if (version > MaxVersionValue) {
            throw new ArgumentOutOfRangeException(nameof(version));
        }
        _value = CombineIdVersionBits(id, version);
    }

    private readonly UnderlyingType _value;

    public UnderlyingType Id =>
        ExtractIdBits(_value);

    public UnderlyingType Version =>
        ExtractVersionBits(_value);

    public UnderlyingType RawValue =>
        _value;

    public bool IsNull =>
        Id == NullId;

    public static Entity Null =>
        new();

    public static bool operator ==(Entity left, Entity right) =>
        left.Equals(right);

    public static bool operator !=(Entity left, Entity right) =>
        !left.Equals(right);

    public bool Equals(Entity other) =>
        Id == other.Id && (other.IsNull || Version == other.Version);

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is not null && obj is Entity entity && entity.Equals(this);

    public override int GetHashCode() =>
        _value.GetHashCode();

    private static UnderlyingType CombineIdVersionBits(UnderlyingType id, UnderlyingType version) =>
        (id & IdMask) | ((version << IdBitCount) & VersionMask); // Version mask has already been shifted

    private static UnderlyingType ExtractIdBits(UnderlyingType bits) =>
        (bits & (IdMask << VersionBitCount)) >> VersionBitCount;

    private static UnderlyingType ExtractVersionBits(UnderlyingType bits) =>
        bits & VersionMask;

    public readonly struct UnderlyingType : IEquatable<UnderlyingType> {
        public static readonly UnderlyingType MaxValue = Underlying.MaxValue;
        public static readonly UnderlyingType MinValue = Underlying.MinValue;

        public UnderlyingType(Underlying value) =>
            Value = value;

        public Underlying Value { get; }

        public static implicit operator UnderlyingType(Underlying value) =>
            new UnderlyingType(value);

        public static implicit operator Underlying(UnderlyingType value) =>
            value.Value;

        public static bool operator ==(UnderlyingType left, UnderlyingType right) =>
            left.Equals(right);

        public static bool operator !=(UnderlyingType left, UnderlyingType right) =>
            !left.Equals(right);

        public static UnderlyingType operator +(UnderlyingType left, UnderlyingType right) =>
            left.Value + right.Value;

        public static UnderlyingType operator -(UnderlyingType left, UnderlyingType right) =>
            left.Value - right.Value;

        public static UnderlyingType operator *(UnderlyingType left, UnderlyingType right) =>
            left.Value * right.Value;

        public static UnderlyingType operator /(UnderlyingType left, UnderlyingType right) =>
            left.Value / right.Value;

        public static UnderlyingType operator %(UnderlyingType left, UnderlyingType right) =>
            left.Value % right.Value;

        public static UnderlyingType operator +(UnderlyingType value) =>
            value.Value;

        public static UnderlyingType operator ++(UnderlyingType value) =>
            new(value.Value + 1);

        public static UnderlyingType operator --(UnderlyingType value) =>
            new(value.Value - 1);

        public static int ToInt32(UnderlyingType value) =>
            (int)value.Value;

        public static UnderlyingType FromInt32(int value) =>
            new UnderlyingType((Underlying)value);

        public bool Equals(UnderlyingType other) =>
            Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) =>
            obj is not null && obj is UnderlyingType value && value.Equals(this);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }
}
