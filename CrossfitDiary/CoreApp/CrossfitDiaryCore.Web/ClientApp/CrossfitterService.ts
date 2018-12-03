import axios, { AxiosResponse } from "axios";
import { ToLogWorkoutViewModel } from "./models/viewModels/ToLogWorkoutViewModel";
import { ExerciseViewModel } from "./models/viewModels/ExerciseViewModel";
import { WorkoutViewModel } from "./models/viewModels/WorkoutViewModel";

export default class CrossfitterService {
  public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel; logWorkoutViewModel: ToLogWorkoutViewModel }) => {
    // const config = { headers: { "Content-Type": "application/json" } };
    return axios.post(
      "api/createAndLogNewWorkout",
      model
      // config
    );
  };

  // tslint:disable-next-line:max-line-length
  public getAllCrossfittersWorkouts = (userId?: string, exerciseId?: number, page?: number, pageSize?: number): Promise<ToLogWorkoutViewModel[]> => {
    //      this.isDataLoading(true);
    return axios.get(`api/getAllCrossfittersWorkouts?exerciseId=${exerciseId}&page=${page}&pageSize=${pageSize}`).then(jsonData => {
      return jsonData.data.map(x => new ToLogWorkoutViewModel().deserialize(x));
    });
  };
  //
  public getExercises = (): Promise<ExerciseViewModel[]> => {
    //      this.isDataLoading(true);
    return axios.get<ExerciseViewModel[]>("api/getExercises").then(jsonData => {
      return jsonData.data.map(x => new ExerciseViewModel().deserialize(x));
    });
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
  public removeWorkout = crossfitterWorkoutId => {
    return axios.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
  };
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
