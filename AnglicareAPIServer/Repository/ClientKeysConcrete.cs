using System.Linq;
using AnglicareAPIServer.Models;
using AnglicareAPIServer.Context;
using AnglicareAPIServer.AES256Encryption;
using System.Data.Entity;

namespace AnglicareAPIServer.Repository
{
    public class ClientKeysConcrete : IClientKeys
    {
        DatabaseContext _context;
        public ClientKeysConcrete()
        {
            _context = new DatabaseContext();
        }

        public void GenerateUniqueKey(out string ClientID, out string ClientSecert)
        {
            ClientID = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            ClientSecert = EncryptionLibrary.KeyGenerator.GetUniqueKey();
        }

        public bool IsUniqueKeyAlreadyGenerate(int UserID)
        {
            bool keyExists = _context.ClientKeys.Any(clientkeys => clientkeys.UserID.Equals(UserID));

            if (keyExists)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int SaveClientIDandClientSecert(ClientKey ClientKeys)
        {
            _context.ClientKeys.Add(ClientKeys);
            return _context.SaveChanges();
        }

        public ClientKey GetGenerateUniqueKeyByUserID(int UserID)
        {
            var clientkey = (from ckey in _context.ClientKeys
                            where ckey.UserID  == UserID
                            select ckey).FirstOrDefault();
            return clientkey;
        }


        public int UpdateClientIDandClientSecert(ClientKey ClientKeys)
        {
            _context.Entry(ClientKeys).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.SaveChanges();
        }

    }
}