using Net8Examples.AliasTypes;

namespace Net8ExamplesTests; 

public class AliasTypeExampleTest {
    [Fact]
    public void AliasesOnlyWorkInTheScope() {
        var conversation = AliasTypeExample.CreateConversation();
        conversation.person1.Should().Be("Alice");
        conversation.person2.Should().Be("Bob");
    }
    
    
    //Show Global Using
}
