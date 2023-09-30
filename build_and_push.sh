#!/bin/bash

set -e

cd $CONTEXT_DIR

# Create the build_args file
env > /tmp/build_args

# Format the build arguments
BUILD_ARGS=$(cat /tmp/build_args | sed -z 's/\n/ --build-arg /g')

# Build and tag the Docker image
docker build -t $FULL_IMAGE_NAME -t $IMAGE_NAME_WITH_REGISTRY:latest -f $DOCKERFILE --build-arg $BUILD_ARGS --no-cache .

# Push the Docker images
docker push $IMAGE_NAME_WITH_REGISTRY:latest
docker push $FULL_IMAGE_NAME

# Remove the temporary build_args file
rm /tmp/build_args
