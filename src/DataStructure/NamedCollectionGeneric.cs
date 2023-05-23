namespace DataStructure;

/// <summary>
/// Collection is an array of elements of the same type.
/// </summary>
public class NamedCollection<T> : NamedCollection
{
    private readonly List<T> _list;
    private IEqualityComparer<T>? _comparer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="data">Init collection elements</param>
    public NamedCollection(string name, IEnumerable<T>? data = null) : base(name)
    {
        _list = new List<T>();
        if (data != null)
        {
            _list.AddRange(data);
        }
    }

    /// <summary>
    /// Returns collection data
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<object> GetData()
    {
        return Data.Cast<object>();
    }

    /// <summary>
    /// Adding of collection.
    /// </summary>
    /// <param name="data">added collection</param>
    /// <typeparam name="TItem">Collection element type</typeparam>
    /// <exception cref="ArgumentNullException">added collection is mandatory</exception>
    public override void Merge<TItem>(IEnumerable<TItem> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        Merge(data.OfType<T>());
    }

    /// <summary>
    /// Subtraction of collections.
    /// </summary>
    /// <param name="data">subtracted collection</param>
    /// <typeparam name="TItem">Collection element type</typeparam>
    /// <exception cref="ArgumentNullException">subtracted collection is mandatory</exception>
    public override void Cut<TItem>(IEnumerable<TItem> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        Cut(data.OfType<T>());
    }


    /// <summary>
    /// Adding of collection.
    /// Elements duplication is forbidden, comparision condition - complete equality of all fields.
    /// Duplicates in target collection are acceptable, merging does not affect them.
    /// </summary>
    /// <param name="data">added collection</param>
    /// <exception cref="ArgumentNullException">added collection is mandatory</exception>
    public void Merge(IEnumerable<T> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        foreach (T item in data)
        {
            if (!Contains(item))
            {
                _list.Add(item);
            }
        }
    }

    /// <summary>
    /// Subtraction of collections.
    /// All elements of subtracted collection are removed from target one.
    /// Target collection elements didn't match elements from subtracted remains unchanged.
    /// </summary>
    /// <param name="data">Subtracted collection</param>
    public void Cut(IEnumerable<T> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        foreach (T item in data)
        {
            IEnumerable<T> itemsToRemove = _list.Where(r => EqualTo(r, item)).ToList();
            foreach (T itemToRemove in itemsToRemove)
            {
                _list.Remove(itemToRemove);
            }

            if (_list.Count == 0)
            {
                break;
            }
        }
    }

    public IEnumerable<T> Data => _list;

    /// <summary>
    /// Support ability to change comparision algorithm flexible
    /// </summary>
    public IEqualityComparer<T> Comparer
    {
        get => _comparer ?? EqualityComparer<T>.Default;
        set => _comparer = value;
    }

    private bool Contains(T item)
    {
        return _list.Contains(item, Comparer);
    }

    private bool EqualTo(T a, T b)
    {
        try
        {
            return Comparer.Equals(a, b)
                   && a != null
                   && b != null
                   && Comparer.GetHashCode(a) == Comparer.GetHashCode(b);
        }
        catch
        {
            return false;
        }
    }
}