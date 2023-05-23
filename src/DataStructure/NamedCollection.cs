namespace DataStructure;
/// <summary>
/// Collection is an array of elements of the same type.
/// Collection elements can be of any data type allowed
/// </summary>
public abstract class NamedCollection 
{
    public string Name { get; }
    protected NamedCollection(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public abstract IEnumerable<object> GetData();
    public abstract void Merge<T>(IEnumerable<T> data);
    public abstract void Cut<T>(IEnumerable<T> data);

}