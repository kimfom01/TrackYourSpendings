#!/bin/bash

set -o allexport
source .env 
set +o allexport

docker build -t "$IMAGE" .

docker push "$IMAGE"

printf "\n"

echo "Build and pushed image "$IMAGE":latest"
