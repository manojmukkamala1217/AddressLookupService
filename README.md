# AddressLookupService

AddressLookupService is a Gateway API takes an IP or domain as an input, lookup for the multuple services like Ping, Rdap, GeoIp and Reverse DNS in parallel using the respective micro service APIs that are deployed in docker container and aggregates the response received from multiple sources returning a single result. 

# Prerequisites

- .NET 6
- Visual Studio 2022

# Building and running the App

# Step 1 Install the docker images of the micro service API projects

```Open Command Prompt as administrator 

cd <project root dir>

docker-compose up --build

``` 
![image](https://user-images.githubusercontent.com/109143438/183299624-ef60e401-54f7-472d-9186-094c3463ea3d.png)


# Step 2 Run AddressLookupService.Gateway.Api 

Since this gatway API is not included in docker compose, it should be run separatly either from command prompt and VS 2022

```Open Command Prompt as administrator 
cd <project root dir>
dotnet run --project AddressLookupService.Gateway.Api.csproj
```
Now, This Gateway API can be accessed using this URl - - http://localhost:5000/api/lookup/8.8.8.8

![image](https://user-images.githubusercontent.com/109143438/183299802-8f935a57-c9ef-40cc-8fec-b0563d3d6ebe.png)

### To test API using Swagger

  - Make `AddressLookupService.Gateway.Api` as a startup project and run it from VS 2022, Or,
 
  - Start the instance of the project using `Project -> Debug -> Start New Instance` option.

  - The swagger URL will be - https://localhost:5001/swagger/index.html
 
 ![image](https://user-images.githubusercontent.com/109143438/183299577-52efe778-1e2b-445a-bc99-2f0d59996e2a.png)

### Testing GET endpoint `/api/lookup/{address}` from swagger.
 
  **Parameter** - `address` => `8.8.8.8` (OR) `google.com`

### Testing GET endpoint `/api/lookup/{servicelist}/{address}` from swagger

  **Parameter** - `servicelist` => `rdap,rdns,ping,geoip`
	
  **Parameter** - `address` => `8.8.8.8` (OR) `google.com`
