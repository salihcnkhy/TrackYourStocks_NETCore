using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Firebase.Enum
{
    public static class FirestoreCollectionExtension
    {
         public static string Value(this FirestoreCollection collection)
        {
            switch(collection)
            {
                case FirestoreCollection.Users: return "Users";
                case FirestoreCollection.Stokcs: return "Stocks";
                case FirestoreCollection.Constants: return "Constants";
                default: return String.Empty;
            }
        }
    }
    public enum FirestoreCollection
    {
        Users,
        Stokcs,
        Constants
    }
}
