namespace Net8Examples.AliasTypes; 

using Conversation = (string person1, string person2);

public class AliasTypeExample {
    public static Conversation CreateConversation() {
        return new("Alice", "Bob");
    }
}
