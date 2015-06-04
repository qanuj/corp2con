namespace e10.Shared.Data.Abstraction
{
    public interface IPerson
    {
        string Name { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
        string Location { get; set; }
        string About { get; set; }
    }
}