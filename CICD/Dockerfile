FROM jetbrains/teamcity-agent:2018.2.4-windowsservercore

SHELL ["powershell"]

RUN Install-WindowsFeature NET-Framework-45-ASPNET
RUN Install-WindowsFeature Web-Asp-Net45

