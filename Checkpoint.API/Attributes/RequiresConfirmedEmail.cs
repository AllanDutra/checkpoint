namespace Checkpoint.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RequiresConfirmedEmail : Attribute { }
}
