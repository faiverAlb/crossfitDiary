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

  public getExercises = (): Promise<ExerciseViewModel[]> => {
    return axios.get<ExerciseViewModel[]>("api/getExercises").then(jsonData => {
      return jsonData.data.map(x => new ExerciseViewModel().deserialize(x));
    });
  };

  public removeWorkout = crossfitterWorkoutId => {
    return axios.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
  };

  //
  //    public getPersonMaximums = (): Q.Promise<PersonExerciseRecord[]> => {
  ////      this.isDataLoading(true);
  //      return this.get<PersonExerciseRecord[]>(this.pathToApp + "api/getPersonMaximums")
  //        .then((jsonData) => {
  //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
  //        });
  ////        .finally(() => { this.isDataLoading(false); });
  //    };
  //
  //    public getStatisticalExercises = (): Q.Promise<ExerciseViewModel[]> => {
  ////      this.isDataLoading(true);
  //      return this.get<ExerciseViewModel[]>(this.pathToApp + "api/getStatisticalExercises")
  //        .then((jsonData) => {
  //          return jsonData.map(x => new ExerciseViewModel().deserialize(x));
  //        });
  ////        .finally(() => { this.isDataLoading(false); });
  //    };
  //
  //    public getPersonExerciseMaximumWeight = (exerciseId: number) => {
  ////      this.isDataLoading(true);
  //      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/personMaximum`)
  //        .then((jsonData) => {
  //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
  //        });
  ////        .finally(() => { this.isDataLoading(false); });
  //    };
  //
  //    public getAllPersonsExerciseMaximumWeights = (exerciseId: number) => {
  ////      this.isDataLoading(true);
  //      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/allPersonsMaximums`)
  //        .then((jsonData) => {
  //          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
  //        });
  ////        .finally(() => { this.isDataLoading(false); });
  //    };
  //

  //
  //    public getPersonLoggingInfo = (preselectedCrossfitterWorkoutId: number): Q.Promise<ToLogWorkoutViewModel> => {
  ////      this.isDataLoading(true);
  //      return this.get<ToLogWorkoutViewModel>(this.pathToApp +
  //          `api/getPersonLoggingInfo/${preselectedCrossfitterWorkoutId}`)
  //        .then((jsonData) => {
  //          return new ToLogWorkoutViewModel().deserialize(jsonData);
  //        });
  ////        .finally(() => {
  ////          this.isDataLoading(false);
  ////        });
  //    };
}
