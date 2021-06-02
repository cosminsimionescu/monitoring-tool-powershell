Get-CimInstance -Class CIM_LogicalDisk | Select-Object @{Name='Size(GB)';
            Expression={'{0:n2}' -f ($_.size/1gb)}},@{Name='Free Space(GB)';Expression={'{0:n2}' -f($_.freespace/1gb)}},
            @{Name='Free (%)';Expression={'{0,1:P2}' -f(($_.freespace/1gb) / ($_.size/1gb))}}, DeviceID | Format-List