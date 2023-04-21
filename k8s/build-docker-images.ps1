$path = $MyInvocation.MyCommand.Path | Split-Path

& docker build --tag dapr-asha/mosquitto:1.0 "$path/mosquitto"
& docker build --tag dapr-asha/ashaservice:1.0 "$path/../ashaservice"
& docker build --tag dapr-asha/anmservice:1.0 "$path/../anmservice"
& docker build --tag dapr-asha/simulation:1.0 "$path/../simulation"
