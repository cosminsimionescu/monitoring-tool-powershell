Get-Process | Sort-Object WorkingSet64 -Descending | Select-Object -First 10 Name,
            @{Name='WorkingSet';Expression={'{0:n2}' -f($_.WorkingSet64/1MB)}}, Id | Format-List