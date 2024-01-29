using Deepslate.Ecs;

namespace Deepslate.Ecs;

public ref struct ComponentStorageEnumerator<TComponent>
    where TComponent : IComponent
{
    private readonly IComponentStorage<TComponent>[] _storages;
    private Span<TComponent> _currentComponentSpan = Span<TComponent>.Empty;
    private int _currentStorageIndex = -1;
    private int _currentIndex = -1;

    internal ComponentStorageEnumerator(IComponentStorage<TComponent>[] storages)
    {
        _storages = storages;
    }

    public bool MoveNext()
    {
        _currentIndex++;
        while (_currentStorageIndex < _storages.Length && _currentIndex >= _currentComponentSpan.Length)
        {
            _currentStorageIndex++;
            _currentIndex = 0;
            _currentComponentSpan = _storages[_currentStorageIndex].AsSpan();
        }

        return _currentStorageIndex < _storages.Length;
    }

    public void Reset()
    {
        _currentStorageIndex = -1;
        _currentIndex = -1;
        _currentComponentSpan = Span<TComponent>.Empty;
    }

    public ref TComponent Current => ref _currentComponentSpan[_currentIndex];
}
