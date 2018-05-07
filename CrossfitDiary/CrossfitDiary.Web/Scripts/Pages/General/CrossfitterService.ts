module General {
  import IExerciseViewModel = Models.ExerciseViewModel;
  import WorkoutViewModel = Models.WorkoutViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;

  export class CrossfitterService extends BaseService {

    constructor(public readonly  pathToApp: string, public isDataLoading: KnockoutObservable<Boolean>) {
      super();
    }

    public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel, logWorkoutViewModel: ToLogWorkoutViewModel }) => {
      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model);
    };

    getAvailableWorkouts = (): Q.Promise<WorkoutViewModel[]> => {
      this.isDataLoading(true);
      return this.get<WorkoutViewModel[]>(this.pathToApp + "api/getAvailableWorkouts").finally(() => { this.isDataLoading(false); });
    };

    getExercises = (): Q.Promise<IExerciseViewModel[]> => {
      this.isDataLoading(true);
      return this.get<IExerciseViewModel[]>(this.pathToApp + "api/getExercises").finally(() => { this.isDataLoading(false); });
    };

    getStatisticalExercises = () => {
      this.isDataLoading(true);
      return this.get(this.pathToApp + "api/getStatisticalExercises").finally(() => { this.isDataLoading(false); });
    };

    getPersonExerciseMaximumWeight = (exerciseId: number) => {
      this.isDataLoading(true);
      return this.get(this.pathToApp + `api/exercises/${exerciseId}/personMaximum`).finally(() => { this.isDataLoading(false); });
    };

    getAllPersonsExerciseMaximumWeights = (exerciseId: number) => {
      this.isDataLoading(true);
      return this.get(this.pathToApp + `api/exercises/${exerciseId}/allPersonsMaximums`).finally(() => { this.isDataLoading(false); });
    };

    removeWorkout = (crossfitterWorkoutId) => {
      this.isDataLoading(true);
      return this.delete(`api/removeWorkout/${crossfitterWorkoutId}`).finally(() => { this.isDataLoading(false); });
    };

    getPersonLoggingInfo = (preselectedCrossfitterWorkoutId: number): Q.Promise<ToLogWorkoutViewModel> => {
      this.isDataLoading(true);
      return this.get<ToLogWorkoutViewModel>(this.pathToApp + `api/getPersonLoggingInfo/${preselectedCrossfitterWorkoutId}`)
        .finally(() => {
          this.isDataLoading(false);
        });
    };
  }
}