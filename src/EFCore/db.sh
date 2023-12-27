docker run -itd -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd" \
   -p 1433:1433 --name mssql --hostname mssql \
   mcr.microsoft.com/azure-sql-edge:latest
