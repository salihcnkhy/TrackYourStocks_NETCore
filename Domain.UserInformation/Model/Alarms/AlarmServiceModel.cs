
namespace Domain.UserInformation.Model
{
    public class AlarmServiceModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public int ConditionType { get; set; }
        public double TriggerPrice { get; set; }
    }
}
