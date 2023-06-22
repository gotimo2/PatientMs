# Build all modules

FROM maven:3-eclipse-temurin-17-alpine AS build

COPY pom.xml /home

COPY ./treatment/src /home/treatment/src
COPY treatment/pom.xml /home/treatment

COPY ./communication/src /home/communication/src
COPY communication/pom.xml /home/communication

RUN mvn -f /home/pom.xml clean package -DskipTests -B

# Treatment microservice
FROM eclipse-temurin:17-jre-alpine as treatment
COPY --from=build /home/treatment/target/treatment-1.0.0.jar /usr/local/lib/treatment.jar
EXPOSE 8081
ENTRYPOINT ["java", "-jar", "/usr/local/lib/treatment.jar"]

# todo: probably improve this

FROM eclipse-temurin:17-jre-alpine as communication
COPY --from=build /home/communication/target/communication-1.0.0.jar /usr/local/lib/communication.jar
EXPOSE 8082
ENTRYPOINT ["java", "-jar", "/usr/local/lib/communication.jar"]
