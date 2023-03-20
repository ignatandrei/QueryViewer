# Database2Code

You can download the extension from https://marketplace.visualstudio.com/items?itemName=ignatandrei.databasetocode

Modify the connectionDetails.txt and it will generate from you everything to WebAPI from SqlServer or Sqlite ( more providers soon). 
 
Also, it generates  different projects ( models, context, controllers, REACT CRA) – and you can also modify the templates. It works ( for the moment) just with tables with 0 or 1 PK.




# Steps to generate

1. Start a new project with DB2Code template
2. Modify the connection details from GeneratorPowershell
3. Run the project and it will generate the code for you
   1. If it shows an error , please copy the powershell command and run it from the terminal ( easy way: put into file a.bat)
4. The code will be generated - run the WebAPI project
   1. If you want to use the REACT CRA, run the npm install and npm start
5. Modify the templates if you want to change something 