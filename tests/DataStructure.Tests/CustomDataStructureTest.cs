namespace DataStructure.Tests;

 [TestClass]
    public class CustomDataStructureTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Duplicate collection name")]
        public void init_structure_with_duplicate_collection_keys_failed()
        {

            var source1 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
            };
            var collection1 = new NamedCollection<CustomElement>("collection1",source1)
            {
                Comparer = new CustomComparer()
            };

           

            var source2 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
                new CustomElement(3,"3"),            };
            var collection2 = new NamedCollection<CustomElement>("collection1",source2)
            {
                Comparer = new CustomComparer()
            };

            var structure = new CustomDataStructure(collection1,collection2);

        }

        [TestMethod]
        public void merge_structures_success()
        {
            var comparer = new CustomComparer();
            var source1 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
            };
            var collection1 = new NamedCollection<CustomElement>("collection1",source1)
            {
                Comparer = comparer
            };

            var source2 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
                new CustomElement(3,"3"),
            };

            var collection2 = new NamedCollection<CustomElement>("collection2",source2)
            {
                Comparer = comparer
            };

            var collection3 = new NamedCollection<CustomElement>("collection1",source2)
            {
                Comparer = comparer
            };

            var collection4 = new NamedCollection<CustomElement>("collection2",source1)
            {
                Comparer = comparer
            };

            var structure1 = new CustomDataStructure(collection1,collection2);
            var structure2 = new CustomDataStructure(collection3,collection4);

            structure1.Merge(structure2);

            Assert.AreEqual(2,structure1.Data.Count());

            Assert.AreEqual(3,structure1.Data.First(r=>r.Name == collection1.Name).GetData().Count());
            Assert.AreEqual(3,structure1.Data.First(r=>r.Name == collection2.Name).GetData().Count());
            Assert.IsFalse(source2.Except(structure1.Data.First(r=>r.Name == collection1.Name).GetData().Cast<CustomElement>(),comparer).Any());
            Assert.IsFalse(source2.Except(structure1.Data.First(r=>r.Name == collection2.Name).GetData().Cast<CustomElement>(),comparer).Any());

        }


        [TestMethod]
        public void cut_structures_success()
        {
            var comparer = new CustomComparer();
            var source1 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
            };
            var collection1 = new NamedCollection<CustomElement>("collection1",source1)
            {
                Comparer = new CustomComparer()
            };

            var source2 = new[]
            {
                new CustomElement(1,"1"),
                new CustomElement(2,"2"),
                new CustomElement(3,"3"),
            };

            var collection2 = new NamedCollection<CustomElement>("collection2",source2)
            {
                Comparer = comparer
            };

            var collection3 = new NamedCollection<CustomElement>("collection1",source2)
            {
                Comparer = comparer
            };

            var collection4 = new NamedCollection<CustomElement>("collection2",source1)
            {
                Comparer = comparer
            };

            var structure1 = new CustomDataStructure(collection1,collection2);
            var structure2 = new CustomDataStructure(collection3,collection4);

            structure1.Cut(structure2);

            Assert.AreEqual(1,structure1.Data.Count());

            Assert.AreEqual(1,structure1.Data.First(r=>r.Name == collection2.Name).GetData().Count());
            Assert.AreEqual(3,structure1.Data.First(r=>r.Name == collection2.Name).GetData().Cast<CustomElement>().First().Id);

        }

    }