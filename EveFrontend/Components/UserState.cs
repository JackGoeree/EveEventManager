public class UserState
{
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email);

    public void SetUser(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void ClearUser()
    {
        Name = null;
        Email = null;
    }
}