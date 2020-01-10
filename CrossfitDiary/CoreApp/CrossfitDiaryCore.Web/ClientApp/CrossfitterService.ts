import axios, { AxiosResponse } from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
import {
  WorkoutViewModel,
  PlanningWorkoutLevel
} from "./models/viewModels/WorkoutViewModel";
import { PersonMaximumViewModel } from "./models/viewModels/PersonMaximumViewModel";
import { LeaderboardItemViewModel } from "./models/viewModels/LeaderboardItemViewModel";
import {WodSubType} from "./models/viewModels/WodSubType";
import {PlanningWorkoutViewModel} from "./models/viewModels/PlanningWorkoutViewModel";

export default class CrossfitterService {
  // tslint:disable-next-line:max-line-length
  public createAndLogWorkout = (model: {
    newWorkoutViewModel: WorkoutViewModel;
    logWorkoutViewModel: ToLogWorkoutViewModel;
  }) => {
    return axios.post("api/createAndLogNewWorkout", model);
  };

  // tslint:disable-next-line:max-line-length
  public createAndPlanWorkout = (planWorkoutModel: PlanningWorkoutViewModel) => {
    return axios.post("api/createAndPlanWorkout", planWorkoutModel);
  };

  // tslint:disable-next-line:max-line-length
  public getAllCrossfittersWorkouts = (
    page?: number,
    pageSize?: number
  ): Promise<ToLogWorkoutViewModel[]> => {
    return axios
      .get(`api/getAllCrossfittersWorkouts?page=${page}&pageSize=${pageSize}`)
      .then(jsonData => {
        return jsonData.data.map(x =>
          new ToLogWorkoutViewModel().deserialize(x)
        );
      });
  }; 
  public getPlannedWorkoutsForToday = (): Promise<{PlanningWorkoutLevel:[WorkoutViewModel]}> => {
    return axios.get(`api/getPlannedWorkoutsForToday`).then(jsonData => {
      let res: any  =  {};
      for (let i = 0; i < jsonData.data.length;i++) {
        res[jsonData.data[i].key] = jsonData.data[i].value.map(x => new PlanningWorkoutViewModel().deserialize(x));
      }
      return res;
    });
  };
  public getWorkoutsList = (): Promise<WorkoutViewModel[]> => {
    return axios.get(`api/getWorkoutsList`).then(jsonData => {
      return jsonData.data.map(x => new WorkoutViewModel().deserialize(x));
    });
  };
  public getExercises = (): Promise<ExerciseViewModel[]> => {
    return axios.get<ExerciseViewModel[]>("api/getExercises").then(jsonData => {
      return jsonData.data.map(x => new ExerciseViewModel().deserialize(x));
    });
  };

  public getExerciseMaximums = (): Promise<PersonMaximumViewModel[]> => {
    return axios
      .get<PersonMaximumViewModel[]>("api/getExerciseMaximums")
      .then(jsonData => {
        return jsonData.data.map(x =>
          new PersonMaximumViewModel().deserialize(x)
        );
      });
  };

  public getWeightsMaximums = (): Promise<PersonMaximumViewModel[]> => {
    return axios
      .get<PersonMaximumViewModel[]>("api/getWeightsMaximums")
      .then(jsonData => {
        return jsonData.data.map(x =>
          new PersonMaximumViewModel().deserialize(x)
        );
      });
  };
  public removeWorkout = crossfitterWorkoutId => {
    return axios.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
  };
  public  deletePlannedWorkout = (toRemovePlannedId: number) => {
    return axios.delete(`api/removePlannedWod/${toRemovePlannedId}`);
  };
  public quickLogWorkout = (logModel: ToLogWorkoutViewModel) => {
    return axios.post("api/quickLogWorkout", logModel);
  };

  public setShowOnlyUserWods = (showOnlyUserWods: boolean) => {
    return axios.post(
      `api/setShowOnlyUserWods?showOnlyUserWods=${showOnlyUserWods}`
    );
  };

  public planWorkoutToLevel = (wodId: number, type: PlanningWorkoutLevel) => {
    return axios.post(`api/planWorkoutToLevel?wodId=${wodId}&type=${type}`);
  };

  public getLeaderboardByWorkout = (
    crossfitterWorkoutId
  ): Promise<LeaderboardItemViewModel[]> => {
    // tslint:disable-next-line:max-line-length
    return axios
      .get<LeaderboardItemViewModel[]>(
        "api/getLeaderboardByWorkout?crossfitterWorkoutId=" +
          crossfitterWorkoutId
      )
      .then(jsonData => {
        return jsonData.data.map(x =>
          new LeaderboardItemViewModel().deserialize(x)
        );
      });
  };
}
