#!/bin/bash

#################################################################
# Code deploy uninstall steps for the Baget server
#################################################################

systemctl status baget.service
systemctl stop baget.service
systemctl disable baget.service

rm -f /etc/systemd/system/baget.service

rm -rf /var/www/baget
