# ExtRS for SSRS 2022
This project provides a simple way to use and extend SSRS Custom Assemblies

# Synopsis
# Custom Behaviors via custom .NET Standard assemblies for SSRS 2022

# Usage 

Example usage:
```vbscript
    =ExtRS.Finance.GetStockPrice(Fields!TickerSymbol.Value)
    =ExtRS.Finance.GetEurosFromDollars(Parameters!Dollars.Value)
    =ExtRS.Weather.GetWeatherShortDesc(Fields!PostalCode.Value)
    =ExtRS.Weather.GetForecastTemp(Parameters!FutureDateTime.Value)
    =ExtRS.Sports.GetLogo(Parameters!TeamName.Value)
    =ExtRS.Sales.FormatSalesStatus(Parameters!TtlProjRev.Value, Parameters!DaysToClose.Value)
```

# Implementation 

## Step 1: Compile this project

## Step 2: Confirm that ExtRS.dll was xcopy''d to ReportServer/bin

You should see the .dll copied along with its .pdb file (not recommended for production, but required for debugging)

## Step 3: Referencing the assemblies custom functions


## Step 4: Report display result


# Debugging

Visual Studio 2022 provides an excellent debugging experience. You need only to attach the debugger to the ReportingService process and VS will hit any of the associated breakpoints.
If you want to debug reports that you are developing in the VS IDE (VS17, VS19, (eventually VS22), etc.) you can attach to ReportServerService to debug any custom functions that run for a report execution.

If you would like to debug ExtRS remotely, you just need to install remote debugging on the remote server and ensure that Port 3314 is open to the client/IP you are debugging from.


Additional usage reference: https://docs.microsoft.com/en-us/sql/reporting-services/custom-assemblies/accessing-custom-assemblies-through-expressions?view=sql-server-ver15#calling-instance-members-from-a-report-definition-file