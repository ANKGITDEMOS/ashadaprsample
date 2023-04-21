# ashadaprsample


Install Docker locally https://docs.docker.com/engine/install/

Install Dapr from https://docs.dapr.io/getting-started/install-dapr-cli/


Go to Infrastructure folder

Run powershell command .\start-all.ps1

\AshaService

cd C:\tryouts\AshaAppSolution\ANMService

To run  Ashaservice Folder

Goto \AshaService

Run dapr.uninstall
Run dapr.init

dapr run --app-id ashaservice --app-port 6000 --dapr-http-port 3600 --dapr-grpc-port 60000 --config ../dapr/config/config.yaml --components-path ../dapr/components dotnet run

To run  ANMService

Goto \ANMService Folder

dapr run --app-id anmservice --app-port 6001 --dapr-http-port 3601 --dapr-grpc-port 60001 --config ../dapr/config/config.yaml --components-path ../dapr/components dotnet run

Goto \Simulation Folder

dotnet run
