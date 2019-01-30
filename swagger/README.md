# Swagger/OpenAPI
https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2  
https://github.com/domaindrivendev/Swashbuckle.AspNetCore  

Swagger/OpenAPI generer dokumentation og hjælpesider.  
Swashbuckle.AspNetCore er et open source projekt, der generer Swagger dokumentation til ASP.NET core Web APIer.  

### Install Swachbuckle for vs code
Go to the root of your project.  
dotnet add TodoApi.csproj package Swashbuckle.AspNetCore  

### Hvad er Swagger/OpenAPI
Bruges til at beskrve et REST API. Et af målene er at nedbringe den tid man bruger på dokumentation.  

### Swagger specification (swagger.json)
svagger.json er kernen i et Swagger flow. Den bliver generet af Swagger tool chain baseret på din REST API.  

### Swagger UI
Er en web-baseret UI, der giver information om REST APIet. Det er muligt at bruge en middleware i din hosted ASP.NET core, der er en embedded version af Swagger UI. Det er muligt at teste endpointene fra den webbaseret UI.  
