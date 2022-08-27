namespace Application.Common.Interfaces
{
    /// <summary>
    /// Service to manage Current User.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// User ID.
        /// </summary>
        string UserId { get; }
    }
}