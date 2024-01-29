using System.Runtime.CompilerServices;

namespace Deepslate.Ecs.GenericWrapper;

#nullable enable

public partial struct Writable
{
    public partial struct ReadOnly
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;




            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData Data;

                public ComponentBundle Current => new();

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {


            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;




            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),

                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;


            }
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;


            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    ReadOnlyComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {

                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;


            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    ReadOnlyComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;

                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;


            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    ReadOnlyComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {

                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;


            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    ReadOnlyComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;

                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }
    }
}
public partial struct Writable<TWritable1>
    where TWritable1 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;

            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;

            }
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent3Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent3Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }
    }
}
public partial struct Writable<TWritable1, TWritable2>
    where TWritable1 : IComponent
    where TWritable2 : IComponent{
    public partial struct ReadOnly
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;

            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;

            }
        }
    }
    public partial struct ReadOnly<TReadOnly1>
        where TReadOnly1 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2, TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent3Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2, TReadOnly1> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent3Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
            }
        }
    }
    public partial struct ReadOnly<TReadOnly1, TReadOnly2>
        where TReadOnly1 : IComponent
        where TReadOnly2 : IComponent
    {
        public readonly struct Query(Deepslate.Ecs.Query query)
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2, TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent3Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent4Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }

        public readonly struct Query<TArchetypeCommand>(Deepslate.Ecs.Query query)
            where TArchetypeCommand : struct, IArchetypeCommand<TArchetypeCommand>
        {
            private readonly Deepslate.Ecs.Query _query = query;

            private readonly IComponentStorage<TWritable1>[] _writeable1Storages = query.GetStorages<TWritable1>();
            private readonly IComponentStorage<TWritable2>[] _writeable2Storages = query.GetStorages<TWritable2>();
            private readonly IComponentStorage<TReadOnly1>[] _readOnly1Storages = query.GetStorages<TReadOnly1>();
            private readonly IComponentStorage<TReadOnly2>[] _readOnly2Storages = query.GetStorages<TReadOnly2>();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator GetEnumerator() => new() { Data = new(_query.MatchedArchetypes) };

            public ref struct Enumerator
            {
                internal QueryEnumeratorData<TWritable1, TWritable2, TReadOnly1, TReadOnly2> Data;

                public ComponentBundle Current => new()
                {
                    ArchetypeCommand = TArchetypeCommand.Create(Data.CurrentArchetype),
                    WritableComponent1 = ref Data.CurrentComponent1Span[Data.CurrentIndex],
                    WritableComponent2 = ref Data.CurrentComponent2Span[Data.CurrentIndex],
                    ReadOnlyComponent1 = ref Data.CurrentComponent3Span[Data.CurrentIndex],
                    ReadOnlyComponent2 = ref Data.CurrentComponent4Span[Data.CurrentIndex]
                };

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Reset() => Data.Reset();
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext() => Data.MoveNext();
            }
            
            public ref struct ComponentBundle
            {
                public TArchetypeCommand ArchetypeCommand;
                public ref TWritable1 WritableComponent1;
                public ref TWritable2 WritableComponent2;
                public ref readonly TReadOnly1 ReadOnlyComponent1;
                public ref readonly TReadOnly2 ReadOnlyComponent2;
            }
        }
    }
}
