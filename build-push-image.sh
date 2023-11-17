#!/bin/bash

set -o allexport
source .env 
set +o allexport

docker build -t "$IMAGE" .

docker push "$IMAGE"

printf "\n"

echo "Built and pushed image trackyourspending:latest"
