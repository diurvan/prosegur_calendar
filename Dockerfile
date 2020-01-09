FROM microsoft/dotnet-nightly:2.1-sdk-alpine3.7

#Change Timezone
RUN apk add tzdata
RUN cp /usr/share/zoneinfo/America/Lima /etc/localtime
RUN echo "America Latina" >		/etc/timezone
RUN apk del tzdata

RUN mkdir -p /app

WORKDIR /app
EXPOSE 5000/tcp