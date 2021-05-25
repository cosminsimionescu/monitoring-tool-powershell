namespace monitoring_tool
{
    public class Scripts
    {
        public string memory_Script()//memory usage
        {
            string memS = @"";
            return memS;
        }

        public string cpu_Script()//cpu usage
        {
            string cpuS = @"";

            return cpuS;
        }

        public string processByCPU_Script()//processes sorted by CPU usage
        {
            string volS = @""; //top 10 processes by CPU usage
            return volS;
        }
        public string processByMem_Script()//processes sorted by Memory usage
        {
            string volS = @"Get-Process | Sort-Object WorkingSet64 -Descending | Select-Object -first 10 Name,
            @{Name='WorkingSet';Expression={'{0:n2}' -f($_.WorkingSet64/1MB)}}, Id | Format-List"; //top 10 processes by Memory usage
            return volS;
        }
        public string volume_Script()//volume usage
        {
            string volS = @"Get-CimInstance -Class CIM_LogicalDisk | Select-Object @{Name='Size(GB)';
            Expression={'{0:n2}' -f ($_.size/1gb)}},@{Name='Free Space(GB)';Expression={'{0:n2}' -f($_.freespace/1gb)}},
            @{Name='Free (%)';Expression={'{0,1:P2}' -f(($_.freespace/1gb) / ($_.size/1gb))}}, DeviceID | Format-List";

            return volS;
        }
    
    }
}
