import axios, { AxiosResponse } from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";
import { PersonMaximumViewModel } from "./models/viewModels/PersonMaximumViewModel";
import { LeaderboardItemViewModel } from "./models/viewModels/LeaderboardItemViewModel";

export default class CrossfitterService {
  // tslint:disable-next-line:max-line-length
  public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel; logWorkoutViewModel: ToLogWorkoutViewModel }) => {
    return axios.post("api/createAndLogNewWorkout", model);
  };

  // tslint:disable-next-line:max-line-length
  public createAndPlanWorkout = (workoutViewModel: WorkoutViewModel) => {
    return axios.post("api/createAndPlanWorkout", workoutViewModel);
  };

  // tslint:disable-next-line:max-line-length
  public getAllCrossfittersWorkouts = (page?: number, pageSize?: number): Promise<ToLogWorkoutViewModel[]> => {
    return axios.get(`api/getAllCrossfittersWorkouts?page=${page}&pageSize=${pageSize}`).then(jsonData => {
      return jsonData.data.map(x => new ToLogWorkoutViewModel().deserialize(x));
    });
  };
  public getPlannedWorkoutsForToday = (): Promise<WorkoutViewModel[]> => {
    return axios.get(`api/getPlannedWorkoutsForToday`).then(jsonData => {
      return jsonData.data.map(x => new WorkoutViewModel().deserialize(x));
    });
  };

  public getExercises = (): Promise<ExerciseViewModel[]> => {
    return axios.get<ExerciseViewModel[]>("api/getExercises").then(jsonData => {
      return jsonData.data.map(x => new ExerciseViewModel().deserialize(x));
    });
  };

  public getExerciseMaximums = (): Promise<PersonMaximumViewModel[]> => {
    return axios.get<PersonMaximumViewModel[]>("api/getExerciseMaximums").then(jsonData => {
      return jsonData.data.map(x => new PersonMaximumViewModel().deserialize(x));
    });
  };

  public getWeightsMaximums = (): Promise<PersonMaximumViewModel[]> => {
    return axios.get<PersonMaximumViewModel[]>("api/getWeightsMaximums").then(jsonData => {
      return jsonData.data.map(x => new PersonMaximumViewModel().deserialize(x));
    });
  };
  public removeWorkout = crossfitterWorkoutId => {
    return axios.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
  };

  public quickLogWorkout = (logModel: ToLogWorkoutViewModel) => {
    return axios.post("api/quickLogWorkout", logModel);
  };

  public setShowOnlyUserWods = (showOnlyUserWods: boolean) => {
    return axios.post(`api/setShowOnlyUserWods?showOnlyUserWods=${showOnlyUserWods}`);
  };

  public getLeaderboardByWorkout = (crossfitterWorkoutId): Promise<LeaderboardItemViewModel[]> => {
    // tslint:disable-next-line:max-line-length
    return axios.get<LeaderboardItemViewModel[]>("api/getLeaderboardByWorkout?crossfitterWorkoutId=" + crossfitterWorkoutId).then(jsonData => {
      return jsonData.data.map(x => new LeaderboardItemViewModel().deserialize(x));
    });
  };
}
