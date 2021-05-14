using MotorPlantBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPlantBusinessLogic.HelperModels
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }

        public int PopPort { get; set; }

        public IMessageInfoStorage MessageStorage { get; set; }

        public IClientStorage ClientStorage { get; set; }
    }
}
