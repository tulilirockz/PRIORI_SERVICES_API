set dotenv-load
set ignore-comments

export IMAGE_NAME := "priori-api-image"
export CONTAINER_NAME := "priori-api-container"

build:
    "$CONTAINER_BUILDER" build --tag "$IMAGE_NAME"

create:
    "$CONTAINER_RUNNER" run -d -p 5000:5000 \
                --network bridge \
                --name "$CONTAINER_NAME" \
                "$IMAGE_NAME"

[private]
runner_action +options:
    "$CONTAINER_RUNNER" {{options}}

start: (runner_action 'start' "$CONTAINER_NAME")

stop: (runner_action 'stop' "$CONTAINER_NAME")

cleanup: stop (runner_action 'rm' "$CONTAINER_NAME") (runner_action 'rmi' "$IMAGE_NAME")