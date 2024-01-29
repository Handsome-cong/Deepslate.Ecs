
namespace Deepslate.Ecs.GenericWrapper;

#nullable enable

public partial struct Writable
{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable1>()
                where TWritable1 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable1)));
        }
    }
}
public partial struct Writable<TWritable1>
    where TWritable1 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable2>()
                where TWritable2 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable2)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2>
    where TWritable1 : IComponent
    where TWritable2 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable3>()
                where TWritable3 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable3)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable4>()
                where TWritable4 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable4)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3, TWritable4>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent
    where TWritable4 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable5>()
                where TWritable5 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable5)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent
    where TWritable4 : IComponent
    where TWritable5 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable6>()
                where TWritable6 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable6)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent
    where TWritable4 : IComponent
    where TWritable5 : IComponent
    where TWritable6 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable7>()
                where TWritable7 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable7)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent
    where TWritable4 : IComponent
    where TWritable5 : IComponent
    where TWritable6 : IComponent
    where TWritable7 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>.ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireWritable<TWritable8>()
                where TWritable8 : IComponent =>
                new(Builder.RequireWritable(typeof(TWritable8)));
        }
    }
}
public partial struct Writable<TWritable1, TWritable2, TWritable3, TWritable4, TWritable5, TWritable6, TWritable7, TWritable8>
    where TWritable1 : IComponent
    where TWritable2 : IComponent
    where TWritable3 : IComponent
    where TWritable4 : IComponent
    where TWritable5 : IComponent
    where TWritable6 : IComponent
    where TWritable7 : IComponent
    where TWritable8 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1>.QueryBuilder RequireReadOnly<TReadOnly1>()
                where TReadOnly1 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly1)));
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2>.QueryBuilder RequireReadOnly<TReadOnly2>()
                where TReadOnly2 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly2)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>.QueryBuilder RequireReadOnly<TReadOnly3>()
                where TReadOnly3 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly3)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>.QueryBuilder RequireReadOnly<TReadOnly4>()
                where TReadOnly4 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly4)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>.QueryBuilder RequireReadOnly<TReadOnly5>()
                where TReadOnly5 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly5)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>.QueryBuilder RequireReadOnly<TReadOnly6>()
                where TReadOnly6 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly6)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>.QueryBuilder RequireReadOnly<TReadOnly7>()
                where TReadOnly7 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly7)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
            public ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>.QueryBuilder RequireReadOnly<TReadOnly8>()
                where TReadOnly8 : IComponent =>
                new(Builder.RequireReadOnly(typeof(TReadOnly8)));
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2, TReadOnly3, TReadOnly4, TReadOnly5, TReadOnly6, TReadOnly7, TReadOnly8>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
        where TReadOnly3 : IComponent
        where TReadOnly4 : IComponent
        where TReadOnly5 : IComponent
        where TReadOnly6 : IComponent
        where TReadOnly7 : IComponent
        where TReadOnly8 : IComponent    {
        public readonly struct QueryBuilder(Deepslate.Ecs.QueryBuilder builder)
        {
            public Deepslate.Ecs.QueryBuilder Builder { get; } = builder;

            public QueryBuilder With<TComponent>()
                where TComponent : IComponent =>
                new(Builder.With(typeof(TComponent)));

            public QueryBuilder Without<TComponent>()
                where TComponent : IComponent =>
                new(Builder.Without(typeof(TComponent)));

            public QueryBuilder Ignore(Archetype archetype) =>
                new(Builder.Ignore(archetype));
        }
    }
}
