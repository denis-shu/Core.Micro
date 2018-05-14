namespace Micro.Services.Identity.Service
{
    public interface IPswrdEncr
    {
         string GetSalt(string val);
         string GetHash(string val, string saly);
    }
}