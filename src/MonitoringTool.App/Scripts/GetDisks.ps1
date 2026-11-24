# Runs on the monitored server. Local fixed disks only (DriveType 3): v1 also returned empty
# CD/DVD drives with Size = 0, which caused a division by zero.
Get-CimInstance -ClassName Win32_LogicalDisk -Filter 'DriveType = 3' -ErrorAction Stop |
    Where-Object { $_.Size -gt 0 } |
    ForEach-Object {
        [pscustomobject]@{
            Drive  = $_.DeviceID
            Label  = [string]$_.VolumeName
            SizeGb = [math]::Round($_.Size / 1GB, 2)
            FreeGb = [math]::Round($_.FreeSpace / 1GB, 2)
        }
    }
