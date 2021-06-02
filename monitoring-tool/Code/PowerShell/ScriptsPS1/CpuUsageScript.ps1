$CPU = (Get-Counter '\Processor(_total)\% Processor Time' -Sample 30).CounterSamples[0].CookedValue
            $RoundCPU = [math]::Round($CPU, 2)
            $RoundCPU