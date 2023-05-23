namespace DataStructure.Tests;

[TestClass]
public class NamedCollectionTest
{

    [TestMethod]
    public void merge_string_collections_success()
    {
        var collection = new NamedCollection<string>("collection1",new []
        {
            "1", "2"
        });

        var newCollection = new[]
        {
            "1", "2", "3"
        };

        collection.Merge(newCollection);
            
        Assert.AreEqual(3,collection.Data.Count());
        Assert.IsFalse(collection.Data.Except(newCollection).Any());

    }

    [TestMethod]
    public void merge_strings_with_numbers_success()
    {
        var source = new[]
        {
            "1", "2"
        };
        var collection = new NamedCollection<string>("collection1",source);

        var newCollection = new[]
        {
            1, 2, 3
        };

        collection.Merge(newCollection);
            
        Assert.AreEqual(2,collection.Data.Count());
        Assert.IsFalse(collection.Data.Except(source).Any());

    }


    [TestMethod]
    public void cut_strings_success()
    {
        var source = new[]
        {
            "1", "2","3"
        };
        var collection = new NamedCollection<string>("collection1",source);

        var newCollection = new[]
        {
            "1",  "3"
        };

        collection.Cut(newCollection);
            
        Assert.AreEqual(1,collection.Data.Count());
        Assert.AreEqual(source[1], collection.Data.First());

    }
    [TestMethod]
    public void cut_collections_with_different_types_success()
    {
        var source = new[]
        {
            "1", "2","3"
        };
        var collection = new NamedCollection<string>("collection1",source);

        var newCollection = new[]
        {
            1,  3
        };

        collection.Cut(newCollection);
            
        Assert.AreEqual(source.Length,collection.Data.Count());
        Assert.IsFalse(collection.Data.Except(source).Any());

    }

    [TestMethod]
    public void merge_custom_objects_without_comparer_success()
    {
        var source = new[]
        {
            new CustomElement(1,"1"),
            new CustomElement(2,"2"),
        };
        var collection = new NamedCollection<CustomElement>("collection1",source);

        var passedList = new[]
        {
            new CustomElement(1,"1"),
            new CustomElement(2,"2"),
            new CustomElement(3,"3"),            };

        collection.Merge(passedList);
            
        Assert.AreEqual(source.Length + passedList.Length,collection.Data.Count());
    }

    [TestMethod]
    public void merge_custom_objects_using_comparer_success()
    {
        var source = new[]
        {
            new CustomElement(1,"1"),
            new CustomElement(2,"2"),
        };
        var collection = new NamedCollection<CustomElement>("collection1",source)
        {
            Comparer = new CustomComparer()
        };

        var passedList = new[]
        {
            new CustomElement(1,"1"),
            new CustomElement(2,"2"),
            new CustomElement(3,"3"),            };

        collection.Merge(passedList);
            
        Assert.AreEqual(3,collection.Data.Count());
        Assert.IsFalse(collection.Data.Except(passedList,collection.Comparer).Any());
    }

}