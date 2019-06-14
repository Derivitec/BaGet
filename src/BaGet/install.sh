#!/bin/bash

#################################################################
# Code deploy install steps for the Baget server
#################################################################

mkdir -p /var/www/baget
unzip publish.zip
cp -R ./publish/* /var/www/baget

echo "[Unit]
Description=Derivitec Nuget Server

[Service]
WorkingDirectory=/var/www/baget
ExecStart=/usr/bin/dotnet /var/www/baget/BaGet.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=derivitec-nuget
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target" > /etc/systemd/system/baget.service

systemctl enable baget.service
systemctl start baget.service
systemctl status baget.service
