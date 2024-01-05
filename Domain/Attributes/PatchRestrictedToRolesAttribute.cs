namespace Core.Attributes;

public class PatchRestrictedToRolesAttribute : Attribute
{
    private string[] UserRoles { get; set; }

    public PatchRestrictedToRolesAttribute(string[] userRoles)
    {
        this.UserRoles = userRoles;
    }

    public string[] GetUserRoles()
    {
        return UserRoles;
    }
}