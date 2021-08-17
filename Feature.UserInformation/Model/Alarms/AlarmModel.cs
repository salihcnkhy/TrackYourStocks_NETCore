
namespace Feature.UserInformation.Model
{
    public class AlarmModel
    {

        public string Id { get; set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public int ConditionType { get; set; }
        public double TriggerPrice { get; set; }
    }
}
