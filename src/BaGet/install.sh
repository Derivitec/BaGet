#!/bin/bash

#################################################################
# Code deploy install steps for the Baget server
#################################################################

echo "[Unit]
Description=Derivitec Nuget Server

[Service]
WorkingDirectory=/var/www/baget
ExecStart=/usr/bin/dotnet BaGet.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=derivitec-nuget
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=EnvironmentName=$EnvironmentName
Environment=AwsRegion=$AwsRegion
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target" > /etc/systemd/system/baget.service

systemctl enable baget.service
systemctl start baget.service
systemctl status baget.service
