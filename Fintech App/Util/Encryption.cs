using BCrypt.Net;

namespace Fintech_App.Util
{
    public class PinService
    {
        public string HashPin(string plainPin)
        {
            // WorkFactor 12 is a good balance between security and speed
            return BCrypt.Net.BCrypt.EnhancedHashPassword(plainPin, workFactor: 12);
        }


        public bool VerifyPin(string inputPin, string hashedPin)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(inputPin, hashedPin);
        }
    }
}
