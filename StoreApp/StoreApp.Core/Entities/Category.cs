namespace StoreApp.Core.Entities
{
    public class Category(string name) : BaseEntity
    {
        public string Name { get; private set; } = name;

        public void Update(string name)
        {
            Name = name;
        }
    }
}
