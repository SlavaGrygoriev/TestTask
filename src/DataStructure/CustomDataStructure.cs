namespace DataStructure;
/// <summary>
/// Data structure consists of named collections
/// Each collection may have own element type
/// </summary>
public class CustomDataStructure 
{
    private readonly List<NamedCollection> _collection;

    public CustomDataStructure(params NamedCollection[] namedCollections)
    {
        if (HasDuplicateName(namedCollections))
        {
            throw new ArgumentException("Duplicate collection name");
        }
        _collection = new List<NamedCollection>(namedCollections);
    }

    /// <summary>
    /// Returns all collections
    /// </summary>
    public IReadOnlyCollection<NamedCollection> Data => _collection.AsReadOnly();


    /// <summary>
    /// Adding of structures.
    /// Elements duplication is forbidden, comparision condition - complete equality of all fields.
    /// Duplicates in target collection are acceptable, merging does not affect them.
    /// </summary>
    /// <param name="customDataStructure">added data structure</param>
    /// <exception cref="ArgumentNullException">data structure is mandatory</exception>
    public void Merge(CustomDataStructure customDataStructure)
    {
        if (customDataStructure == null) throw new ArgumentNullException(nameof(customDataStructure));
        foreach (var namedCollection in customDataStructure.Data)
        {
            Merge(namedCollection);
        }
    }

    /// <summary>
    /// Subtraction of structures.
    /// All elements of subtracted structure should be removed from target one.
    /// Target structure elements didn't match elements from subtracted remains unchanged.
    /// </summary>
    /// <param name="customDataStructure">subtracted data structure</param>
    /// <exception cref="ArgumentNullException">data structure is mandatory</exception>
    public void Cut(CustomDataStructure customDataStructure)
    {
        if (customDataStructure == null) throw new ArgumentNullException(nameof(customDataStructure));
        foreach (var namedCollection in customDataStructure.Data)
        {
            Cut(namedCollection);
        }
    }
    
    private bool HasDuplicateName(NamedCollection[] namedCollections)
    {
        return namedCollections.Select(r => r.Name).Distinct().Count() != namedCollections.Length;
    }

    private void Merge(NamedCollection namedCollection)
    {
        var targetCollection = _collection.FirstOrDefault(r => r.Name.Equals(namedCollection.Name));

        if (targetCollection == null)
        {
            _collection.Add(namedCollection);
        }
        else
        {
            targetCollection.Merge(namedCollection.GetData());
        }
    }

    private void Cut(NamedCollection namedCollection)
    {
        if (namedCollection == null) throw new ArgumentNullException(nameof(namedCollection));

        var data = namedCollection.GetData();
        var collectionName = namedCollection.Name;
        var targetCollection = _collection.FirstOrDefault(r => r.Name.Equals(collectionName));
        if (targetCollection != null)
        {
            targetCollection.Cut(data);
            if (!targetCollection.GetData().Any())
            {
                _collection.Remove(targetCollection);
            }
        }
    }
}