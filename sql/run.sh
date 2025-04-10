#!/bin/bash

docker build -t mi_sql_server .
docker run -p 1433:1433  --name sqlpreview --hostname sqlpreview -d mi_sql_server