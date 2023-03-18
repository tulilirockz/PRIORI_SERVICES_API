create-db:
	podman run --name "sqlserver" \
				-e "ACCEPT_EULA=Y" \
				-e "MSSQL_SA_PASSWORD=@ScoobyDooby23" \
				-p 1433:1433 \
				-d 'mcr.microsoft.com/mssql/server:2022-latest'

# Server=192.168.15.14,1433;DataBase=Priori;user id=sa;password=@ScoobyDooby23
stop-db:
	podman stop "sqlserver"

start-db:
	podman start "sqlserver"

delete-db:
	podman rm sqlserver