# PruebaLD.DiegoM

El siguiente proyecto está realizado el framework .net core y React js

Tambien esta realizado bajo un patron de aaquitectura en capas donde se definen 6 capas principales 
estas son:

Capa API,
Capa logica de negocio,
Capa Acceso a datos,
Capa Utilidades,
Capa Entidades,
Capa Web

La base de datos esta realizada en SQL 

El Script de la base de datos se encuentra en una carpeta dentro del proyecto


Pasos para probar la solucion:

1. Ejecute el script de la base de datos en su sql local
2. Cambie la conexion hacia la base de datos
La conexion a la base de datos se encuentra en el proyecto: PruebaLD.DiegoM.API -> appsettings.json

![image](https://user-images.githubusercontent.com/31227628/134843323-ab497b21-772a-42cd-8608-50174f8526b0.png)

3. Son 2 proyectos los que se tienen que iniciar estos son:
   PruebaLD.DiegoM.API,
   PruebaLD.DiegoM.Web
   
   Esto se realiza en la configuracion de la solución , adjunto ejemplo 
   ![establecer proyectos de inicio](https://user-images.githubusercontent.com/31227628/134843586-eb18c53a-baa7-4cbe-847e-6af287892375.gif)







