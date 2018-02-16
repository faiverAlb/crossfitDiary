module General {
  import IExerciseViewModel = Models.ExerciseViewModel;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class CrossfitterService extends BaseService {

    constructor(public pathToApp: string) {
      super();
    }

    createWorkout = (model: WorkoutViewModel) => {
      return this.post(this.pathToApp + "api/createWorkout", model);

    };

    logWorkout = (model: ToLogWorkoutViewModel) => {
      return this.post(this.pathToApp + "api/logWorkout", model);
    };

    createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel, logWorkoutViewModel: ToLogWorkoutViewModel }) => {
      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model);
    };

    getAvailableWorkouts = (): Q.Promise<WorkoutViewModel[]> => {
      return this.get <WorkoutViewModel[]>(this.pathToApp + "api/getAvailableWorkouts");
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