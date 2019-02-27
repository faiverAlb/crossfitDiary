import axios, { AxiosResponse } from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";

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
  public getAllCrossfittersWorkouts = (userId?: string, exerciseId?: number, page?: number, pageSize?: number): Promise<ToLogWorkoutViewModel[]> => {
    return axios.get(`api/getAllCrossfittersWorkouts?exerciseId=${exerciseId}&page=${page}&pageSize=${pageSize}`).then(jsonData => {
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

  public removeWorkout = crossfitterWorkoutId => {
    return axios.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
  };

  public quickLogWorkout = (logModel: ToLogWorkoutViewModel) => {
    return axios.post("api/quickLogWorkout", logModel);
  };
}
