using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRpc.AdminManage.Dtos
{
    public class RpcMonitorMethodRequestDto
    {
        public string RequestTypeName { get; set; }

        public string RequestMethodName { get; set; }

        public int TotalExcuteCount { get; set; }

        public int ErrorCount { get; set; }
    }
}
