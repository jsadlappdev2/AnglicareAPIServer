using AnglicareAPIServer.Models;

namespace AnglicareAPIServer.Repository
{
    public interface IRegisterUser
    {
        void Add(RegisterUser registeruser);
        bool ValidateRegisteredUser(RegisterUser registeruser);
        bool ValidateUsername(RegisterUser registeruser);
        int GetLoggedUserID(RegisterUser registeruser);
    }
}
