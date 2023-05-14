# System Architecture
![alt text](https://drive.google.com/uc?export=view&id=1mXtN0PDfnVsvs5RfMobLrPI_52Vpxbue)

# System Understanind
<p>When we look at the architecture we see that this application as a whole uses two application where the main system of frontend is in react and backend as .net web api</p>
<p>This whole is deployed as a kuberbetes cluster with configuration set up in their respective yaml files of both deployment and service</p>
<p>The database used is mogo db with integrated in a .net api that also have authentication with jwt token and cache in redis</p>

# Commands to run the application
 
### In the Reat App can run:
</p>
<br/>
1)`npm start`<br/>
2`npm test` [To run the unit test]<br/>
3)`npm run build`[Build the application]<br/>
4)`npm run eject`
</p>

### Run the .net Api
<p>
</br>
1) dotnet build<br/>
2) dotnet run</p>

### run docker files 
</br>
<p>
1) docker image <imag name></p>
