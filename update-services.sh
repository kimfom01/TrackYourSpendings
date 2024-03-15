#!/bin/bash

GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m'

if [ $# -eq 0 ]; then
	echo -e "${RED}No services provided!${NC}"
	exit 1
fi

printf "You are updating the following services: ${GREEN}$@${NC}\n\n"

git pull origin main

docker compose pull && docker compose up -d $@
