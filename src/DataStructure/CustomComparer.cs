namespace DataStructure;
/// <summary>
/// Custom comparision algorithm
/// </summary>
public class CustomComparer : IEqualityComparer<CustomElement>
{
    public bool Equals(CustomElement? x, CustomElement? y)
    {
        return x?.Id == y?.Id && x?.Name == y?.Name;
    }

    public int GetHashCode(CustomElement obj)
    {
        return obj.Id.GetHashCode();
    }
}