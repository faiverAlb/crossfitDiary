module General {
  import IExerciseViewModel = Models.IExerciseViewModel;

  export class CrossfitterService extends BaseService {

    constructor(public pathToApp: string) {
      super();
    }

    createWorkout = model => {
      return this.post(this.pathToApp + "api/createWorkout", model)
        .finally(() => {
          window.location.href = "/Home";
        });
    };

    logWorkout = (model) => {
      return this.post(this.pathToApp + "api/logWorkout", model)
        .finally(() => {
          window.location.href = "/Home";
        });
    };

    createAndLogWorkout = (model) => {
      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model)
        .finally(() => {
          window.location.href = "/Home";
        });
    };

    getAvailableWorkouts = () => {
      return this.get(this.pathToApp + "api/getAvailableWorkouts");
    };

    getExercises = (): Q.Promise<IExerciseViewModel[]> => {
      return this.get<IExerciseViewModel[]>(this.pathToApp + "api/getExercises");
    };

    getStatisticalExercises = () => {
      return this.get(this.pathToApp + "api/getStatisticalExercises");
    };

    getPersonExerciseMaximumWeight = (exerciseId: number) => {
      return this.get(this.pathToApp + `api/exercises/${exerciseId}/personMaximum`);
    };

    getAllPersonsExerciseMaximumWeights = (exerciseId: number) => {
      return this.get(this.pathToApp + `api/exercises/${exerciseId}/allPersonsMaximums`);
    };

    removeWorkout = (crossfitterWorkoutId) => {
      return this.delete(`api/removeWorkout/${crossfitterWorkoutId}`);
    };
  }
}