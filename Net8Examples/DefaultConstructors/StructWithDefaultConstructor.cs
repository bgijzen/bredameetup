namespace Net8Examples.DefaultConstructors;

public struct StructWithDefaultConstructor(string name, int grade) {
    public string Name => name;
    public int Grade => grade;
}
