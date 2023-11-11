using Net8Examples.DefaultConstructors;

namespace Net8ExamplesTests;

public class DefaultConstructorsTests {
    [Fact]
    public void RecordsWithDefaultConstructorsGetProperties() {
        RecordWithDefaultConstructor record = new RecordWithDefaultConstructor("Student", 7);
        record.Grade.Should().Be(7);
        record.Name.Should().Be("Student");
    }

    [Fact]
    public void ClassWithDefaultConstructorGetsFields() {
        ClassWithDefaultConstructor classWithDefaultConstructor = new ClassWithDefaultConstructor("Student", 7);
        
    }

    [Fact]
    public void ShowOtherOverload() { }
    
    [Fact]
    public void ShowOverride() {}
    
    [Fact]
    public void StructWithDefaultConstructorGetsFields() {
        StructWithDefaultConstructor st = new StructWithDefaultConstructor("Student", 7);
        st.Grade.Should().Be(7);
        st.Name.Should().Be("Student");
    }
}
