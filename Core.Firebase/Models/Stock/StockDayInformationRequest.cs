using Google.Cloud.Firestore;

namespace Core.Firebase.Model
{
    public class StockDayInformationRequest
    {
        public CollectionReference Reference { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
    }
}
