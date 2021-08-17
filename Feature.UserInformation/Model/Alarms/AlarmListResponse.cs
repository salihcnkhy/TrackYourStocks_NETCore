
using Core.Base;
using System.Collections.Generic;

namespace Feature.UserInformation.Model
{
    public class AlarmListResponse : Response
    {
        public List<AlarmModel> AlarmList { get; set; }
    }
}
