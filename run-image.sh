#!/bin/bash

set -o allexport
source .env 
set +o allexport

docker rm "$CONTAINER"

docker run -p "$CONTAINER_PORT_1" -p "$CONTAINER_PORT_2" \
  --name "$CONTAINER" \
  --env CLIENT_SECRET="$CLIENT_SECRET" \
  --env CLIENT_ID="$CLIENT_ID" \
  --env REDIRECT_URI="$REDIRECT_URI" \
  --env DB_URI="$DB_URI"\
  --env ASPNETCORE_URLS="$ASPNETCORE_URLS" \
  --env ASPNETCORE_HTTPS_PORT="$ASPNETCORE_HTTPS_PORT" \
  "$IMAGE"