# Image aus Repo verwenden
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Update der Paketliste
RUN apt-get update
# Verzeichnis erstellen, um Dateien zu kopieren
WORKDIR /app
# Kopiere die Dateien aus dem aktuellen Verzeichnis in das Arbeitsverzeichnis des Containers
COPY src/ .

# Umgebungsvariablen für die Konfiguration des Backends
ENV REST_API_URL=""
ENV MIN_WRONG_ANSWERS=""
ENV MAX_WRONG_ANSWERS=""

# Setze Umgebungsvariablen für das Backend
ARG REST_API_URL_ARG
ARG MIN_WRONG_ANSWERS_ARG
ARG MAX_WRONG_ANSWERS_ARG

ENV REST_API_URL=$REST_API_URL_ARG
ENV MIN_WRONG_ANSWERS=$MIN_WRONG_ANSWERS_ARG
ENV MAX_WRONG_ANSWERS=$MAX_WRONG_ANSWERS_ARG

RUN dotnet publish -o outputDir

# Einstiegspunkt definieren
ENTRYPOINT ["dotnet", "outputDir/CounTrivia.dll"]