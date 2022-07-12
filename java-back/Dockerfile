FROM gradle:7.4.1-jdk17-alpine as build

COPY --chown=gradle:gradle . /home/gradle/src
WORKDIR /home/gradle/src

RUN gradle build --no-daemon -x test
RUN mkdir -p build/libs/dependency && (cd build/libs/dependency; jar -xf ../*.jar)

FROM openjdk:17-jre-slim

EXPOSE 8080
VOLUME /tmp

RUN wget https://github.com/open-telemetry/opentelemetry-java-instrumentation/releases/latest/download/opentelemetry-javaagent.jar

ARG DEPENDENCY=/home/gradle/src/build/libs/build/libs/dependency
COPY --from=build ${DEPENDENCY}/BOOT-INF/lib /app/lib
COPY --from=build ${DEPENDENCY}/META-INF /app/META-INF
COPY --from=build ${DEPENDENCY}/BOOT-INF/classes /app
ENTRYPOINT ["java","-javaagent:opentelemetry-javaagent.jar","-cp","app:app/lib/*","com.example.demo.DemoApplication"]