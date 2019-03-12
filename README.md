# crossfitDiary
Asp.net Core 2.0 (at current point) + Vuejs + Typescript + MSSql (EF Core + Dapper) project to track crossfit/workouts records

# Features list

*  Create AMRAP / For Time / For Time * N / EMOM / E2MOM / Not For Time workouts
*  Edit workout
*  Plan workout of the day
*  Quick log for workout of the day

# Technologies List

*  Vue: [Bootstrap Vue](https://bootstrap-vue.js.org)
*  Vue: VueJs using [Typescript](https://www.typescriptlang.org/)
*  Vue: [Vuex](https://vuex.vuejs.org/) for shared states between components
*  Vue: Component per workout type (for ex. Amrap is separate Workout Type)
*  [Font awesome](https://fontawesome.com/)
*  Typescript: [axios](https://github.com/axios/axios) for server api calls
*  Asp.net Core: WebApi + MVC
*  Asp.net Core: EF Core with Identity using Google auth, Vk Auth. Coming soon: Facebook Auth
*  Asp.net Core: [Dapper](https://github.com/StackExchange/Dapper) for some performance improvements
*  DB: MSSQL

# Screenshots

<img src="https://user-images.githubusercontent.com/3446779/54178070-839e6800-44a5-11e9-8486-a4059dbceb84.png" height="400"></img>
<img src="https://user-images.githubusercontent.com/3446779/54178069-839e6800-44a5-11e9-8432-7ab0f7938219.png" height="800"></img>
<img src="https://user-images.githubusercontent.com/3446779/54178068-839e6800-44a5-11e9-98b8-db69ab93cdd9.png" height="400"></img>
<img src="https://user-images.githubusercontent.com/3446779/54178071-839e6800-44a5-11e9-951d-6757df6a641b.png" height="800"></img>

# Installation:

1. Create customer local appsettings.json
2. Specify credentials for Sql Server and credentials for Google Authentication (you need to create ones using Google Dev Console)
3. "npm install" in terminal to install related UI packages: Vue, Bootstrap, Font Awesome, etc.

# Development


* "npm start" to start watching only current entry point page. Check package.json -> "scripts" -> "start" for more info 
* "npm run build-local" to build entire UI
