FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory.
WORKDIR /app

# Copy the file from your host to your current location.
COPY --from=build-env /app/out .

# Inform Docker that the container is listening on the specified port at runtime.
EXPOSE 8080

# Run the specified command within the container.
ENTRYPOINT [ "dotnet", "SpotFinder-Api.dll" ]
