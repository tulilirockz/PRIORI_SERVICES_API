set dotenv-load
set ignore-comments

build-image:
    buildah build --tag priori-api-image

create-container:
    podman run  -d \
                -p 5000:5000 \
                --network bridge \
                --name 'priori-api-container' \
                priori-api-image 

start-container:
    podman start priori-api-container

stop-container:
    -podman stop priori-api-container

cleanup: stop-container
    -podman rm priori-api-container
    -podman rmi priori-api-image

setup-bootstrap:
    yarn install
    [ -d "static/res/bootstrap" ] && rm -r static/res/bootstrap
    mkdir -p static/res/bootstrap
    cp -r -f node_modules/bootstrap static/res