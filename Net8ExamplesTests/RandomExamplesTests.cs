using System.Text;
using Xunit.Abstractions;

namespace Net8ExamplesTests;

public class RandomExamplesTests {
    private readonly ITestOutputHelper _testOutputHelper;

    public RandomExamplesTests(ITestOutputHelper testOutputHelper) {
        _testOutputHelper = testOutputHelper;
    }

    public string[] GetOrdered() {
        return new[] { "Konijnenberg 24", "Konijnenberg 30", "Kleine krogt", "Crogtdijk", "Elders" };
    }
    
    [Fact]
    public void ShufflingShouldChangeOrder() {
        string[] buildings = GetOrdered();
        Random.Shared.Shuffle(buildings);
        buildings.Should().BeEquivalentTo(GetOrdered());
        buildings.Should().NotBeEquivalentTo(GetOrdered(), config => config.WithStrictOrdering());
    }
    
    [Fact]
    public void ShufflingShouldChangeOrderInDifferentWays() {
        string[] buildings1 = GetOrdered();
        string[] buildings2 = GetOrdered();
        
        Random.Shared.Shuffle(buildings1);
        Random.Shared.Shuffle(buildings2);

        buildings1.Should().BeEquivalentTo(buildings2);
        buildings1.Should().NotBeEquivalentTo(buildings2, config => config.WithStrictOrdering());
    }

    [Fact]
    public void GetItemsShouldOftenReturnDifferentThings() {
        string[] items1 = Random.Shared.GetItems(GetOrdered(), 2);
        string[] items2 = Random.Shared.GetItems(GetOrdered(), 2);

        items1.Length.Should().Be(2);
        items2.Length.Should().Be(2);
        
        items1.Should().NotBeEquivalentTo(items2, config => config.WithStrictOrdering());
    }

    [Fact]
    public void GetItemsWillSometimesReturnAnItemMoreThanOnce() {
        bool onlyUnique = false;
        string[] buildings = GetOrdered();

        for (int i = 0; i < 1000; i++) {
            string[] items = Random.Shared.GetItems(buildings, 2);
            if (items[0] == items[1]) {
                onlyUnique = true;
                break;
            }
        }

        onlyUnique.Should().BeTrue();
    }
    
    [Fact]
    public void GetItemsCanReturnMoreItemsThanInitial() {
        string[] items = { "😃", "'😆", "🙃", "🤩" };

        string[] randomEmojis = Random.Shared.GetItems(items, 100);
        randomEmojis.Length.Should().Be(100);


        StringBuilder stringBuilder = new StringBuilder(200);
        foreach (var randomEmoji in randomEmojis) {
            stringBuilder.Append(randomEmoji);
        }
        
        _testOutputHelper.WriteLine(stringBuilder.ToString());
    }
}
