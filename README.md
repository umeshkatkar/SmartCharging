# SmartCharging
This is .net core web api services which provides smart solution for electric vehicle charging facility. It has different entities like Charging Group, Charging Station under group and Charging Connectors under station.
Instruction:
-To create database , change connection string in appSetting.json file
- In Package-Management-Console type "Add-Migration InitialCreate" to create migration for database using entity framework
- In Package-Management-Console type "update-database"
- Once database created , create entities like, Group, station and connectors from PostMan
- xUnit testing framework used
- .net core 3.1 and Entity framework aslo 3.1 used
- 
