using UITests;
using Xunit;
using Xunit.Abstractions;

[assembly: TestCollectionOrderer(nameof(PriorityCollectionOrderer),"UITests.Shared")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace UITests
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionPriorityAttribute : Attribute
    {
        public int Priority { get; }
        public CollectionPriorityAttribute(int priority) => Priority = priority;
    }

    public class PriorityCollectionOrderer : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
        {
            return testCollections.OrderBy(collection =>
            {
                var priorityAttribute = collection.CollectionDefinition?
                    .GetCustomAttributes(typeof(CollectionPriorityAttribute).AssemblyQualifiedName)
                    .FirstOrDefault();

                return priorityAttribute?.GetNamedArgument<int>("Priority") ?? int.MaxValue;
            });
        }
    }
}
