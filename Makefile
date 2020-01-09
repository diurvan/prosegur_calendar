#!/bin/sh
.DEFAULT_GOAL := help

.PHONY:	build \
		release\
##
## Development Commands
##
build:	##Constuir imagen para DEV:	make build
	docker build \
		-f docker/dev/Dockerfile \
		-t $(IMAGE_DEV)
		docker/dev/ \
		--no-cache

release: ##Constuir imagen para PRD:	make build
	$(call delete_directory_release)
	$(call detect_user)
	docker run \
		--workdir "/${APP_DIR}" \
		--rm \
		-it \
		-u ${USERID}:${USERID} \
		-v ${PWD}/passwd:/etc/passwd:ro \
		-v "${PWD}/${APP_DIR}":${APP_DIR} \
		--tty=false \
		${IMAGE_DEV} \
		dotnet publish -c Release -o /${APP_DIR}/release
	$(call get_files_config)
	@rm -rf ${PWD}/passwd